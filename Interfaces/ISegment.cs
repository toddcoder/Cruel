namespace Cruel.Interfaces
{
   public interface ISegment
   {
      int Index { get; }

      int Length { get; }

      int StartLine { get; }

      int StartColumn { get; }

      int EndLine { get; }

      int EndColumn { get; }
   }
}