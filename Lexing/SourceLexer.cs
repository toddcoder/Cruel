using System.Collections.Generic;
using System.Security.Cryptography;
using Core.Monads;

namespace Cruel.Lexing
{
   public class SourceLexer
   {
      protected IdentifierLexer identifierLexer;
      protected WhitespaceLexer whitespaceLexer;
      protected EndOfLineLexer endOfLineLexer;
      protected OpenParenthesisLexer openParenthesisLexer;
      protected CloseParenthesisLexer closeParenthesisLexer;
      protected CommaLexer commaLexer;
      protected StringLexer stringLexer;
      protected IntegerLexer integerLexer;
      protected FloatLexer floatLexer;
      protected OpenBracketLexer openBracketLexer;
      protected CloseBracketLexer closeBracketLexer;
      protected OpenBraceLexer openBraceLexer;
      protected CloseBraceLexer closeBraceLexer;
      protected OperatorLexer operatorLexer;

      public SourceLexer()
      {
         identifierLexer = new IdentifierLexer();
         whitespaceLexer = new WhitespaceLexer();
         endOfLineLexer = new EndOfLineLexer();
         openParenthesisLexer = new OpenParenthesisLexer();
         closeParenthesisLexer = new CloseParenthesisLexer();
         commaLexer = new CommaLexer();
         stringLexer = new StringLexer();
         integerLexer = new IntegerLexer();
         floatLexer = new FloatLexer();
         openBracketLexer = new OpenBracketLexer();
         closeBracketLexer = new CloseBracketLexer();
         openBraceLexer = new OpenBraceLexer();
         closeBraceLexer = new CloseBraceLexer();
         operatorLexer = new OperatorLexer();
      }

      public IResult<IEnumerable<Token>> Tokenize(string source)
      {
         var state = new LexingState(source);

         var result=
            from identifier in identifierLexer.Match(state)
            from whitespace in whitespaceLexer.Match(state)
            from endOfLine in endOfLineLexer.Match(state)
            from openParenthesis in openParenthesisLexer.Match(state)
            from closeParenthesis in closeParenthesisLexer.Match(state)
            from comma in commaLexer.Match(state)
            from str in stringLexer.Match(state)
            from flt in floatLexer.Match(state)
            from integer in integerLexer.Match(state)
            from 
      }
   }
}

/*
 *       
      OpenBracket,
      CloseBracket,
      OpenBrace,
      CloseBrace,
      Plus,
      Dash,
      Star,
      Slash,
      Caret,
      Tilde,
*/