using Cruel.Analyzing;
using Cruel.Nodes.Expressions;
using Cruel.Operations;

namespace Cruel.Nodes.Statements
{
   public class ExpressionStatement : Statement
   {
      protected Expression expression;

      public ExpressionStatement(Expression expression) : base(expression.Segment)
      {
         this.expression = expression;
      }

      public override void Analyze(AnalysisState state) => expression.Analyze(state);

      public override void Generate(OperationsBuilder builder) => expression.Generate(builder);

      public override string ToString() => expression.ToString();
   }
}