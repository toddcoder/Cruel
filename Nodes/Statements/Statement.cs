namespace Cruel.Nodes.Statements
{
   public abstract class Statement : Node
   {
      public Statement(int index, int length, int startLine, int startColumn, int endLine, int endColumn) :
         base(index, length, startLine, startColumn, endLine, endColumn) { }
   }
}