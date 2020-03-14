using System.Collections.Generic;

namespace Cruel.VirtualMachine
{
   public class Machine
   {
      Module main;
      Stack<long> stack;

      public Machine(Module main)
      {
         this.main = main;
         stack = new Stack<long>();
      }

      public void Execute(IHostInterface hostInterface)
      {
         var index = 0;
         var incrementIndex = true;
         while (index < main.OperationsCount)
         {
            var operation = main.GetOperation(index);
            var operand = operation.Operand;

            switch (operation.OperationCode)
            {
               case OperationCode.NoOp:
                  break;
               case OperationCode.Load:
                  break;
               case OperationCode.LoadI:
                  break;
               case OperationCode.Save:
                  break;
               case OperationCode.SaveI:
                  break;
               case OperationCode.Branch:
                  break;
               case OperationCode.BranchI:
                  break;
               case OperationCode.BranchIfTrue:
                  break;
               case OperationCode.BranchIfTrueI:
                  break;
               case OperationCode.BranchIfFalse:
                  break;
               case OperationCode.BranchIfFalseI:
                  break;
            }

            if (incrementIndex)
            {
               index++;
            }
         }
      }
   }
}