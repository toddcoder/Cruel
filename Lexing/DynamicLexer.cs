namespace Cruel.Lexing
{
   public abstract class DynamicLexer : Lexer
   {
      protected TokenType type;

      public abstract string Pattern { get; }

      public override TokenType Type { get; }
   }
}