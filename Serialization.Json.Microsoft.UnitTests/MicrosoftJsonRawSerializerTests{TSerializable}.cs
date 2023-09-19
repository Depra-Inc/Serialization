using Depra.Serialization.Extensions;

namespace Depra.Serialization.Json.Microsoft.UnitTests;

[TestFixture(typeof(SerializableClass))]
[TestFixture(typeof(SerializableStruct))]
[TestFixture(typeof(SerializableRecord))]
internal sealed class MicrosoftJsonRawSerializerTests<TSerializable> where TSerializable : new()
{
	private IRawSerializer _serializer;

	[SetUp]
	public void Setup() => _serializer = new MicrosoftJsonSerializer();

	[Test]
	public void SerializeToBytes_ThenResultIsNotNullOrEmpty()
	{
		// Arrange.
		var input = new TSerializable();

		// Act.
		var serialized = _serializer.Serialize(input);

		// Assert.
		serialized.Should().NotBeNullOrEmpty();

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input}\n" +
		                      $"{nameof(serialized)} : {serialized.Flatten()}");
	}
}