using Depra.Serialization.Extensions;
using Depra.Serialization.Interfaces;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;

namespace Depra.Serialization.Json.Newtonsoft.UnitTests;

internal sealed class NewtonsoftJsonRawSerializerTests
{
	private static IEnumerable<Container> GetInput()
	{
		yield return new Container(typeof(SerializableClass), new SerializableClass());
		yield return new Container(typeof(SerializableStruct), new SerializableStruct());
		yield return new Container(typeof(SerializableRecord), new SerializableRecord());
	}

	private IRawSerializer _serializer;

	[SetUp]
	public void Setup() => _serializer = new NewtonsoftJsonSerializer();

	[Test]
	public void SerializeToBytes_ThenResultIsNotNullOrEmpty(
		[ValueSource(nameof(GetInput))] Container input)
	{
		// Arrange & Act.
		var serialized = _serializer.Serialize(input.Value, input.Type);

		// Assert.
		serialized.Should().NotBeNullOrEmpty();

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input.Value}\n" +
		                      $"{nameof(serialized)} : {serialized.Flatten()}");
	}
}