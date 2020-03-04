namespace Cruel.Lexing
{
   public class OpenParenthesisLexer : Lexer
   {
      public override string Pattern => "^ '('";

      public override TokenType Type => TokenType.OpenParenthesis;
   }
}