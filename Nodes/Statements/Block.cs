using Core.Enumerables;
using Cruel.Analyzing;
using Cruel.Operations;

namespace Cruel.Nodes.Statements
{
   public class Block : Statement
   {
      Statement[] statements;

      public Block(Segment segment, Statement[] statements) : base(segment)
      {
         this.statements = statements;
      }

      public override void Analyze(AnalysisState state)
      {
         foreach (var statement in statements)
         {
            statement.Analyze(state);
         }
      }

      public override void Generate(OperationsBuilder builder)
      {
         foreach (var statement in statements)
         {
            statement.Generate(builder);
         }
      }

      public override string ToString() => statements.Stringify("\r\n");
   }
}