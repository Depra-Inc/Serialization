// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Serialization.Binary;
using Depra.Serialization.Extensions;
using Depra.Serialization.Json;
using Depra.Serialization.Xml;

namespace Depra.Serialization.UnitTests;

[TestFixture(typeof(SerializableClass))]
[TestFixture(typeof(SerializableStruct))]
[TestFixture(typeof(SerializableRecord))]
internal sealed class RawSerializersTests<TSerializable> where TSerializable : new()
{
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
		[ValueSource(nameof(GetSerializers))] ISerializer serializer)
	{
		// Arrange.
		var input = new TSerializable();

		// Act.
		var serialized = serializer.Serialize(input);
		var deserialized = serializer.Deserialize<TSerializable>(serialized);

		// Assert.
		deserialized.Should().BeEquivalentTo(input);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input}\n" +
		                      $"{nameof(serialized)} : {serialized.Flatten()}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}
}