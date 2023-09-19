using System;
using System.Text;
using Depra.Serialization.Internal;

namespace Depra.Serialization.Xml
{
	public readonly partial struct StandardXmlSerializer : ITextSerializer
	{
		private static readonly Encoding ENCODING_TYPE = Encoding.UTF8;

		public string SerializeToString<TIn>(TIn input) =>
			ENCODING_TYPE.GetString(Serialize(input));

		public string SerializeToString(object input, Type inputType) =>
			ENCODING_TYPE.GetString(Serialize(input, inputType));

		public TOut Deserialize<TOut>(string input)
		{
			using var stream = new EncodedMemoryStream(ENCODING_TYPE, input);
			return Deserialize<TOut>(stream);
		}

		public object Deserialize(string input, Type outputType)
		{
			using var stream = new EncodedMemoryStream(ENCODING_TYPE, input);
			return Deserialize(stream, outputType);
		}

		string ITextSerializer.SerializeToPrettyString<TIn>(TIn input) =>
			SerializeToString(input);

		string ITextSerializer.SerializeToPrettyString(object input, Type inputType) =>
			SerializeToString(input, inputType);
	}
}