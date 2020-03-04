using System.Text;
using Core.Monads;
using Core.Numbers;
using Core.Strings;

namespace Cruel.Lexing
{
   public class StringLexer : Lexer
   {
      public override string Pattern => "^ [dquote]";

      public override TokenType Type => TokenType.String;

      public override void AddToken(LexingState state, string text)
      {
         var builder = new StringBuilder();
         var escaped = false;
         var hex = false;
         var hexText = new StringBuilder();

         foreach (var ch in state.CurrentSource)
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
                        state.Exception(exception);
                        return;
                     }
                     else
                     {
                        state.Exception("Bad hex");
                     }
                  }

                  state.NextToken(builder + "\"", TokenType.String);
                  return;
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
                           state.Exception(exception);
                           return;
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

         state.Exception("Open string");
      }

      public static IMatched<char> fromHex(string text)
      {
         return $"0x{text}".FromHex().Result($"Didn't understand {text}").Match().Map(i => (char)i);
      }
   }
}