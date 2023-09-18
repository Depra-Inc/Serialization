// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Depra.Serialization.Binary;
using Depra.Serialization.Extensions;
using Depra.Serialization.Json;
using Depra.Serialization.Xml;

namespace Depra.Serialization.UnitTests;

[TestFixture(typeof(SerializableClass))]
[TestFixture(typeof(SerializableStruct))]
[TestFixture(typeof(SerializableRecord))]
internal sealed class GenericSerializersTests<TSerializable> where TSerializable : new()
{
	private static IEnumerable<IGenericSerializer> GetSerializers()
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
		[ValueSource(nameof(GetSerializers))] IGenericSerializer serializer)
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

	[Test]
	public void SerializeToString_AndDeserializeFromString_ThenResultEqualsInput(
		[ValueSource(nameof(GetSerializers))] IGenericSerializer serializer)
	{
		// Arrange.
		var input = new TSerializable();

		// Act.
		var serialized = serializer.SerializeToString(input);
		var deserialized = serializer.Deserialize<TSerializable>(serialized);

		TestContext.WriteLine(serialized);

		// Assert.
		deserialized.Should().BeEquivalentTo(input);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}

	[Test]
	public void SerializeToStream_AndDeserializeFromString_ThenResultEqualsInput(
		[ValueSource(nameof(GetSerializers))] IGenericSerializer serializer)
	{
		// Arrange.
		var input = new TSerializable();

		// Act.
		TSerializable deserialized;
		using (var stream = new MemoryStream())
		{
			serializer.Serialize(stream, input);
			deserialized = serializer.Deserialize<TSerializable>(stream);
		}

		// Assert.
		deserialized.Should().BeEquivalentTo(input);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}

	[Test]
	public async Task SerializeToStreamAsync_AndDeserializeFromStringAsync_ThenResultEqualsInput(
		[ValueSource(nameof(GetSerializers))] IGenericSerializer serializer)
	{
		// Arrange.
		var input = new TSerializable();

		// Act.
		TSerializable deserialized;
		await using (var stream = new MemoryStream())
		{
			await serializer.SerializeAsync(stream, input);
			deserialized = await serializer.DeserializeAsync<TSerializable>(stream);
		}

		// Assert.
		deserialized.Should().BeEquivalentTo(input);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}
}