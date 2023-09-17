using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Extensions
{
	public static partial class SerializerExtensions
	{
		internal static object DeserializeFromString(ISerializer serializer, string input, Encoding encoding, Type outputType)
		{
			var bytes = encoding.GetBytes(input);
			using var stream = new MemoryStream(bytes);

			return serializer.Deserialize(stream, outputType);
		}

		internal static ValueTask<object> DeserializeAsync(this ISerializer self, Stream inputStream,
			CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return new ValueTask<object>(self.Deserialize(inputStream, typeof(object)));
		}
	}
}