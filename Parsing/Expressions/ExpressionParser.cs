using Core.Monads;

namespace Cruel.Parsing.Expressions
{
   public class ExpressionParser : Parser
   {
      public override IMatched<Unit> Parse(ParsingState state)
      {
         var builder = new ExpressionBuilder();
         return Unit.Matched();
      }
   }
}