namespace Cruel.Nodes.Symbols
{
   public abstract class Symbol : Node
   {
      public Symbol(int index, int length, int startLine, int startColumn, int endLine, int endColumn) :
         base(index, length, startLine, startColumn, endLine, endColumn) { }
   }
}