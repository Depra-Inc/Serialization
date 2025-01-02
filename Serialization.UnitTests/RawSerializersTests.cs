// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Serialization.Binary;
using Depra.Serialization.Extensions;
using Depra.Serialization.Json;
using Depra.Serialization.Xml;

namespace Depra.Serialization.UnitTests;

internal sealed class RawSerializersTests
{
	private static IEnumerable<Container> GetInput()
	{
		yield return new Container(typeof(SerializableClass), new SerializableClass());
		yield return new Container(typeof(SerializableStruct), new SerializableStruct());
		yield return new Container(typeof(SerializableRecord), new SerializableRecord());
	}

	private static IEnumerable<ISerializer> GetSerializers()
	{
		// Binary.
		yield return new BinarySerializer();

		// XML.
		yield return new StandardXmlSerializer();
		yield return new DataContractXmlSerializer();

		// Json.
		yield return new DataContractJsonSerializerAdapter();

		// Add more serializers here if needed.
	}

	[Test]
	public void SerializeToBytes_AndDeserializeFromBytes_ThenResultEqualsInput(
		[ValueSource(nameof(GetInput))] Container input,
		[ValueSource(nameof(GetSerializers))] ISerializer serializer)
	{
		// Arrange & Act.
		var serialized = serializer.Serialize(input.Value, input.Type);
		var deserialized = serializer.Deserialize(serialized, input.Type);

		// Assert.
		deserialized.Should().BeEquivalentTo(input.Value);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input.Value}\n" +
		                      $"{nameof(serialized)} : {serialized.Flatten()}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}
}