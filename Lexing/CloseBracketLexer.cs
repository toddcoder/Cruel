namespace Cruel.Lexing
{
   public class CloseBracketLexer : Lexer
   {
      public override string Pattern => "^ '}'";

      public override TokenType Type => TokenType.CloseBracket;
   }
}