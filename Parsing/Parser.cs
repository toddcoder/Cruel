using Core.Monads;

namespace Cruel.Parsing
{
   public abstract class Parser
   {
      public abstract IMatched<Unit> Parse(ParsingState state);
   }
}