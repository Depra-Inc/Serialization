using Depra.Serialization.Extensions;

namespace Depra.Serialization.Json.Microsoft.UnitTests;

[TestFixture(typeof(SerializableClass))]
[TestFixture(typeof(SerializableStruct))]
[TestFixture(typeof(SerializableRecord))]
internal sealed class MicrosoftJsonMemorySerializerTests<TSerializable> where TSerializable : new()
{
	private IMemoryDeserializer _serializer;

	[SetUp]
	public void Setup() => _serializer = new MicrosoftJsonSerializer();
}

[TestFixture(typeof(SerializableClass))]
[TestFixture(typeof(SerializableStruct))]
[TestFixture(typeof(SerializableRecord))]
internal sealed class MicrosoftJsonRawSerializerTests<TSerializable> where TSerializable : new()
{
	private IRawSerializer _serializer;

	[SetUp]
	public void Setup() => _serializer = new MicrosoftJsonSerializer();

	[Test]
	public void SerializeToBytes_AndDeserializeFromBytes_ThenResultEqualsInput()
	{
		// Arrange.
		var input = new TSerializable();

		// Act.
		var serialized = _serializer.Serialize(input);
		var deserialized = _serializer.Deserialize<TSerializable>(serialized);

		// Assert.
		deserialized.Should().BeEquivalentTo(input);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input}\n" +
		                      $"{nameof(serialized)} : {serialized.Flatten()}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}
}