namespace Cruel.VirtualMachine
{
	public struct Operation
	{
		public Operation(OperationCode operationCode, long operand)
		{
			OperationCode = operationCode;
			Operand = operand;
		}

		public OperationCode OperationCode { get; }

		public long Operand { get; }
	}
}