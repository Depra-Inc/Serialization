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
		internal static byte[] SerializeToBytes<TIn>(IGenericSerializer serializer, TIn input)
		{
			using var memoryStream = WrapSerializationToMemoryStream(serializer, input);
			return memoryStream.ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static string SerializeToString<TIn>(IGenericSerializer serializer, TIn input, Encoding encoding)
		{
			using var memoryStream = WrapSerializationToMemoryStream(serializer, input);
			return encoding.GetString(memoryStream.ToArray());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static TOut DeserializeFromString<TOut>(IGenericSerializer serializer, string input, Encoding encoding)
		{
			using var memoryStream = new MemoryStream(encoding.GetBytes(input));
			return serializer.Deserialize<TOut>(memoryStream);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Task SerializeAsync<TIn>(IGenericSerializer self, Stream outputStream, TIn input) =>
			Task.Run(() => self.Serialize(outputStream, input));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ValueTask<TOut> DeserializeAsync<TOut>(IGenericSerializer self, Stream inputStream,
			CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return new ValueTask<TOut>(self.Deserialize<TOut>(inputStream));
		}

		/// <summary>
		/// Serializes the given <paramref name="input"/> into <see cref="MemoryStream"/>.
		/// </summary>
		/// <param name="serializer"><see cref="IGenericSerializer"/> for <paramref name="input"/>.</param>
		/// <param name="input">The object to be serialized.</param>
		/// <returns>The serialized <paramref name="input"/> as <see cref="MemoryStream"/>.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static MemoryStream WrapSerializationToMemoryStream<TIn>(IGenericSerializer serializer, TIn input)
		{
			var memoryStream = new MemoryStream();
			serializer.Serialize(memoryStream, input);

			return memoryStream;
		}
	}
}