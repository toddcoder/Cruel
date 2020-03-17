using Core.Strings;

namespace Cruel.Lexing
{
   public class Substring
   {
      protected string source;
      protected int sourceLength;
      protected Segment segment;

      public Substring(string source)
      {
         this.source = source;

         sourceLength = this.source.Length;
         segment = new Segment();
      }

      public bool More => segment.Index < sourceLength;

      public string Current => source.Drop(segment.Index);

      public char CurrentChar => source[segment.Index];

      public Segment Segment => segment;

      public bool PreviousNewLine { get; set; }

      public bool Advance(int lengthOfToken)
      {
         segment.Length = lengthOfToken;
         if (PreviousNewLine)
         {
            segment.EndColumn = segment.Length;
            segment.StartLine++;
            segment.EndLine++;
         }
         else
         {
            segment.EndColumn = segment.EndColumn + segment.Length - 1;
         }

         return More;
      }

      public bool Next()
      {
         segment.Index += segment.Length;

         if (PreviousNewLine)
         {
            segment.StartColumn = 1;
            PreviousNewLine = false;
         }
         else
         {
            segment.StartColumn += segment.Length;
         }

         return More;
      }
   }
}