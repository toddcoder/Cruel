using Cruel.Parsing.Expressions;

namespace Cruel.Nodes.Expressions
{
   public abstract class Symbol : Node
   {
      public Symbol(int index, int length, int startLine, int startColumn, int endLine, int endColumn) :
         base(index, length, startLine, startColumn, endLine, endColumn) { }

      public abstract int Arity { get; }

      public virtual bool LeftToRight => true;

      public abstract Precedence Precedence { get;  }
   }
}