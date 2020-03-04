namespace Cruel.Lexing
{
   public class CommaLexer : Lexer
   {
      public override string Pattern => "^ ','";

      public override TokenType Type => TokenType.Comma;
   }
}