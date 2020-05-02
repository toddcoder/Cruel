using System.Collections.Generic;
using Core.Monads;
using Cruel.Lexing;
using Cruel.Nodes.Statements;

namespace Cruel.Parsing
{
   public class CruelParser
   {
      protected static Parser[] parsers;

      static CruelParser()
      {
         var list = new List<Parser>();
         list.Add(new ExpressionStatement());
      }

      protected IEnumerable<Token> tokens;

      public CruelParser(IEnumerable<Token> tokens)
      {
         this.tokens = tokens;
      }

      public IResult<Block> Parse()
      {
      }
   }
}