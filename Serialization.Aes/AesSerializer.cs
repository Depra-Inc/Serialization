using System;

namespace Depra.Serialization.Aes
{
	public class AesSerializer : ISerializer
	{
		public byte[] Serialize<TIn>(TIn input) => throw new NotImplementedException();

		public byte[] Serialize(object input, Type inputType) => throw new NotImplementedException();

		public TOut Deserialize<TOut>(byte[] input) => throw new NotImplementedException();

		public object Deserialize(byte[] input, Type outputType) => throw new NotImplementedException();
	}
}