// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

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
	private static IEnumerable<IRawSerializer> GetSerializers()
	{
		// Binary.
#pragma warning disable CS0612
		yield return new BinarySerializer();
#pragma warning restore CS0612

		// XML.
		yield return new StandardXmlSerializer();
		yield return new DataContractXmlSerializer();

		// Json.
		yield return new DataContractJsonSerializerAdapter();

		// Add more serializers here if needed.
	}

	[Test]
	public void SerializeToBytes_ThenResultIsNotNullOrEmpty(
		[ValueSource(nameof(GetSerializers))] IRawSerializer serializer)
	{
		// Arrange.
		var input = new TSerializable();

		// Act.
		var serialized = serializer.Serialize(input);

		// Assert.
		serialized.Should().NotBeNullOrEmpty();

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input}\n" +
		                      $"{nameof(serialized)} : {serialized.Flatten()}");
	}
}