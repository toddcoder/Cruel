namespace Cruel.Lexing
{
	public class IdentifierLexer : Lexer
   {
      public override string Pattern => "^ ['A-Za-z_'] ['A-Za-z_0-9']*";

      public override TokenType Type => TokenType.Identifier;
   }
}