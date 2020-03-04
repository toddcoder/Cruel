namespace Cruel.Lexing
{
   public class OpenBraceLexer : Lexer
   {
      public override string Pattern => "^ '['";

      public override TokenType Type => TokenType.OpenBrace;
   }
}