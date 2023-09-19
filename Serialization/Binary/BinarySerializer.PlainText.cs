using System;
using System.Text;
using Depra.Serialization.Internal;

namespace Depra.Serialization.Binary
{
	public sealed partial class BinarySerializer : ITextSerializer
	{
		private static readonly Encoding ENCODING_TYPE = Encoding.ASCII;

		public string SerializeToString<TIn>(TIn input) =>
			ENCODING_TYPE.GetString(Serialize(input));

		public string SerializeToString(object input, Type inputType) =>
			ENCODING_TYPE.GetString(Serialize(input, inputType));

		public TOut Deserialize<TOut>(string input)
		{
			using var memoryStream = new EncodedMemoryStream(ENCODING_TYPE, input);
			return Deserialize<TOut>(memoryStream);
		}

		public object Deserialize(string input, Type outputType)
		{
			using var memoryStream = new EncodedMemoryStream(ENCODING_TYPE, input);
			return Deserialize(memoryStream, outputType);
		}

		string ITextSerializer.SerializeToPrettyString<TIn>(TIn input) =>
			SerializeToString(input);

		string ITextSerializer.SerializeToPrettyString(object input, Type inputType) =>
			SerializeToString(input, inputType);
	}
}