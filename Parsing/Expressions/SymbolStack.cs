﻿using System.Collections.Generic;
using Core.Enumerables;
using Core.Monads;
using Cruel.Nodes.Expressions;
using static Core.Monads.AttemptFunctions;
using static Core.Monads.MonadFunctions;

namespace Cruel.Parsing.Expressions
{
   public class SymbolStack
   {
      Stack<Symbol> stack;

      public SymbolStack()
      {
         stack = new Stack<Symbol>();
      }

      public void Push(Symbol symbol) => stack.Push(symbol);

      public IResult<Symbol> Pop() => tryTo(() => stack.Pop());

      public IMaybe<Symbol> Peek() => maybe(!IsEmpty, () => stack.Peek());

      public bool IsEmpty => stack.Count == 0;

      public bool IsPending(Symbol next)
      {
         if (IsEmpty)
         {
            return false;
         }

         var symbol = stack.Peek();
         return symbol.LeftToRight ? symbol.Precedence <= next.Precedence : symbol.Precedence < next.Precedence;
      }

      public void Clear() => stack.Clear();

      public override string ToString() => stack.Stringify(" ");
   }
}