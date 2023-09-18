// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Depra.Serialization.Extensions;

namespace Depra.Serialization.Json.Newtonsoft.UnitTests;

[TestFixture(typeof(SerializableClass))]
[TestFixture(typeof(SerializableStruct))]
[TestFixture(typeof(SerializableRecord))]
internal sealed class NewtonsoftJsonSerializerTests<TSerializable> where TSerializable : new()
{
	private NewtonsoftJsonSerializer _serializer;

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

	[Test]
	public void SerializeToStream_AndDeserializeToInputType_ThenResultEqualsInput()
	{
		// Arrange.
		var input = new TSerializable();

		// Act.
		TSerializable deserialized;
		using (var stream = new MemoryStream())
		{
			_serializer.Serialize(stream, input);
			deserialized = _serializer.Deserialize<TSerializable>(stream);
		}

		// Assert.
		deserialized.Should().BeEquivalentTo(input);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}

	[Test]
	public async Task SerializeToStreamAsync_AndDeserializeAsyncToInputType_ThenResultEqualsInput()
	{
		// Arrange.
		var input = new TSerializable();

		// Act.
		TSerializable deserialized;
		await using (var stream = new MemoryStream())
		{
			await _serializer.SerializeAsync(stream, input);
			deserialized = await _serializer.DeserializeAsync<TSerializable>(stream);
		}

		// Assert.
		deserialized.Should().BeEquivalentTo(input);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}
}