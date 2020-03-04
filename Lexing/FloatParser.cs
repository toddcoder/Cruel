namespace Cruel.Lexing
{
   public class FloatParser : Lexer
   {
      public override string Pattern => "^ /([/d '_']+ '.' [/d '_']+) (/'e' /(['-+']? /d+))? /'i'?";

      public override TokenType Type => TokenType.Float;
   }
}