using Depra.Serialization.Extensions;

namespace Depra.Serialization.Json.Microsoft.UnitTests;

internal sealed class MicrosoftJsonRawSerializerTests
{
	private static IEnumerable<Container> GetInput()
	{
		yield return new Container(typeof(SerializableClass), new SerializableClass());
		yield return new Container(typeof(SerializableStruct), new SerializableStruct());
		yield return new Container(typeof(SerializableRecord), new SerializableRecord());
	}

	private IRawSerializer _serializer;

	[SetUp]
	public void Setup() => _serializer = new MicrosoftJsonSerializer();

	[Test]
	public void SerializeToBytes_AndDeserializeFromBytes_ThenResultEqualsInput(
		[ValueSource(nameof(GetInput))] Container input)
	{
		// Arrange & Act.
		var serialized = _serializer.Serialize(input.Value, input.Type);
		var deserialized = _serializer.Deserialize(serialized, input.Type);

		// Assert.
		deserialized.Should().BeEquivalentTo(input.Value);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input.Value}\n" +
		                      $"{nameof(serialized)} : {serialized.Flatten()}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}
}