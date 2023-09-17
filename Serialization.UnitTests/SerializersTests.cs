using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Depra.Serialization.Binary;
using Depra.Serialization.Extensions;
using Depra.Serialization.Json;
using Depra.Serialization.Xml;

namespace Depra.Serialization.UnitTests;

[TestFixture(TestOf = typeof(ISerializer))]
internal sealed class SerializersTests
{
	private static IEnumerable<Container> GetInput()
	{
		yield return new Container(typeof(SerializableClass), new SerializableClass(Guid.NewGuid().ToString()));
		yield return new Container(typeof(SerializableStruct), new SerializableStruct(Guid.NewGuid().ToString()));
		yield return new Container(typeof(SerializableRecord), new SerializableRecord(Guid.NewGuid().ToString()));
	}

	private static IEnumerable<ISerializer> GetSerializers()
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
	public void SerializeToBytes_ThenResultIsNotNullOrEmpty(
		[ValueSource(nameof(GetInput))] Container input,
		[ValueSource(nameof(GetSerializers))] ISerializer serializer)
	{
		// Arrange & Act.
		var inputClassAsBytes = serializer.Serialize(input.Value, input.Type);

		// Assert.
		inputClassAsBytes.Should().NotBeNullOrEmpty();

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input.Value}\n" +
		                      $"{nameof(inputClassAsBytes)} : {inputClassAsBytes.Flatten()}");
	}

	[Test]
	public void SerializeToString_AndDeserializeFromString_ThenResultEqualsInput(
		[ValueSource(nameof(GetInput))] Container input,
		[ValueSource(nameof(GetSerializers))] ISerializer serializer)
	{
		// Arrange & Act.
		var serialized = serializer.SerializeToString(input.Value, input.Type);
		var deserialized = serializer.Deserialize(serialized, input.Type);

		// Assert.
		deserialized.Should().BeEquivalentTo(input.Value);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input.Value}\n" +
		                      $"{nameof(serialized)} : {serialized}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}

	[Test]
	public void SerializeToStream_AndDeserializeFromString_ThenResultEqualsInput(
		[ValueSource(nameof(GetInput))] Container input,
		[ValueSource(nameof(GetSerializers))] ISerializer serializer)
	{
		// Arrange.
		object deserialized;

		// Act.
		using (var stream = new MemoryStream())
		{
			serializer.Serialize(stream, input.Value, input.Type);
			deserialized = serializer.Deserialize(stream, input.Type);
		}

		// Assert.
		deserialized.Should().BeEquivalentTo(input.Value);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input.Value}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}

	[Test]
	public async Task SerializeToStreamAsync_AndDeserializeFromStringAsync_ThenResultEqualsInput(
		[ValueSource(nameof(GetInput))] Container input,
		[ValueSource(nameof(GetSerializers))] ISerializer serializer)
	{
		// Arrange.
		object deserialized;

		// Act.
		await using (var stream = new MemoryStream())
		{
			await serializer.SerializeAsync(stream, input.Value, input.Type);
			deserialized = await serializer.DeserializeAsync(stream, input.Type);
		}

		// Assert.
		deserialized.Should().BeEquivalentTo(input.Value);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input.Value}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}

	internal sealed record Container(Type Type, object Value)
	{
		public readonly Type Type = Type;
		public readonly object Value = Value;

		public override string ToString() => Type.Name;
	}
}