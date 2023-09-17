// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using Depra.Serialization.Binary;
using Depra.Serialization.Extensions;
using Depra.Serialization.Json;
using Depra.Serialization.Xml;

namespace Depra.Serialization.UnitTests;

[TestFixture(TestOf = typeof(GenericSerializerExtensions))]
internal sealed class GenericSerializerExtensionsTests
{
	private static IEnumerable<ISerializer> GetSerializers()
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
	public void WhenCloneClass_AndInputIsNotNull_ThenClonedObjectEqualsInput(
		[ValueSource(nameof(GetSerializers))] IGenericSerializer serializer)
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var inputClassInstance = new SerializableClass(randomId);

		// Act.
		var clonedClassInstance = serializer.Clone(inputClassInstance);

		// Assert.
		clonedClassInstance.Should().BeEquivalentTo(inputClassInstance);

		TestContext.WriteLine($"{nameof(inputClassInstance)} : {inputClassInstance}\n" +
		                      $"{nameof(clonedClassInstance)} : {clonedClassInstance}");
	}

	[Test]
	public void WhenCloneStruct_AndInputIsNotNull_ThenClonedObjectEqualsInput(
		[ValueSource(nameof(GetSerializers))] IGenericSerializer serializer)
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var inputStructInstance = new SerializableStruct(randomId);

		// Act.
		var clonedStructInstance = serializer.Clone(inputStructInstance);

		// Assert.
		clonedStructInstance.Should().BeEquivalentTo(inputStructInstance);

		TestContext.WriteLine($"{nameof(inputStructInstance)} : {inputStructInstance}\n" +
		                      $"{nameof(clonedStructInstance)} : {clonedStructInstance}");
	}

	[Test]
	public void WhenCloneRecord_AndInputIsNotNull_ThenClonedObjectEqualsInput(
		[ValueSource(nameof(GetSerializers))] IGenericSerializer serializer)
	{
		// Arrange.
		var randomId = Guid.NewGuid().ToString();
		var inputRecordInstance = new SerializableRecord(randomId);

		// Act.
		var clonedRecordInstance = serializer.Clone(inputRecordInstance);

		// Assert.
		clonedRecordInstance.Should().BeEquivalentTo(inputRecordInstance);

		TestContext.WriteLine($"{nameof(inputRecordInstance)} : {inputRecordInstance}\n" +
		                      $"{nameof(clonedRecordInstance)} : {clonedRecordInstance}");
	}
}