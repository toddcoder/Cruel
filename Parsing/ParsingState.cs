using Cruel.Lexing;

namespace Cruel.Parsing
{
   public class ParsingState
   {
      public ParsingState(Token[] tokens)
      {
         Crawler = new TokenCrawler(tokens, 0);
      }

      public TokenCrawler Crawler { get; }
   }
}