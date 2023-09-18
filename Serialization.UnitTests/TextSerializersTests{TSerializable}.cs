using Depra.Serialization.Binary;
using Depra.Serialization.Json;
using Depra.Serialization.Xml;

namespace Depra.Serialization.UnitTests;

[TestFixture(typeof(SerializableClass))]
[TestFixture(typeof(SerializableStruct))]
[TestFixture(typeof(SerializableRecord))]
internal sealed class TextSerializersTests<TSerializable> where TSerializable : new()
{
	private static IEnumerable<ITextSerializer> GetSerializers()
	{
		// Binary.
#pragma warning disable CS0612
		yield return new BinarySerializer();
#pragma warning restore CS0612

		// XML.
		yield return new StandardXmlSerializer();
		yield return new DataContractXmlSerializer();

		// Json.
		yield return new DataContractJsonSerializerAdapter();

		// Add more serializers here if needed.
	}

	[Test]
	public void SerializeToString_AndDeserializeFromString_ThenResultEqualsInput(
		[ValueSource(nameof(GetSerializers))] ITextSerializer serializer)
	{
		// Arrange.
		var input = new TSerializable();

		// Act.
		var serialized = serializer.SerializeToString(input);
		var deserialized = serializer.Deserialize<TSerializable>(serialized);

		TestContext.WriteLine(serialized);

		// Assert.
		deserialized.Should().BeEquivalentTo(input);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}
}