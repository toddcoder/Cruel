namespace Cruel.Lexing
{
   public class CloseBraceLexer : Lexer
   {
      public override string Pattern => "^ ']'";

      public override TokenType Type => TokenType.CloseBrace;
   }
}