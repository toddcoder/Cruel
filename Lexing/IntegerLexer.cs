namespace Cruel.Lexing
{
   public class IntegerLexer : Lexer
   {
      public override string Pattern => "^ /([/d '_']+) /['Lif']? /b";

      public override TokenType Type => TokenType.Integer;
   }
}