namespace Cruel.Lexing
{
   public class CloseParenthesisLexer : Lexer
   {
      public override string Pattern => "^ ')'";

      public override TokenType Type => TokenType.CloseParenthesis;
   }
}