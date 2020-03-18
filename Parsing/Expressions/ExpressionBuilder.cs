using System.Collections.Generic;
using Core.Enumerables;
using Core.Monads;
using Cruel.Nodes.Expressions;
using static Core.Monads.MonadFunctions;

namespace Cruel.Parsing.Expressions
{
   public class ExpressionBuilder
   {
      protected SymbolStack stack;
      protected List<Symbol> symbols;
      protected List<Symbol> ordered;
      IMaybe<Symbol> lastSymbol;

      public ExpressionBuilder()
      {
         stack = new SymbolStack();
         symbols = new List<Symbol>();
         ordered = new List<Symbol>();
         lastSymbol = none<Symbol>();
      }

      public IResult<Unit> Add(Symbol symbol)
      {
         ordered.Add(symbol);
         lastSymbol = symbol.Some();

         while (stack.IsPending(symbol))
         {
            if (stack.Pop().If(out var poppedSymbol, out var exception))
            {
               symbols.Add(poppedSymbol);
            }
            else
            {
               return failure<Unit>(exception);
            }
         }

         if (symbol.Precedence != Precedence.Value)
         {
            stack.Push(symbol);
            return Unit.Success();
         }
         else
         {
            symbols.Add(symbol);
            return Unit.Success();
         }
      }

      public IResult<Unit> EndOfExpression()
      {
         while (!stack.IsEmpty)
         {
            if (stack.Pop().ValueOrCast<Unit>(out var symbol, out var asUnit))
            {
               symbols.Add(symbol);
            }
            else
            {
               return asUnit;
            }
         }

         return Unit.Success();
      }

      public IResult<Expression> ToExpression() => EndOfExpression().Map(_ => new Expression(symbols.ToArray()));

      public IEnumerable<Symbol> Ordered => ordered;

      public override string ToString() => ordered.Stringify(" ");

      public int Length => ordered.Count;

      public void Clear()
      {
         stack.Clear();
         symbols.Clear();
         ordered.Clear();
      }

      public IMaybe<Symbol> LastSymbol => lastSymbol;
   }
}