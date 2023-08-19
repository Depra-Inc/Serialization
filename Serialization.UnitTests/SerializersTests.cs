// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using System.Threading.Tasks;
using Depra.Serialization.Extensions;
using Depra.Serialization.UnitTests.Helpers;

namespace Depra.Serialization.UnitTests;

[TestFixture(TestOf = typeof(ISerializer))]
internal sealed class SerializersTests
{
	[Test]
	public void WhenSerializeClassToBytes_AndInputIsNotEmpty_ThenSerializedBytesIsNotNullOrEmpty(
		[ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
		ISerializer serializer)
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var inputClassInstance = new SerializableClass(randomId);

		// Act.
		var inputClassAsBytes = serializer.Serialize(inputClassInstance);

		// Assert.
		inputClassAsBytes.Should().NotBeNullOrEmpty();

		// Debug.
		TestContext.WriteLine($"{nameof(inputClassInstance)} : {inputClassInstance}\n" +
		                      $"{nameof(inputClassAsBytes)} : {inputClassAsBytes.Flatten()}");
	}

	[Test]
	public void WhenSerializeStructToBytes_AndInputIsNotEmpty_ThenSerializedBytesIsNotNullOrEmpty(
		[ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
		ISerializer serializer)
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var inputStructInstance = new SerializableStruct(randomId);

		// Act.
		var inputStructInstanceAsBytes = serializer.Serialize(inputStructInstance);

		// Assert.
		inputStructInstanceAsBytes.Should().NotBeNullOrEmpty();

		// Debug.
		TestContext.WriteLine($"{nameof(inputStructInstance)} : {inputStructInstance}\n" +
		                      $"{nameof(inputStructInstanceAsBytes)} : {inputStructInstanceAsBytes.Flatten()}");
	}

	[Test]
	public void WhenSerializeRecordToBytes_AndInputIsNotEmpty_ThenSerializedBytesIsNotNullOrEmpty(
		[ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
		ISerializer serializer)
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var inputRecordInstance = new SerializableRecord(randomId);

		// Act.
		var inputRecordInstanceAsBytes = serializer.Serialize(inputRecordInstance);

		// Assert.
		inputRecordInstanceAsBytes.Should().NotBeNullOrEmpty();

		// Debug.
		TestContext.WriteLine($"{nameof(inputRecordInstance)} : {inputRecordInstance}\n" +
		                      $"{nameof(inputRecordInstanceAsBytes)} : {inputRecordInstanceAsBytes.Flatten()}");
	}

	[Test]
	public void WhenSerializeClassToString_AndDeserializeFromString_ThenDeserializedClassEqualsInput(
		[ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
		ISerializer serializer)
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var inputClassInstance = new SerializableClass(randomId);

		// Act.
		var inputClassInstanceAsString = serializer.SerializeToString(inputClassInstance);
		var deserializedClassInstance = serializer.Deserialize<SerializableClass>(inputClassInstanceAsString);

		TestContext.WriteLine(inputClassInstanceAsString);

		// Assert.
		deserializedClassInstance.Should().BeEquivalentTo(inputClassInstance);

		// Debug.
		TestContext.WriteLine($"{nameof(inputClassInstance)} : {inputClassInstance}\n" +
		                      $"{nameof(deserializedClassInstance)} : {deserializedClassInstance}");
	}

	[Test]
	public void WhenSerializeStructToString_AndDeserializeFromString_ThenDeserializedStructEqualsInput(
		[ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
		ISerializer serializer)
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var inputClassInstance = new SerializableStruct(randomId);

		// Act.
		var inputClassInstanceAsString = serializer.SerializeToString(inputClassInstance);
		var deserializedClassInstance = serializer.Deserialize<SerializableStruct>(inputClassInstanceAsString);

		// Assert.
		deserializedClassInstance.Should().BeEquivalentTo(inputClassInstance);

		// Debug.
		TestContext.WriteLine($"{nameof(inputClassInstance)} : {inputClassInstance}\n" +
		                      $"{nameof(deserializedClassInstance)} : {deserializedClassInstance}");
	}

	[Test]
	public void WhenSerializeRecordToString_AndDeserializeFromString_ThenDeserializedRecordEqualsInput(
		[ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
		ISerializer serializer)
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var inputClassInstance = new SerializableRecord(randomId);

		// Act.
		var inputClassInstanceAsString = serializer.SerializeToString(inputClassInstance);
		var deserializedClassInstance = serializer.Deserialize<SerializableRecord>(inputClassInstanceAsString);

		// Assert.
		deserializedClassInstance.Should().BeEquivalentTo(inputClassInstance);

		// Debug.
		TestContext.WriteLine($"{nameof(inputClassInstance)} : {inputClassInstance}\n" +
		                      $"{nameof(deserializedClassInstance)} : {deserializedClassInstance}");
	}


	[Test]
	public void WhenSerializeClassToStream_AndDeserializeToInputType_ThenDeserializedClassEqualsInput(
		[ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
		ISerializer serializer)
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var inputClassInstance = new SerializableClass(randomId);

		// Act.
		SerializableClass deserializedClassInstance;
		using (var stream = new MemoryStream())
		{
			serializer.Serialize(stream, inputClassInstance);
			deserializedClassInstance = serializer.Deserialize<SerializableClass>(stream);
		}

		// Assert.
		deserializedClassInstance.Should().BeEquivalentTo(inputClassInstance);

		// Debug.
		TestContext.WriteLine($"{nameof(inputClassInstance)} : {inputClassInstance}\n" +
		                      $"{nameof(deserializedClassInstance)} : {deserializedClassInstance}");
	}

	[Test]
	public void WhenSerializeStructToStream_AndDeserializeToInputType_ThenDeserializedStructEqualsInput(
		[ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
		ISerializer serializer)
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var inputStructInstance = new SerializableStruct(randomId);

		// Act.
		SerializableStruct deserializedStructInstance;
		using (var stream = new MemoryStream())
		{
			serializer.Serialize(stream, inputStructInstance);
			deserializedStructInstance = serializer.Deserialize<SerializableStruct>(stream);
		}

		// Assert.
		deserializedStructInstance.Should().BeEquivalentTo(inputStructInstance);

		// Debug.
		TestContext.WriteLine($"{nameof(inputStructInstance)} : {inputStructInstance}\n" +
		                      $"{nameof(deserializedStructInstance)} : {deserializedStructInstance}");
	}

	[Test]
	public void WhenSerializeRecordToStream_AndDeserializeToInputType_ThenDeserializedRecordEqualsInput(
		[ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
		ISerializer serializer)
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var inputRecordInstance = new SerializableRecord(randomId);

		// Act.
		SerializableRecord deserializedRecordInstance;
		using (var stream = new MemoryStream())
		{
			serializer.Serialize(stream, inputRecordInstance);
			deserializedRecordInstance = serializer.Deserialize<SerializableRecord>(stream);
		}

		// Assert.
		deserializedRecordInstance.Should().BeEquivalentTo(inputRecordInstance);

		// Debug.
		TestContext.WriteLine($"{nameof(inputRecordInstance)} : {inputRecordInstance}\n" +
		                      $"{nameof(deserializedRecordInstance)} : {deserializedRecordInstance}");
	}

	[Test]
	public async Task WhenSerializeClassToStreamAsync_AndDeserializeAsyncToInputType_ThenDeserializedClassEqualsInput(
		[ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
		ISerializer serializer)
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var inputClassInstance = new SerializableClass(randomId);

		// Act.
		SerializableClass deserializedClassInstance;
		await using (var stream = new MemoryStream())
		{
			await serializer.SerializeAsync(stream, inputClassInstance);
			deserializedClassInstance = await serializer.DeserializeAsync<SerializableClass>(stream);
		}

		// Assert.
		deserializedClassInstance.Should().BeEquivalentTo(inputClassInstance);

		// Debug.
		TestContext.WriteLine($"{nameof(inputClassInstance)} : {inputClassInstance}\n" +
		                      $"{nameof(deserializedClassInstance)} : {deserializedClassInstance}");
	}

	[Test]
	public async Task WhenSerializeStructToStreamAsync_AndDeserializeAsyncToInputType_ThenDeserializedStructEqualsInput(
		[ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
		ISerializer serializer)
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var inputStructInstance = new SerializableStruct(randomId);

		// Act.
		SerializableStruct deserializedStructInstance;
		await using (var stream = new MemoryStream())
		{
			await serializer.SerializeAsync(stream, inputStructInstance);
			deserializedStructInstance = await serializer.DeserializeAsync<SerializableStruct>(stream);
		}

		// Assert.
		deserializedStructInstance.Should().BeEquivalentTo(inputStructInstance);

		// Debug.
		TestContext.WriteLine($"{nameof(inputStructInstance)} : {inputStructInstance}\n" +
		                      $"{nameof(deserializedStructInstance)} : {deserializedStructInstance}");
	}

	[Test]
	public async Task WhenSerializeRecordToStreamAsync_AndDeserializeAsyncToInputType_ThenDeserializedRecordEqualsInput(
		[ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
		ISerializer serializer)
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var inputRecordInstance = new SerializableRecord(randomId);

		// Act.
		SerializableRecord deserializedRecordInstance;
		await using (var stream = new MemoryStream())
		{
			await serializer.SerializeAsync(stream, inputRecordInstance);
			deserializedRecordInstance = await serializer.DeserializeAsync<SerializableRecord>(stream);
		}

		// Assert.
		deserializedRecordInstance.Should().BeEquivalentTo(inputRecordInstance);

		// Debug.
		TestContext.WriteLine($"{nameof(inputRecordInstance)} : {inputRecordInstance}\n" +
		                      $"{nameof(deserializedRecordInstance)} : {deserializedRecordInstance}");
	}
}