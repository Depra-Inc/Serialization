using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Json.Newtonsoft.UnitTests;

[TestFixture(typeof(SerializableClass))]
[TestFixture(typeof(SerializableStruct))]
[TestFixture(typeof(SerializableRecord))]
internal sealed class NewtonsoftJsonTextSerializerTests<TSerializable> where TSerializable : new()
{
	private ITextSerializer _serializer;

	[SetUp]
	public void Setup() => _serializer = new NewtonsoftJsonSerializer();
	
	[Test]
	public void SerializeToString_AndDeserializeFromString_ThenResultEqualsInput()
	{
		// Arrange.
		var input = new TSerializable();

		// Act.
		var serialized = _serializer.SerializeToString(input);
		var deserialized = _serializer.Deserialize<TSerializable>(serialized);

		TestContext.WriteLine(serialized);

		// Assert.
		deserialized.Should().BeEquivalentTo(input);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input}\n" +
		                      $"{nameof(serialized)} : {serialized}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}
}