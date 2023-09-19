using Depra.Serialization.Extensions;
using Depra.Serialization.Interfaces;

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