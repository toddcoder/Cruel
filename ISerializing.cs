using System.Collections.Generic;

namespace Cruel
{
	public interface ISerializing
	{
		void Serialize(List<byte> stream);

		void Deserialize(Queue<byte> stream);
	}
}