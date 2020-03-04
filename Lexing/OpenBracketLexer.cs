namespace Cruel.Lexing
{
   public class OpenBracketLexer : Lexer
   {
      public override string Pattern => "^ '{'";

      public override TokenType Type => TokenType.OpenBracket;
   }
}