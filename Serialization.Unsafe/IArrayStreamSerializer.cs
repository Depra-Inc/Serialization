using System.IO;

namespace Depra.Serialization.Unsafe
{
	public interface IArrayStreamSerializer
	{
		void Serialize<TIn>(Stream outputStream, TIn[] input);

		TOut[] Deserialize<TOut>(Stream inputStream);
	}
}