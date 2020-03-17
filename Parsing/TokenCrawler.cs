using System;
using System.Linq;
using Core.Monads;
using Cruel.Lexing;
using static Core.Monads.AttemptFunctions;
using static Core.Monads.MonadFunctions;

namespace Cruel.Parsing
{
   public class TokenCrawler
   {
      public class TokenCrawlerResult
      {
         public TokenCrawlerResult(int index, int length, Token[] tokens)
         {
            Index = index;
            Length = length;
            Tokens = tokens;
         }

         public int Index { get; }

         public int Length { get; }

         public Token[] Tokens { get; }

         public void Deconstruct(out int index, out int length, out Token[] tokens)
         {
            index = Index;
            length = Length;
            tokens = Tokens;
         }
      }

      protected Token[] tokens;
      protected int tokenLength;
      protected int index;
      protected int length;

      public TokenCrawler(Token[] tokens, int index)
      {
         this.tokens = tokens;
         this.index = index;

         tokenLength = this.tokens.Length;
         length = 0;
      }

      public bool More => index < tokenLength;

      public int Index => index;

      public int Length => length;

      protected TokenCrawler addToken()
      {
         length++;
         return this;
      }

      public IResult<TokenCrawlerResult> Result()
      {
         var result = tryTo(() => new TokenCrawlerResult(index, length, tokens.Skip(index).Take(length).ToArray()));
         if (result.HasValue)
         {
            index += length;
            length = 0;
         }

         return result;
      }

      public IMatched<TokenCrawler> Match(Predicate<Token> predicate) => ifMatches(More && predicate(tokens[index]), addToken);

      public IMatched<TokenCrawler> Match(TokenType type) => Match(t => t.Type == type);

      public IMatched<TokenCrawler> Match(string text) => Match(t => t.Text == text);

      public IMatched<TokenCrawler> Match(string text, TokenType type) => Match(t => t.Text == text && t.Type == type);
   }
}