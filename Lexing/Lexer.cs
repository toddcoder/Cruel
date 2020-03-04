using Core.Monads;
using static Core.Monads.MonadFunctions;

namespace Cruel.Lexing
{
   public abstract class Lexer
   {
      public abstract string Pattern { get; }

      public abstract TokenType Type { get; }

      public virtual IMaybe<Unit> Match(LexingState state)
      {
         if (state.Match(Pattern).If(out var text, out var anyException))
         {
            AddToken(state, text);
            return none<Unit>();
         }
         else if (anyException.If(out var exception))
         {
            state.NextToken(exception.Message, TokenType.Exception);
            return none<Unit>();
         }
         else
         {
            return Unit.Some();
         }
      }

      public virtual void AddToken(LexingState state, string text) => state.NextToken(text, Type);
   }
}