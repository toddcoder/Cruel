namespace Cruel.VirtualMachine
{
	public class Module
	{
		long[] fields;
		Operation[] operations;

		public Module(int length, Operation[] operations)
		{
			fields = new long[length];
			this.operations = operations;
		}

		public int OperationsCount => operations.Length;

		public void SetField(int index, long value) => fields[index] = value;

		public long GetField(int index) => fields[index];

		public Operation GetOperation(int index) => operations[index];
	}
}