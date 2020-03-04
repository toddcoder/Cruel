namespace Cruel.Lexing
{
   public class Token
   {
      public Token(string text, TokenType type, int index, int length, int startLine, int startColumn, int endLine, int endColumn)
      {
         Text = text;
         Type = type;
         Index = index;
         Length = length;
         StartLine = startLine;
         StartColumn = startColumn;
         EndLine = endLine;
         EndColumn = endColumn;
      }

      public string Text { get; }

      public TokenType Type { get; }

      public int Index { get; }

      public int Length { get; }

      public int StartLine { get; }

      public int StartColumn { get; }

      public int EndLine { get; }

      public int EndColumn { get; }

      public override string ToString() => $"'{Text}' @ ({Index}:{Length}) [{StartLine}, {StartColumn}]-[{EndLine}, {EndColumn}]";
   }
}