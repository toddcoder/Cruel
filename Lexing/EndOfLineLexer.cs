namespace Cruel.Lexing
{
   public class EndOfLineLexer : Lexer
   {
      public override string Pattern => "^ /r /n | /r | /n | ';'";

      public override TokenType Type => TokenType.EndOfLine;
   }
}