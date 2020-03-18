namespace Cruel
{
   public class Segment
   {
      public static Segment Empty => new Segment(-1, -1, 0, 0, 0, 0);

      public Segment(int index, int length, int startLine, int startColumn, int endLine, int endColumn)
      {
         Index = index;
         Length = length;
         StartLine = startLine;
         StartColumn = startColumn;
         EndLine = endLine;
         EndColumn = endColumn;
      }

      public Segment() : this(0, 0, 1, 1, 1, 1) { }

      public int Index { get; set; }

      public int Length { get; set; }

      public int StartLine { get; set; }

      public int StartColumn { get; set; }

      public int EndLine { get; set; }

      public int EndColumn { get; set; }

      public void Deconstruct(out int index, out int length, out int startLine, out int startColumn, out int endLine,
         out int endColumn)
      {
         index = Index;
         length = Length;
         startLine = StartLine;
         startColumn = StartColumn;
         endLine = EndLine;
         endColumn = EndColumn;
      }

      public override string ToString() => $"({Index}:{Length}) [{StartLine}, {StartColumn}]-[{EndLine}, {EndColumn}]";
   }
}