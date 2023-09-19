using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Json.Newtonsoft.UnitTests;

internal sealed class NewtonsoftJsonTextSerializerTests
{
	private static IEnumerable<Container> GetInput()
	{
		yield return new Container(typeof(SerializableClass), new SerializableClass());
		yield return new Container(typeof(SerializableStruct), new SerializableStruct());
		yield return new Container(typeof(SerializableRecord), new SerializableRecord());
	}

	private ITextSerializer _serializer;

	[SetUp]
	public void Setup() => _serializer = new NewtonsoftJsonSerializer();

	[Test]
	public void SerializeToString_AndDeserializeFromString_ThenResultEqualsInput(
		[ValueSource(nameof(GetInput))] Container input)
	{
		// Arrange & Act.
		var serialized = _serializer.SerializeToString(input.Value, input.Type);
		var deserialized = _serializer.Deserialize(serialized, input.Type);

		// Assert.
		deserialized.Should().BeEquivalentTo(input.Value);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input.Value}\n" +
		                      $"{nameof(serialized)} : {serialized}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}
}