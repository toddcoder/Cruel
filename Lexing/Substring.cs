using Core.Strings;

namespace Cruel.Lexing
{
   public class Substring
   {
      protected string source;
      protected int sourceLength;
      protected int index;
      protected int length;
      protected int startLine;
      protected int startColumn;
      protected int endLine;
      protected int endColumn;

      public Substring(string source)
      {
         this.source = source;

         sourceLength = this.source.Length;
         index = 0;
         length = 0;
         startLine = 1;
         startColumn = 1;
         endLine = 1;
         endColumn = 1;
      }

      public bool More => index < sourceLength;

      public string Current => source.Drop(index);

      public char CurrentChar => source[index];

      public int Index => index;

      public int Length => length;

      public int StartLine => startLine;

      public int StartColumn => startColumn;

      public int EndLine => endLine;

      public int EndColumn => endColumn;

      public bool PreviousNewLine { get; set; }

      public bool Advance(int lengthOfToken)
      {
         length = lengthOfToken;
         if (PreviousNewLine)
         {
            endColumn = length;
            startLine++;
            endLine++;
         }
         else
         {
            endColumn = endColumn + length - 1;
         }

         return More;
      }

      public bool Next()
      {
         index += length;

         if (PreviousNewLine)
         {
            startColumn = 1;
            PreviousNewLine = false;
         }
         else
         {
            startColumn += length;
         }

         return More;
      }
   }
}