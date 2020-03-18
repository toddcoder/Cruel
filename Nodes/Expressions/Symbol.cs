using Cruel.Parsing.Expressions;

namespace Cruel.Nodes.Expressions
{
   public abstract class Symbol : Node
   {
      public Symbol(Segment segment) : base(segment) { }

      public abstract int Arity { get; }

      public virtual bool LeftToRight => true;

      public abstract Precedence Precedence { get; }
   }
}