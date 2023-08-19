// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Depra.Serialization.Extensions;
using Depra.Serialization.Json.Newtonsoft.UnitTests.Stubs;

namespace Depra.Serialization.Json.Newtonsoft.UnitTests;

[TestFixture(TestOf = typeof(NewtonsoftJsonSerializer))]
internal class NewtonsoftJsonSerializerTests
{
	[Test]
	public void WhenSerializeClassToBytes_AndInputIsNotEmpty_ThenSerializedBytesIsNotNullOrEmpty()
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var serializerAdapter = new NewtonsoftJsonSerializer();
		var inputClassInstance = new SerializableClass(randomId);

		// Act.
		var inputClassAsBytes = serializerAdapter.Serialize(inputClassInstance);

		// Assert.
		inputClassAsBytes.Should().NotBeNullOrEmpty();

		// Debug.
		TestContext.WriteLine($"{nameof(inputClassInstance)} : {inputClassInstance}\n" +
		                      $"{nameof(inputClassAsBytes)} : {inputClassAsBytes.Flatten()}");
	}

	[Test]
	public void WhenSerializeStructToBytes_AndInputIsNotEmpty_ThenSerializedBytesIsNotNullOrEmpty()
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var serializerAdapter = new NewtonsoftJsonSerializer();
		var inputStructInstance = new SerializableStruct(randomId);

		// Act.
		var inputStructInstanceAsBytes = serializerAdapter.Serialize(inputStructInstance);

		// Assert.
		inputStructInstanceAsBytes.Should().NotBeNullOrEmpty();

		// Debug.
		TestContext.WriteLine($"{nameof(inputStructInstance)} : {inputStructInstance}\n" +
		                      $"{nameof(inputStructInstanceAsBytes)} : {inputStructInstanceAsBytes.Flatten()}");
	}

	[Test]
	public void WhenSerializeRecordToBytes_AndInputIsNotEmpty_ThenSerializedBytesIsNotNullOrEmpty()
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var serializerAdapter = new NewtonsoftJsonSerializer();
		var inputRecordInstance = new SerializableRecord(randomId);

		// Act.
		var inputRecordInstanceAsBytes = serializerAdapter.Serialize(inputRecordInstance);

		// Assert.
		inputRecordInstanceAsBytes.Should().NotBeNullOrEmpty();

		// Debug.
		TestContext.WriteLine($"{nameof(inputRecordInstance)} : {inputRecordInstance}\n" +
		                      $"{nameof(inputRecordInstanceAsBytes)} : {inputRecordInstanceAsBytes.Flatten()}");
	}

	[Test]
	public void WhenSerializeClassToString_AndDeserializeFromString_ThenDeserializedClassEqualsInput()
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var serializerAdapter = new NewtonsoftJsonSerializer();
		var inputClassInstance = new SerializableClass(randomId);

		// Act.
		var inputClassInstanceAsString = serializerAdapter.SerializeToString(inputClassInstance);
		var deserializedClassInstance = serializerAdapter.Deserialize<SerializableClass>(inputClassInstanceAsString);

		TestContext.WriteLine(inputClassInstanceAsString);

		// Assert.
		deserializedClassInstance.Should().BeEquivalentTo(inputClassInstance);

		// Debug.
		TestContext.WriteLine($"{nameof(inputClassInstance)} : {inputClassInstance}\n" +
		                      $"{nameof(deserializedClassInstance)} : {deserializedClassInstance}");
	}

	[Test]
	public void WhenSerializeStructToString_AndDeserializeFromString_ThenDeserializedStructEqualsInput()
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var serializerAdapter = new NewtonsoftJsonSerializer();
		var inputClassInstance = new SerializableStruct(randomId);

		// Act.
		var inputClassInstanceAsString = serializerAdapter.SerializeToString(inputClassInstance);
		var deserializedClassInstance = serializerAdapter.Deserialize<SerializableStruct>(inputClassInstanceAsString);

		// Assert.
		deserializedClassInstance.Should().BeEquivalentTo(inputClassInstance);

		// Debug.
		TestContext.WriteLine($"{nameof(inputClassInstance)} : {inputClassInstance}\n" +
		                      $"{nameof(deserializedClassInstance)} : {deserializedClassInstance}");
	}

	[Test]
	public void WhenSerializeRecordToString_AndDeserializeFromString_ThenDeserializedRecordEqualsInput()
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var serializerAdapter = new NewtonsoftJsonSerializer();
		var inputClassInstance = new SerializableRecord(randomId);

		// Act.
		var inputClassInstanceAsString = serializerAdapter.SerializeToString(inputClassInstance);
		var deserializedClassInstance = serializerAdapter.Deserialize<SerializableRecord>(inputClassInstanceAsString);

		// Assert.
		deserializedClassInstance.Should().BeEquivalentTo(inputClassInstance);

		// Debug.
		TestContext.WriteLine($"{nameof(inputClassInstance)} : {inputClassInstance}\n" +
		                      $"{nameof(deserializedClassInstance)} : {deserializedClassInstance}");
	}


	[Test]
	public void WhenSerializeClassToStream_AndDeserializeToInputType_ThenDeserializedClassEqualsInput()
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var serializerAdapter = new NewtonsoftJsonSerializer();
		var inputClassInstance = new SerializableClass(randomId);

		// Act.
		SerializableClass deserializedClassInstance;
		using (var stream = new MemoryStream())
		{
			serializerAdapter.Serialize(stream, inputClassInstance);
			deserializedClassInstance = serializerAdapter.Deserialize<SerializableClass>(stream);
		}

		// Assert.
		deserializedClassInstance.Should().BeEquivalentTo(inputClassInstance);

		// Debug.
		TestContext.WriteLine($"{nameof(inputClassInstance)} : {inputClassInstance}\n" +
		                      $"{nameof(deserializedClassInstance)} : {deserializedClassInstance}");
	}

	[Test]
	public void WhenSerializeStructToStream_AndDeserializeToInputType_ThenDeserializedStructEqualsInput()
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var serializerAdapter = new NewtonsoftJsonSerializer();
		var inputStructInstance = new SerializableStruct(randomId);

		// Act.
		SerializableStruct deserializedStructInstance;
		using (var stream = new MemoryStream())
		{
			serializerAdapter.Serialize(stream, inputStructInstance);
			deserializedStructInstance = serializerAdapter.Deserialize<SerializableStruct>(stream);
		}

		// Assert.
		deserializedStructInstance.Should().BeEquivalentTo(inputStructInstance);

		// Debug.
		TestContext.WriteLine($"{nameof(inputStructInstance)} : {inputStructInstance}\n" +
		                      $"{nameof(deserializedStructInstance)} : {deserializedStructInstance}");
	}

	[Test]
	public void WhenSerializeRecordToStream_AndDeserializeToInputType_ThenDeserializedRecordEqualsInput()
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var serializerAdapter = new NewtonsoftJsonSerializer();
		var inputRecordInstance = new SerializableRecord(randomId);

		// Act.
		SerializableRecord deserializedRecordInstance;
		using (var stream = new MemoryStream())
		{
			serializerAdapter.Serialize(stream, inputRecordInstance);
			deserializedRecordInstance = serializerAdapter.Deserialize<SerializableRecord>(stream);
		}

		// Assert.
		deserializedRecordInstance.Should().BeEquivalentTo(inputRecordInstance);

		// Debug.
		TestContext.WriteLine($"{nameof(inputRecordInstance)} : {inputRecordInstance}\n" +
		                      $"{nameof(deserializedRecordInstance)} : {deserializedRecordInstance}");
	}

	[Test]
	public async Task WhenSerializeClassToStreamAsync_AndDeserializeAsyncToInputType_ThenDeserializedClassEqualsInput()
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var serializerAdapter = new NewtonsoftJsonSerializer();
		var inputClassInstance = new SerializableClass(randomId);

		// Act.
		SerializableClass deserializedClassInstance;
		await using (var stream = new MemoryStream())
		{
			await serializerAdapter.SerializeAsync(stream, inputClassInstance);
			deserializedClassInstance = await serializerAdapter.DeserializeAsync<SerializableClass>(stream);
		}

		// Assert.
		deserializedClassInstance.Should().BeEquivalentTo(inputClassInstance);

		// Debug.
		TestContext.WriteLine($"{nameof(inputClassInstance)} : {inputClassInstance}\n" +
		                      $"{nameof(deserializedClassInstance)} : {deserializedClassInstance}");
	}

	[Test]
	public async Task WhenSerializeStructToStreamAsync_AndDeserializeAsyncToInputType_ThenDeserializedStructEqualsInput()
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var serializerAdapter = new NewtonsoftJsonSerializer();
		var inputStructInstance = new SerializableStruct(randomId);

		// Act.
		SerializableStruct deserializedStructInstance;
		await using (var stream = new MemoryStream())
		{
			await serializerAdapter.SerializeAsync(stream, inputStructInstance);
			deserializedStructInstance = await serializerAdapter.DeserializeAsync<SerializableStruct>(stream);
		}

		// Assert.
		deserializedStructInstance.Should().BeEquivalentTo(inputStructInstance);

		// Debug.
		TestContext.WriteLine($"{nameof(inputStructInstance)} : {inputStructInstance}\n" +
		                      $"{nameof(deserializedStructInstance)} : {deserializedStructInstance}");
	}

	[Test]
	public async Task WhenSerializeRecordToStreamAsync_AndDeserializeAsyncToInputType_ThenDeserializedRecordEqualsInput()
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var serializerAdapter = new NewtonsoftJsonSerializer();
		var inputRecordInstance = new SerializableRecord(randomId);

		// Act.
		SerializableRecord deserializedRecordInstance;
		await using (var stream = new MemoryStream())
		{
			await serializerAdapter.SerializeAsync(stream, inputRecordInstance);
			deserializedRecordInstance = await serializerAdapter.DeserializeAsync<SerializableRecord>(stream);
		}

		// Assert.
		deserializedRecordInstance.Should().BeEquivalentTo(inputRecordInstance);

		// Debug.
		TestContext.WriteLine($"{nameof(inputRecordInstance)} : {inputRecordInstance}\n" +
		                      $"{nameof(deserializedRecordInstance)} : {deserializedRecordInstance}");
	}
}