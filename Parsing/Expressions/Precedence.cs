namespace Cruel.Parsing.Expressions
{
   public enum Precedence
   {
      Value = 0,
      SendMessage = 10,
      PrefixOperator = 20,
      PostfixOperator = 30,
      Raise = 40,
      MultiplyDivide = 50,
      Range = 60,
      AddSubtract = 70,
      Shift = 80,
      Boolean = 90,
      BitAnd = 100,
      BitXOr = 110,
      BitOr = 120,
      And = 130,
      Or = 140,
      Format = 150,
      Concatenate = 160,
      ChainedOperator = 170,
      KeyValue = 180,
      Comma = 190
   }
}