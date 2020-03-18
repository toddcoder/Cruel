using Core.Enumerables;
using Cruel.Analyzing;
using Cruel.Operations;
using Cruel.Parsing.Expressions;

namespace Cruel.Nodes.Expressions
{
   public class Expression : Symbol
   {
      protected Symbol[] symbols;

      public Expression(Symbol[] symbols) : base(symbols.Segment())
      {
         this.symbols = symbols;
      }

      public override void Analyze(AnalysisState state) { }

      public override void Generate(OperationsBuilder builder) { }

      public override int Arity => 0;

      public override Precedence Precedence => Precedence.Value;

      public override string ToString() => symbols.Stringify(" ");
   }
}