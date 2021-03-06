﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Assertions;
using Core.Computers;
using Core.Monads;
using Core.Numbers;
using Core.RegularExpressions;
using Core.Strings;
using static Core.Assertions.AssertionFunctions;

namespace Cruel.Lexing
{
   public class Lexer
   {
      protected const string REGEX_WHITESPACE = "^ [' /t']+";
      protected const string REGEX_END_OF_LINE = "^ (/r /n | /r | /n)";
      protected const string REGEX_IDENTIFIER = "^ (['A-Za-z_'] ['A-Za-z0-9_']*)";
      protected const string REGEX_FLOAT = "^ ([/d '_']+ '.' [/d '_']+) (/'e' /(['-+']? /d+))? /'i'?";
      protected const string REGEX_INT = "^ ([/d '_']+) /['Lif']? /b";
      protected const string REGEX_STRING = "^ [dquote]";
      protected const string REGEX_OPERATOR = "^ ['+*//^~-']";

      public static IResult<Lexer> New(FileName file)
      {
         var fileTrying = file.TryTo;
         return
            from extension in assert(() => file).Must().HaveExtensionOf(".cruel").OrFailure()
            from exists in assert(() => file).Must().Exist().OrFailure()
            from source in fileTrying.Text
            select new Lexer(source);
      }

      protected Substring substring;
      protected List<Token> tokens;
      protected Matcher matcher;

      public Lexer(string source)
      {
         substring = new Substring(source);
         tokens = new List<Token>();
         matcher = new Matcher();
      }

      public IResult<IEnumerable<Token>> Parse()
      {
         var result = true;
         while (result && substring.More)
         {
            result = matchWhitespace() || matchEndOfLine() || matchIdentifier() || matchFloat() || matchInt() || matchString() ||
               matchOperator() || matchOpenParenthesis() || matchCloseParenthesis() || matchOpenBracket() || matchCloseBracket() ||
               matchOpenBrace() || matchCloseBrace() || matchSemicolon() || matchComma() || matchEqual();
         }

         return tokens.Success<IEnumerable<Token>>();
      }

      protected bool match(string text, TokenType type, Func<string, string, bool> matchFunc)
      {
         if (matchFunc(substring.Current, text) && substring.Advance(text.Length))
         {
            var token = new Token(text, type, substring);
            tokens.Add(token);
            if (substring.Next())
            {
               substring.PreviousNewLine = type == TokenType.EndOfLine;
               return true;
            }
         }

         return false;
      }

      protected bool match(string pattern, TokenType type, Func<Matcher, string> matchFunc)
      {
         if (matcher.IsMatch(substring.Current, pattern))
         {
            var text = matchFunc(matcher);
            if (substring.Advance(text.Length))
            {
               var token = new Token(text, type, substring);
               tokens.Add(token);
               if (substring.Next())
               {
                  substring.PreviousNewLine = type == TokenType.EndOfLine;
                  return true;
               }
            }
         }

         return false;
      }

      protected bool match(string pattern, TokenType type) => match(pattern, type, m => m.FirstMatch);

      protected bool match(char ch, TokenType type)
      {
         if (substring.More && substring.CurrentChar == ch && substring.Advance(1))
         {
            var token = new Token(ch.ToString(), type, substring);
            tokens.Add(token);
            if (substring.Next())
            {
               substring.PreviousNewLine = type == TokenType.EndOfLine;
               return true;
            }
         }

         return false;
      }

      protected static IMatched<char> fromHex(string text)
      {
         return $"0x{text}".FromHex().Result($"Didn't understand {text}").Match().Map(i => (char)i);
      }

      protected void addException(string message)
      {
         var token = new Token(message, TokenType.Exception, substring);
         tokens.Add(token);
      }

      protected void addException(Exception exception) => addException(exception.Message);

      protected bool matchWhitespace() => match(REGEX_WHITESPACE, TokenType.Whitespace);

      protected bool matchEndOfLine() => match(REGEX_END_OF_LINE, TokenType.EndOfLine);

      protected bool matchIdentifier() => match(REGEX_IDENTIFIER, TokenType.Identifier);

      protected bool matchFloat() => match(REGEX_FLOAT, TokenType.Float);

      protected bool matchInt() => match(REGEX_INT, TokenType.Integer);

      protected bool matchString()
      {
         if (matcher.IsMatch(substring.Current, REGEX_STRING))
         {
            var current = substring.Current;
            var builder = new StringBuilder("\"");
            var escaped = false;
            var hex = false;
            var hexText = new StringBuilder();

            foreach (var ch in current.Skip(1))
            {
               switch (ch)
               {
                  case '"':
                     if (escaped)
                     {
                        builder.Append('"');
                        escaped = false;
                        break;
                     }

                     if (hex)
                     {
                        if (fromHex(hexText.ToString()).If(out var matchedChar, out var anyException))
                        {
                           builder.Append(matchedChar);
                        }
                        else if (anyException.If(out var exception))
                        {
                           substring.Advance(hexText.Length);
                           addException(exception);
                           substring.Next();
                           return false;
                        }
                        else
                        {
                           substring.Advance(hexText.Length);
                           addException("Bad hex");
                           substring.Next();
                        }
                     }

                     builder.Append("\"");
                     var text = builder.ToString();
                     if (substring.Advance(text.Length))
                     {
                        var token = new Token(text, TokenType.String, substring);
                        tokens.Add(token);
                        return substring.Next();
                     }
                     else
                     {
                        return false;
                     }
                  case '\\':
                     if (escaped)
                     {
                        builder.Append('\\');
                        escaped = false;
                        break;
                     }

                     escaped = true;
                     break;
                  case 'n':
                     if (escaped)
                     {
                        builder.Append('\n');
                        escaped = false;
                        break;
                     }

                     builder.Append('n');
                     break;
                  case 'r':
                     if (escaped)
                     {
                        builder.Append('\r');
                        escaped = false;
                        break;
                     }

                     builder.Append('r');
                     break;
                  case 't':
                     if (escaped)
                     {
                        builder.Append('\t');
                        escaped = false;
                        break;
                     }

                     builder.Append('t');
                     break;
                  case 'u':
                     if (escaped)
                     {
                        hex = true;
                        hexText.Clear();
                        escaped = false;
                        break;
                     }

                     builder.Append('u');
                     break;
                  case '{':
                     if (escaped)
                     {
                        hex = true;
                        hexText.Clear();
                        escaped = false;
                        break;
                     }

                     builder.Append('{');
                     break;
                  default:
                     if (hex)
                     {
                        if (ch.Between('0').And('9') || ch.Between('a').And('f') && hexText.Length < 6)
                        {
                           hexText.Append(ch);
                        }
                        else
                        {
                           hex = false;
                           if (fromHex(hexText.ToString()).If(out var matchedChar, out var anyException))
                           {
                              builder.Append(matchedChar);
                           }
                           else if (anyException.If(out var exception))
                           {
                              substring.Advance(hexText.Length);
                              addException(exception);
                              substring.Next();
                              return false;
                           }

                           if (ch == 96)
                           {
                              builder.Append(ch);
                           }
                        }
                     }
                     else
                     {
                        builder.Append(ch);
                     }

                     escaped = false;
                     break;
               }
            }

            substring.Advance(builder.Length);
            addException("Open string");
            substring.Next();
         }

         return false;
      }

      protected bool matchOperator()
      {
         if (matcher.IsMatch(substring.Current, REGEX_OPERATOR))
         {
            var operatorSource = matcher.FirstMatch;
            if (substring.Advance(operatorSource.Length))
            {
               TokenType type;
               switch (operatorSource)
               {
                  case "+":
                     type = TokenType.Plus;
                     break;
                  case "-":
                     type = TokenType.Dash;
                     break;
                  case "*":
                     type = TokenType.Star;
                     break;
                  case "/":
                     type = TokenType.Slash;
                     break;
                  case "^":
                     type = TokenType.Caret;
                     break;
                  case "~":
                     type = TokenType.Tilde;
                     break;
                  default:
                     return false;
               }

               var token = new Token(operatorSource, type, substring);
               tokens.Add(token);

               return substring.Next();
            }
         }

         return false;
      }

      protected bool matchOpenParenthesis() => match('(', TokenType.OpenParenthesis);

      protected bool matchCloseParenthesis() => match(')', TokenType.CloseParenthesis);

      protected bool matchOpenBracket() => match('[', TokenType.OpenBracket);

      protected bool matchCloseBracket() => match(']', TokenType.CloseBracket);

      protected bool matchOpenBrace() => match('{', TokenType.OpenBrace);

      protected bool matchCloseBrace() => match('}', TokenType.CloseBrace);

      protected bool matchSemicolon() => match(';', TokenType.Semicolon);

      protected bool matchComma() => match(',', TokenType.Comma);

      protected bool matchEqual() => match('=', TokenType.Equal);
   }
}