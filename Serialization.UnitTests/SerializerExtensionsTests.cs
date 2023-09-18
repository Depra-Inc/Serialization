// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Depra.Serialization.Binary;
using Depra.Serialization.Extensions;
using Depra.Serialization.Json;
using Depra.Serialization.Xml;

namespace Depra.Serialization.UnitTests;

[TestFixture(typeof(SerializableClass))]
[TestFixture(typeof(SerializableStruct))]
[TestFixture(typeof(SerializableRecord))]
internal sealed class SerializerExtensionsTests<TSerializable> where TSerializable : new()
{
	private static IEnumerable<RawAndStreamSerializer> GetSerializers()
	{
		// Binary.
#pragma warning disable CS0612
		yield return new RawAndStreamSerializer(new BinarySerializer());
#pragma warning restore CS0612

		// XML.
		yield return new RawAndStreamSerializer(new StandardXmlSerializer());
		yield return new RawAndStreamSerializer(new DataContractXmlSerializer());

		// Json.
		yield return new RawAndStreamSerializer(new DataContractJsonSerializerAdapter());

		// Add more serializers here if needed.
	}

	[Test]
	public void Clone_ThenClonedObjectEqualsInput(
		[ValueSource(nameof(GetSerializers))] RawAndStreamSerializer serializer)
	{
		// Arrange.
		var input = new TSerializable();

		// Act.
		var cloned = serializer.Clone(input);

		// Assert.
		cloned.Should().BeEquivalentTo(input);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input}\n" +
		                      $"{nameof(cloned)} : {cloned}");
	}

	internal sealed class RawAndStreamSerializer : IRawSerializer, IStreamSerializer
	{
		private readonly object _serializer;

		public RawAndStreamSerializer(object serializer) => _serializer = serializer;

		public byte[] Serialize<TIn>(TIn input) =>
			((IRawSerializer)_serializer).Serialize(input);

		public byte[] Serialize(object input, Type inputType) =>
			((IRawSerializer)_serializer).Serialize(input, inputType);

		public TOut Deserialize<TOut>(byte[] input) =>
			((IRawSerializer)_serializer).Deserialize<TOut>(input);

		public object Deserialize(byte[] input, Type outputType) =>
			((IRawSerializer)_serializer).Deserialize(input, outputType);

		public void Serialize<TIn>(Stream outputStream, TIn input) =>
			((IStreamSerializer)_serializer).Serialize(outputStream, input);

		public void Serialize(Stream outputStream, object input, Type inputType) =>
			((IStreamSerializer)_serializer).Serialize(outputStream, input, inputType);

		public Task SerializeAsync<TIn>(Stream outputStream, TIn input) =>
			((IStreamSerializer)_serializer).SerializeAsync(outputStream, input);

		public Task SerializeAsync(Stream outputStream, object input, Type inputType) =>
			((IStreamSerializer)_serializer).SerializeAsync(outputStream, input, inputType);

		public TOut Deserialize<TOut>(Stream inputStream) =>
			((IStreamSerializer)_serializer).Deserialize<TOut>(inputStream);

		public object Deserialize(Stream inputStream, Type outputType) =>
			((IStreamSerializer)_serializer).Deserialize(inputStream, outputType);

		public ValueTask<TOut> DeserializeAsync<TOut>(Stream inputStream,
			CancellationToken cancellationToken = default) =>
			((IStreamSerializer)_serializer).DeserializeAsync<TOut>(inputStream, cancellationToken);

		public ValueTask<object> DeserializeAsync(Stream inputStream, Type outputType,
			CancellationToken cancellationToken = default) =>
			((IStreamSerializer)_serializer).DeserializeAsync(inputStream, outputType, cancellationToken);

	}
}