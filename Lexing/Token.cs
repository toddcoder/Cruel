namespace Cruel.Lexing
{
   public class Token
   {
      public Token(string text, TokenType type, Segment segment)
      {
         Text = text;
         Type = type;
         Segment = segment;
      }

      public Token(string text, TokenType type, Substring substring) : this(text, type, substring.Segment) { }

      public string Text { get; }

      public TokenType Type { get; }

      public Segment Segment { get; }

      public override string ToString() => $"'{Text}'-{Type} @ {Segment}";
   }
}