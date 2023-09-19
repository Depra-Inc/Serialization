using Depra.Serialization.Extensions;

namespace Depra.Serialization.Json.Newtonsoft.UnitTests;

[TestFixture(typeof(SerializableClass))]
[TestFixture(typeof(SerializableStruct))]
[TestFixture(typeof(SerializableRecord))]
internal sealed class NewtonsoftJsonRawSerializerTests<TSerializable> where TSerializable : new()
{
	private IRawSerializer _serializer;

	[SetUp]
	public void Setup() => _serializer = new NewtonsoftJsonSerializer();

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