using System.IO;
using System.Threading.Tasks;
using Depra.Serialization.Binary;
using Depra.Serialization.Json;
using Depra.Serialization.Xml;

namespace Depra.Serialization.UnitTests;

[TestFixture(typeof(SerializableClass))]
[TestFixture(typeof(SerializableStruct))]
[TestFixture(typeof(SerializableRecord))]
internal sealed class StreamSerializersTests<TSerializable> where TSerializable : new()
{
	private static IEnumerable<IStreamSerializer> GetSerializers()
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
	public void SerializeToStream_AndDeserializeFromString_ThenResultEqualsInput(
		[ValueSource(nameof(GetSerializers))] IStreamSerializer serializer)
	{
		// Arrange.
		var input = new TSerializable();

		// Act.
		TSerializable deserialized;
		using (var stream = new MemoryStream())
		{
			serializer.Serialize(stream, input);
			deserialized = serializer.Deserialize<TSerializable>(stream);
		}

		// Assert.
		deserialized.Should().BeEquivalentTo(input);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}

	[Test]
	public async Task SerializeToStreamAsync_AndDeserializeFromStringAsync_ThenResultEqualsInput(
		[ValueSource(nameof(GetSerializers))] IStreamSerializer serializer)
	{
		// Arrange.
		var input = new TSerializable();

		// Act.
		TSerializable deserialized;
		await using (var stream = new MemoryStream())
		{
			await serializer.SerializeAsync(stream, input);
			deserialized = await serializer.DeserializeAsync<TSerializable>(stream);
		}

		// Assert.
		deserialized.Should().BeEquivalentTo(input);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}
}