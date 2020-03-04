namespace Cruel.Lexing
{
   public class WhitespaceLexer : Lexer
   {
      public override string Pattern => "^ [' ' /t]+";

      public override TokenType Type => TokenType.Whitespace;
   }
}