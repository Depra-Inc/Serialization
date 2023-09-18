using System.IO;
using System.Threading.Tasks;
using Depra.Serialization.Binary;
using Depra.Serialization.Json;
using Depra.Serialization.Xml;

namespace Depra.Serialization.UnitTests;

internal sealed class StreamSerializersTests
{
	private static IEnumerable<Container> GetInput()
	{
		yield return new Container(typeof(SerializableClass), new SerializableClass(Guid.NewGuid().ToString()));
		yield return new Container(typeof(SerializableStruct), new SerializableStruct(Guid.NewGuid().ToString()));
		yield return new Container(typeof(SerializableRecord), new SerializableRecord(Guid.NewGuid().ToString()));
	}

	private static IEnumerable<IRawSerializer> GetSerializers()
	{
		// Binary.
		yield return new BinarySerializer();

		// XML.
		yield return new StandardXmlSerializer();
		yield return new DataContractXmlSerializer();

		// Json.
		yield return new DataContractJsonSerializerAdapter();

		// Add more serializers here if needed.
	}

	[Test]
	public void SerializeToStream_AndDeserializeFromString_ThenResultEqualsInput(
		[ValueSource(nameof(GetInput))] Container input,
		[ValueSource(nameof(GetSerializers))] IStreamSerializer serializer)
	{
		// Arrange.
		using var stream = new MemoryStream();

		// Act.
		serializer.Serialize(stream, input.Value, input.Type);
		var deserialized = serializer.Deserialize(stream, input.Type);

		// Assert.
		deserialized.Should().BeEquivalentTo(input.Value);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input.Value}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}

	[Test]
	public async Task SerializeToStreamAsync_AndDeserializeFromStringAsync_ThenResultEqualsInput(
		[ValueSource(nameof(GetInput))] Container input,
		[ValueSource(nameof(GetSerializers))] IStreamSerializer serializer)
	{
		// Arrange.
		await using var stream = new MemoryStream();

		// Act.
		await serializer.SerializeAsync(stream, input.Value, input.Type);
		var deserialized = await serializer.DeserializeAsync(stream, input.Type);

		// Assert.
		deserialized.Should().BeEquivalentTo(input.Value);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input.Value}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}

}