using System.Collections.Generic;

namespace Cruel.Interfaces
{
	public interface ISerializing
	{
		void Serialize(List<byte> stream);

		void Deserialize(Queue<byte> stream);
	}
}