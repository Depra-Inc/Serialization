using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Depra.Serialization.Binary;
using Depra.Serialization.Extensions;
using Depra.Serialization.Interfaces;
using Depra.Serialization.Json;
using Depra.Serialization.Json.Microsoft;
using Depra.Serialization.Json.Newtonsoft;
using Depra.Serialization.Xml;

namespace Depra.Serialization.Benchmarks;

public class SerializationExtensionsBenchmarks
{
	public static IEnumerable<RawAndStreamSerializer> Serializers()
	{
		// Binary.
		yield return new RawAndStreamSerializer(new BinarySerializer());

		// XML.
		yield return new RawAndStreamSerializer(new StandardXmlSerializer());
		yield return new RawAndStreamSerializer(new DataContractXmlSerializer());

		// Json.
		yield return new RawAndStreamSerializer(new DataContractJsonSerializerAdapter());
		yield return new RawAndStreamSerializer(new NewtonsoftJsonSerializer());
		yield return new RawAndStreamSerializer(new MicrosoftJsonSerializer());

		// Add more serializers here if needed.
	}

	private SerializableClass _serializableClass;
	private SerializableStruct _serializableStruct;
	private SerializableRecord _serializableRecord;

	[ParamsSource(nameof(Serializers))]
	public RawAndStreamSerializer Serializer { get; set; }

	[GlobalSetup]
	public void Setup()
	{
		_serializableClass = new SerializableClass();
		_serializableStruct = new SerializableStruct();
		_serializableRecord = new SerializableRecord();
	}

	[Benchmark, BenchmarkCategory(Category.CLONE_CATEGORY_NAME)]
	public object CloneClass() => Serializer.Clone(_serializableClass, SerializableClass.Type);

	[Benchmark, BenchmarkCategory(Category.CLONE_CATEGORY_NAME)]
	public object CloneStruct() => Serializer.Clone(_serializableStruct, SerializableStruct.Type);

	[Benchmark, BenchmarkCategory(Category.CLONE_CATEGORY_NAME)]
	public object CloneRecord() => Serializer.Clone(_serializableRecord, SerializableRecord.Type);

	[Benchmark, BenchmarkCategory(Category.CLONE_CATEGORY_NAME)]
	public SerializableClass CloneClass_Generic() => Serializer.Clone(_serializableClass);

	[Benchmark, BenchmarkCategory(Category.CLONE_CATEGORY_NAME)]
	public SerializableStruct CloneStruct_Generic() => Serializer.Clone(_serializableStruct);

	[Benchmark, BenchmarkCategory(Category.CLONE_CATEGORY_NAME)]
	public SerializableRecord CloneRecord_Generic() => Serializer.Clone(_serializableRecord);

	public sealed class RawAndStreamSerializer : IRawSerializer, IStreamSerializer
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

		public ValueTask<TOut> DeserializeAsync<TOut>(
			Stream inputStream,
			CancellationToken cancellationToken = default) =>
			((IStreamSerializer)_serializer).DeserializeAsync<TOut>(inputStream, cancellationToken);

		public ValueTask<object> DeserializeAsync(
			Stream inputStream,
			Type outputType,
			CancellationToken cancellationToken = default) =>
			((IStreamSerializer)_serializer).DeserializeAsync(inputStream, outputType, cancellationToken);

	}
}