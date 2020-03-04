namespace Cruel.Lexing
{
   public class OperatorLexer : Lexer
   {
      protected TokenType type;

      public override string Pattern => "^ ['+*//^~,-']";

      public override void AddToken(LexingState state, string text)
      {
         switch (text)
         {
            case "+":
               type = TokenType.Plus;
               break;
            case "-":
               type = TokenType.Dash;
               break;
            case "*":
               type = TokenType.Star;
               break;
            case "/":
               type = TokenType.Slash;
               break;
            case "^":
               type = TokenType.Caret;
               break;
            case "~":
               type = TokenType.Tilde;
               break;
            case ",":
               type = TokenType.Comma;
               break;
         }

         base.AddToken(state, text);
      }

      public override TokenType Type => type;
   }
}