using System.Collections.Generic;

namespace Cruel.VirtualMachine
{
	public class Frame
	{
		protected Stack<long> stack;

		public Frame()
		{
			stack = new Stack<long>();
		}
	}
}