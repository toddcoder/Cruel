using System;
using System.Collections.Generic;
using System.Linq;
using Core.Monads;
using Core.RegularExpressions;
using Core.Strings;

namespace Cruel.Lexing
{
   public class LexingState
   {
      protected string source;
      protected int index;
      protected int line;
      protected int column;
      protected List<Token> tokens;
      protected Matcher matcher;
      protected bool more;

      public LexingState(string source)
      {
         this.source = source;

         index = 0;
         line = 1;
         column = 1;
         tokens = new List<Token>();
         matcher = new Matcher();
      }

      public IEnumerable<Token> Tokens => tokens;

      public int Index => index;

      public string CurrentSource => source.Drop(index);

      public void NextToken(string fragment, TokenType type)
      {
         var length = fragment.Length;
         var startLine = line;
         var startColumn = column;
         if (type == TokenType.EndOfLine)
         {
            line++;
            column = 1;
         }

         column += length;
         var endLine = line;
         var endColumn = column;

         var segment =new Segment(index, length, startLine, startColumn, endLine, endColumn);
         var token = new Token(fragment, type, segment);
         tokens.Add(token);

         index += length;

         if (type == TokenType.EndOfFile)
         {
            more = false;
         }
      }

      public void Exception(Exception exception) => Exception(exception.Message);

      public void Exception(string message) => NextToken(message, TokenType.Exception);

      public bool More => more;

      public IMatched<string> Match(string pattern)
      {
	      var currentSource = source.Drop(index);
	      return matcher.MatchOne(currentSource, pattern).Map(m => m.Text);
      }

      public IMatched<string[]> MatchGrouped(string pattern)
      {
	      var currentSource = source.Drop(index);
	      return matcher.MatchAll(currentSource, pattern).Map(m => m.Select(n => n.Text).ToArray());
      }
   }
}