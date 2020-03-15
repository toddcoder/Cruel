using Cruel.Analyzing;
using Cruel.Interfaces;
using Cruel.Operations;

namespace Cruel.Nodes
{
   public abstract class Node : ISegment
   {
      public Node(int index, int length, int startLine, int startColumn, int endLine, int endColumn)
      {
         Index = index;
         Length = length;
         StartLine = startLine;
         StartColumn = startColumn;
         EndLine = endLine;
         EndColumn = endColumn;
      }

      public int Index { get; }

      public int Length { get; }

      public int StartLine { get; }

      public int StartColumn { get; }

      public int EndLine { get; }

      public int EndColumn { get; }

      public abstract void Analyze(AnalysisState state);

      public abstract void Generate(OperationsBuilder builder);
   }
}