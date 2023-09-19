using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Json.Newtonsoft.UnitTests;

internal sealed class NewtonsoftJsonStreamSerializerTests
{
	private static IEnumerable<Container> GetInput()
	{
		yield return new Container(typeof(SerializableClass), new SerializableClass());
		yield return new Container(typeof(SerializableStruct), new SerializableStruct());
		yield return new Container(typeof(SerializableRecord), new SerializableRecord());
	}
	
	private IStreamSerializer _serializer;

	[SetUp]
	public void Setup() => _serializer = new NewtonsoftJsonSerializer();
	
	[Test]
	public void SerializeToStream_AndDeserializeFromString_ThenResultEqualsInput(
		[ValueSource(nameof(GetInput))] Container input)
	{
		// Arrange.
		using var stream = new MemoryStream();

		// Act.
		_serializer.Serialize(stream, input.Value, input.Type);
		var deserialized = _serializer.Deserialize(stream, input.Type);

		// Assert.
		deserialized.Should().BeEquivalentTo(input.Value);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input.Value}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}

	[Test]
	public async Task SerializeToStreamAsync_AndDeserializeFromStringAsync_ThenResultEqualsInput(
		[ValueSource(nameof(GetInput))] Container input)
	{
		// Arrange.
		await using var stream = new MemoryStream();

		// Act.
		await _serializer.SerializeAsync(stream, input.Value, input.Type);
		var deserialized = await _serializer.DeserializeAsync(stream, input.Type);

		// Assert.
		deserialized.Should().BeEquivalentTo(input.Value);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input.Value}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}
}