using Cruel.Analyzing;
using Cruel.Interfaces;
using Cruel.Operations;

namespace Cruel.Nodes
{
   public abstract class Node : ISegment
   {
      public Node(Segment segment)
      {
         Segment = segment;
      }

      public Segment Segment { get; }

      public abstract void Analyze(AnalysisState state);

      public abstract void Generate(OperationsBuilder builder);
   }
}