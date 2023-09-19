using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Depra.Serialization.Unsafe
{
	public readonly struct UnsafeBinaryStreamSerializer : IArrayStreamSerializer
	{
		public void Serialize<T>(Stream outputStream, T[] input)
		{
			var bytes = ChangeType(input, Array.Empty<byte>());
			try
			{
				using var writer = new BinaryWriter(outputStream);
				writer.Write(bytes.LongLength);
				writer.Write(bytes);
			}
			finally
			{
				ChangeType(bytes, Array.Empty<T>());
			}
		}

		public T[] Deserialize<T>(Stream inputStream)
		{
			using var reader = new BinaryReader(inputStream);
			var lenght = reader.ReadInt64();
			var bytes = reader.ReadBytes((int)lenght);
			return ChangeType(bytes, Array.Empty<T>());
		}

		private static unsafe byte[] ChangeType<T>(T[] input, byte[] bytes)
		{
			var size = Marshal.SizeOf<T>();
			var arrayPointer = (long*)AddressOf(input).ToPointer();
			var destinationPointer = (long*)AddressOf(bytes).ToPointer();
			var newLenght = size / sizeof(byte) * input.LongLength;
			*arrayPointer = *destinationPointer;
			*(arrayPointer + 1) = newLenght;

			var result = (object)input;
			var resultAsBytes = (byte[])result;

			return resultAsBytes;
		}

		private static unsafe T[] ChangeType<T>(byte[] input, T[] sample)
		{
			var size = Marshal.SizeOf<T>();
			var arrayPointer = (long*)AddressOf(input).ToPointer();
			var destinationPointer = (long*)AddressOf(sample).ToPointer();
			var newLength = input.Length / size;
			*arrayPointer = *destinationPointer;
			*(arrayPointer + 1) = newLength;

			var result = (object)input;
			var resultAsT = (T[])result;

			return resultAsT;
		}

		private static unsafe IntPtr AddressOf(object value)
		{
			var typedReference = __makeref(value);
			return **(IntPtr**)&typedReference;
		}
	}
}