using System;
using Core.Applications;
using Core.Assertions;
using Core.Computers;
using Core.Monads;
using Cruel.Lexing;
using static Core.Assertions.AssertionFunctions;
using static Core.Monads.MonadFunctions;

namespace CruelCompiler
{
   class Program : CommandLineInterface
   {
      static void Main()
      {
         var program = new Program { Test = true };
         program.Run();
      }

      [EntryPoint(EntryPointType.This)]
      public void EntryPoint()
      {
         if (Compile)
         {
            compile();
         }
      }

      public bool Compile { get; set; }

      public IMaybe<FileName> File { get; set; } = none<FileName>();

      void compile()
      {
         assert(() => File).Must().HaveValue().OrThrow();
         if (File.If(out var file))
         {
            assert(() => file).Must().Exist().OrThrow();
            var anyTokens =
               from lexer in Lexer.New(file)
               from parsed in lexer.Parse()
               select parsed;
            if (anyTokens.If(out var tokens))
            {
               foreach (var token in tokens)
               {
                  Console.WriteLine(token);
               }

               Console.WriteLine("Done");
            }
            else
            {
               Console.WriteLine("No tokens");
            }
         }
      }
   }
}