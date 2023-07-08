// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Depra.Serialization.Application.UnitTests.Helpers;
using Depra.Serialization.Application.UnitTests.Stubs;
using Depra.Serialization.Domain.Extensions;
using Depra.Serialization.Domain.Interfaces;

namespace Depra.Serialization.Application.UnitTests;

[TestFixture(TestOf = typeof(SerializerExtensions))]
internal class SerializerExtensionsTests
{
    [Test]
    public void WhenCloneClass_AndInputIsNotNull_ThenClonedObjectEqualsInput(
        [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
        ISerializer serializer)
    {
        // Arrange.
        var randomId = Guid.NewGuid().ToString();
        var inputClassInstance = new SerializableClass(randomId);

        // Act.
        var clonedClassInstance = serializer.Clone(inputClassInstance);

        // Assert.
        clonedClassInstance.Should().BeEquivalentTo(inputClassInstance);

        ConsoleHelper.PrintResults<SerializableClass>(
            inputClassInstance, nameof(inputClassInstance),
            clonedClassInstance, nameof(clonedClassInstance));
    }

    [Test]
    public void WhenCloneStruct_AndInputIsNotNull_ThenClonedObjectEqualsInput(
        [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
        ISerializer serializer)
    {
        // Arrange.
        var randomId = Guid.NewGuid().ToString();
        var inputStructInstance = new SerializableStruct(randomId);

        // Act.
        var clonedStructInstance = serializer.Clone(inputStructInstance);

        // Assert.
        clonedStructInstance.Should().BeEquivalentTo(inputStructInstance);

        ConsoleHelper.PrintResults<SerializableStruct>(
            inputStructInstance, nameof(inputStructInstance),
            clonedStructInstance, nameof(clonedStructInstance));
    }

    [Test]
    public void WhenCloneRecord_AndInputIsNotNull_ThenClonedObjectEqualsInput(
        [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
        ISerializer serializer)
    {
        // Arrange.
        var randomId = Guid.NewGuid().ToString();
        var inputRecordInstance = new SerializableRecord(randomId);

        // Act.
        var clonedRecordInstance = serializer.Clone(inputRecordInstance);

        // Assert.
        clonedRecordInstance.Should().BeEquivalentTo(inputRecordInstance);

        ConsoleHelper.PrintResults<SerializableRecord>(
            inputRecordInstance, nameof(inputRecordInstance),
            clonedRecordInstance, nameof(clonedRecordInstance));
    }
}