// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Threading.Tasks;
using Depra.Serialization.Application.UnitTests.Helpers;
using Depra.Serialization.Application.UnitTests.Types;
using Depra.Serialization.Domain.Interfaces;

namespace Depra.Serialization.Application.UnitTests;

[TestFixture(TestOf = typeof(ISerializer))]
internal class SerializersTests
{
    [Test]
    public void WhenSerializeClassToBytes_AndInputIsNotEmpty_ThenSerializedBytesIsNotNullOrEmpty(
        [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
        ISerializer serializer)
    {
        // Arrange.
        var inputClassInstance = new SerializableClass(RandomIdGenerator.Generate());

        // Act.
        var inputClassAsBytes = serializer.Serialize(inputClassInstance);

        // Assert.
        inputClassAsBytes.Should().NotBeNullOrEmpty();

        ConsoleHelper.PrintResults<SerializableClass>(
            inputClassInstance, nameof(inputClassInstance),
            inputClassAsBytes, nameof(inputClassAsBytes));
    }

    [Test]
    public void WhenSerializeStructToBytes_AndInputIsNotEmpty_ThenSerializedBytesIsNotNullOrEmpty(
        [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
        ISerializer serializer)
    {
        // Arrange.
        var inputStructInstance = new SerializableStruct(RandomIdGenerator.Generate());

        // Act.
        var inputStructInstanceAsBytes = serializer.Serialize(inputStructInstance);

        // Assert.
        inputStructInstanceAsBytes.Should().NotBeNullOrEmpty();

        ConsoleHelper.PrintResults<SerializableStruct>(
            inputStructInstance, nameof(inputStructInstance),
            inputStructInstanceAsBytes, nameof(inputStructInstanceAsBytes));
    }

    [Test]
    public void WhenSerializeRecordToBytes_AndInputIsNotEmpty_ThenSerializedBytesIsNotNullOrEmpty(
        [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
        ISerializer serializer)
    {
        // Arrange.
        var inputRecordInstance = new SerializableRecord(RandomIdGenerator.Generate());

        // Act.
        var inputRecordInstanceAsBytes = serializer.Serialize(inputRecordInstance);

        // Assert.
        inputRecordInstanceAsBytes.Should().NotBeNullOrEmpty();

        ConsoleHelper.PrintResults<SerializableRecord>(
            inputRecordInstance, nameof(inputRecordInstance),
            inputRecordInstanceAsBytes, nameof(inputRecordInstanceAsBytes));
    }

    [Test]
    public void WhenSerializeClassToString_AndDeserializeFromString_ThenDeserializedClassEqualsInput(
        [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
        ISerializer serializer)
    {
        // Arrange.
        var inputClassInstance = new SerializableClass(RandomIdGenerator.Generate());

        // Act.
        var inputClassInstanceAsString = serializer.SerializeToString(inputClassInstance);
        var deserializedClassInstance = serializer.Deserialize<SerializableClass>(inputClassInstanceAsString);

        Console.WriteLine(inputClassInstanceAsString);

        // Assert.
        deserializedClassInstance.Should().BeEquivalentTo(inputClassInstance);

        ConsoleHelper.PrintResults<SerializableClass>(
            inputClassInstance, nameof(inputClassInstance),
            deserializedClassInstance, nameof(deserializedClassInstance));
    }

    [Test]
    public void WhenSerializeStructToString_AndDeserializeFromString_ThenDeserializedStructEqualsInput(
        [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
        ISerializer serializer)
    {
        // Arrange.
        var inputClassInstance = new SerializableStruct(RandomIdGenerator.Generate());

        // Act.
        var inputClassInstanceAsString = serializer.SerializeToString(inputClassInstance);
        var deserializedClassInstance = serializer.Deserialize<SerializableStruct>(inputClassInstanceAsString);

        // Assert.
        deserializedClassInstance.Should().BeEquivalentTo(inputClassInstance);

        ConsoleHelper.PrintResults<SerializableStruct>(
            inputClassInstance, nameof(inputClassInstance),
            deserializedClassInstance, nameof(deserializedClassInstance));
    }

    [Test]
    public void WhenSerializeRecordToString_AndDeserializeFromString_ThenDeserializedRecordEqualsInput(
        [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
        ISerializer serializer)
    {
        // Arrange.
        var inputClassInstance = new SerializableRecord(RandomIdGenerator.Generate());

        // Act.
        var inputClassInstanceAsString = serializer.SerializeToString(inputClassInstance);
        var deserializedClassInstance = serializer.Deserialize<SerializableRecord>(inputClassInstanceAsString);

        // Assert.
        deserializedClassInstance.Should().BeEquivalentTo(inputClassInstance);

        ConsoleHelper.PrintResults<SerializableRecord>(
            inputClassInstance, nameof(inputClassInstance),
            deserializedClassInstance, nameof(deserializedClassInstance));
    }


    [Test]
    public void WhenSerializeClassToStream_AndDeserializeToInputType_ThenDeserializedClassEqualsInput(
        [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
        ISerializer serializer)
    {
        // Arrange.
        var inputClassInstance = new SerializableClass(RandomIdGenerator.Generate());

        // Act.
        SerializableClass deserializedClassInstance;
        using (var stream = new MemoryStream())
        {
            serializer.Serialize(stream, inputClassInstance);
            deserializedClassInstance = serializer.Deserialize<SerializableClass>(stream);
        }

        // Assert.
        deserializedClassInstance.Should().BeEquivalentTo(inputClassInstance);

        ConsoleHelper.PrintResults<SerializableClass>(
            inputClassInstance, nameof(inputClassInstance),
            deserializedClassInstance, nameof(deserializedClassInstance));
    }

    [Test]
    public void WhenSerializeStructToStream_AndDeserializeToInputType_ThenDeserializedStructEqualsInput(
        [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
        ISerializer serializer)
    {
        // Arrange.
        var inputStructInstance = new SerializableStruct(RandomIdGenerator.Generate());

        // Act.
        SerializableStruct deserializedStructInstance;
        using (var stream = new MemoryStream())
        {
            serializer.Serialize(stream, inputStructInstance);
            deserializedStructInstance = serializer.Deserialize<SerializableStruct>(stream);
        }

        // Assert.
        deserializedStructInstance.Should().BeEquivalentTo(inputStructInstance);

        ConsoleHelper.PrintResults<SerializableStruct>(
            inputStructInstance, nameof(inputStructInstance),
            deserializedStructInstance, nameof(deserializedStructInstance));
    }

    [Test]
    public void WhenSerializeRecordToStream_AndDeserializeToInputType_ThenDeserializedRecordEqualsInput(
        [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
        ISerializer serializer)
    {
        // Arrange.
        var inputRecordInstance = new SerializableRecord(RandomIdGenerator.Generate());

        // Act.
        SerializableRecord deserializedRecordInstance;
        using (var stream = new MemoryStream())
        {
            serializer.Serialize(stream, inputRecordInstance);
            deserializedRecordInstance = serializer.Deserialize<SerializableRecord>(stream);
        }

        // Assert.
        deserializedRecordInstance.Should().BeEquivalentTo(inputRecordInstance);

        ConsoleHelper.PrintResults<SerializableRecord>(
            inputRecordInstance, nameof(inputRecordInstance),
            deserializedRecordInstance, nameof(deserializedRecordInstance));
    }

    [Test]
    public async Task WhenSerializeClassToStreamAsync_AndDeserializeAsyncToInputType_ThenDeserializedClassEqualsInput(
        [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
        ISerializer serializer)
    {
        // Arrange.
        var inputClassInstance = new SerializableClass(RandomIdGenerator.Generate());

        // Act.
        SerializableClass deserializedClassInstance;
        await using (var stream = new MemoryStream())
        {
            await serializer.SerializeAsync(stream, inputClassInstance);
            deserializedClassInstance = await serializer.DeserializeAsync<SerializableClass>(stream);
        }

        // Assert.
        deserializedClassInstance.Should().BeEquivalentTo(inputClassInstance);

        ConsoleHelper.PrintResults<SerializableClass>(
            inputClassInstance, nameof(inputClassInstance),
            deserializedClassInstance, nameof(deserializedClassInstance));
    }

    [Test]
    public async Task WhenSerializeStructToStreamAsync_AndDeserializeAsyncToInputType_ThenDeserializedStructEqualsInput(
        [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
        ISerializer serializer)
    {
        // Arrange.
        var inputStructInstance = new SerializableStruct(RandomIdGenerator.Generate());

        // Act.
        SerializableStruct deserializedStructInstance;
        await using (var stream = new MemoryStream())
        {
            await serializer.SerializeAsync(stream, inputStructInstance);
            deserializedStructInstance = await serializer.DeserializeAsync<SerializableStruct>(stream);
        }

        // Assert.
        deserializedStructInstance.Should().BeEquivalentTo(inputStructInstance);

        ConsoleHelper.PrintResults<SerializableStruct>(
            inputStructInstance, nameof(inputStructInstance),
            deserializedStructInstance, nameof(deserializedStructInstance));
    }

    [Test]
    public async Task WhenSerializeRecordToStreamAsync_AndDeserializeAsyncToInputType_ThenDeserializedRecordEqualsInput(
        [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
        ISerializer serializer)
    {
        // Arrange.
        var inputRecordInstance = new SerializableRecord(RandomIdGenerator.Generate());

        // Act.
        SerializableRecord deserializedRecordInstance;
        await using (var stream = new MemoryStream())
        {
            await serializer.SerializeAsync(stream, inputRecordInstance);
            deserializedRecordInstance = await serializer.DeserializeAsync<SerializableRecord>(stream);
        }

        // Assert.
        deserializedRecordInstance.Should().BeEquivalentTo(inputRecordInstance);

        ConsoleHelper.PrintResults<SerializableRecord>(
            inputRecordInstance, nameof(inputRecordInstance),
            deserializedRecordInstance, nameof(deserializedRecordInstance));
    }
}