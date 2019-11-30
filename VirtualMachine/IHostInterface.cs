using Core.Monads;

namespace Cruel.VirtualMachine
{
	public interface IHostInterface
	{
		void Print(string text);

		IResult<string> ReadLine();
	}
}