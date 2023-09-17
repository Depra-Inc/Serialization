using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Internal
{
	internal static partial class SerializationHelper
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static byte[] SerializeToBytes(ISerializer serializer, object input, Type inputType)
		{
			using var memoryStream = WrapSerializationToMemoryStream(serializer, input, inputType);
			return memoryStream.ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static string SerializeToString(ISerializer serializer, object input, Type inputType, Encoding encoding)
		{
			using var memoryStream = WrapSerializationToMemoryStream(serializer, input, inputType);
			return encoding.GetString(memoryStream.ToArray());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static object DeserializeFromString(ISerializer serializer, string input, Type outputType, Encoding encoding)
		{
			using var memoryStream = new MemoryStream(encoding.GetBytes(input));
			return serializer.Deserialize(memoryStream, outputType);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Task SerializeAsync(ISerializer self, Stream outputStream, object input, Type inputType) =>
			Task.Run(() => self.Serialize(outputStream, input, inputType));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ValueTask<object> DeserializeAsync(this ISerializer self, Stream inputStream, Type outputType,
			CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return new ValueTask<object>(self.Deserialize(inputStream, outputType));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static MemoryStream WrapSerializationToMemoryStream(this ISerializer serializer, object input, Type inputType)
		{
			var memoryStream = new MemoryStream();
			serializer.Serialize(memoryStream, input, inputType);

			return memoryStream;
		}
	}
}