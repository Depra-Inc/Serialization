// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Depra.Serialization.Benchmarks.SerializableTypes;
using Depra.Serialization.Binary;
using Depra.Serialization.Extensions;
using Depra.Serialization.Interfaces;
using Depra.Serialization.Json;
using Depra.Serialization.Json.Microsoft;
using Depra.Serialization.Json.Newtonsoft;
using Depra.Serialization.Xml;

namespace Depra.Serialization.Benchmarks;

[CategoriesColumn, GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class SerializationBenchmarks
{
	private const string CLONE_CATEGORY_NAME = "Clone";
	private const string SERIALIZE_TO_BYTES_CATEGORY_NAME = "To Bytes";
	private const string SERIALIZE_TO_STRING_CATEGORY_NAME = "To String";

	private static readonly Type SERIALIZABLE_CLASS_TYPE = typeof(SerializableClass);
	private static readonly Type SERIALIZABLE_STRUCT_TYPE = typeof(SerializableStruct);
	private static readonly Type SERIALIZABLE_RECORD_TYPE = typeof(SerializableRecord);

	public static IEnumerable<ISerializer> Serializers()
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
		yield return new NewtonsoftJsonSerializer();
		yield return new MicrosoftJsonSerializer();

		// Add more serializers here if needed.
	}

	public static IEnumerable<IGenericSerializer> GenericSerializers()
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
		yield return new NewtonsoftJsonSerializer();
		yield return new MicrosoftJsonSerializer();

		// Add more serializers here if needed.
	}

	private SerializableClass _serializableClass;
	private SerializableStruct _serializableStruct;
	private SerializableRecord _serializableRecord;

	[ParamsSource(nameof(Serializers))]
	public ISerializer Serializer { get; set; }

	[ParamsSource(nameof(GenericSerializers))]
	public IGenericSerializer GenericSerializer { get; set; }

	[GlobalSetup]
	public void Setup()
	{
		_serializableClass = new SerializableClass();
		_serializableStruct = new SerializableStruct();
		_serializableRecord = new SerializableRecord();
	}

	[Benchmark, BenchmarkCategory(SERIALIZE_TO_BYTES_CATEGORY_NAME)]
	public byte[] SerializeClass_ToBytes() => Serializer.Serialize(_serializableClass, SERIALIZABLE_CLASS_TYPE);

	[Benchmark, BenchmarkCategory(SERIALIZE_TO_BYTES_CATEGORY_NAME)]
	public byte[] SerializeStruct_ToBytes() => Serializer.Serialize(_serializableStruct, SERIALIZABLE_STRUCT_TYPE);

	[Benchmark, BenchmarkCategory(SERIALIZE_TO_BYTES_CATEGORY_NAME)]
	public byte[] SerializeRecord_ToBytes() => Serializer.Serialize(_serializableRecord, SERIALIZABLE_RECORD_TYPE);


	[Benchmark, BenchmarkCategory(SERIALIZE_TO_BYTES_CATEGORY_NAME)]
	public byte[] SerializeClass_ToBytes_Generic() => GenericSerializer.Serialize(_serializableClass);

	[Benchmark, BenchmarkCategory(SERIALIZE_TO_BYTES_CATEGORY_NAME)]
	public byte[] SerializeStruct_ToBytes_Generic() => GenericSerializer.Serialize(_serializableStruct);

	[Benchmark, BenchmarkCategory(SERIALIZE_TO_BYTES_CATEGORY_NAME)]
	public byte[] SerializeRecord_ToBytes_Generic() => GenericSerializer.Serialize(_serializableRecord);

	[Benchmark, BenchmarkCategory(SERIALIZE_TO_STRING_CATEGORY_NAME)]
	public string SerializeClass_ToString() => Serializer.SerializeToString(_serializableClass, SERIALIZABLE_CLASS_TYPE);

	[Benchmark, BenchmarkCategory(SERIALIZE_TO_STRING_CATEGORY_NAME)]
	public string SerializeStruct_ToString() => Serializer.SerializeToString(_serializableStruct, SERIALIZABLE_STRUCT_TYPE);

	[Benchmark, BenchmarkCategory(SERIALIZE_TO_STRING_CATEGORY_NAME)]
	public string SerializeRecord_ToString() => Serializer.SerializeToString(_serializableRecord, SERIALIZABLE_RECORD_TYPE);


	[Benchmark, BenchmarkCategory(SERIALIZE_TO_STRING_CATEGORY_NAME)]
	public string SerializeClass_ToString_Generic() => GenericSerializer.SerializeToString(_serializableClass);

	[Benchmark, BenchmarkCategory(SERIALIZE_TO_STRING_CATEGORY_NAME)]
	public string SerializeStruct_ToString_Generic() => GenericSerializer.SerializeToString(_serializableStruct);

	[Benchmark, BenchmarkCategory(SERIALIZE_TO_STRING_CATEGORY_NAME)]
	public string SerializeRecord_ToString_Generic() => GenericSerializer.SerializeToString(_serializableRecord);

	[Benchmark, BenchmarkCategory(CLONE_CATEGORY_NAME)]
	public object CloneClass() => Serializer.Clone(_serializableClass, SERIALIZABLE_CLASS_TYPE);

	[Benchmark, BenchmarkCategory(CLONE_CATEGORY_NAME)]
	public object CloneStruct() => Serializer.Clone(_serializableStruct, SERIALIZABLE_STRUCT_TYPE);

	[Benchmark, BenchmarkCategory(CLONE_CATEGORY_NAME)]
	public object CloneRecord() => Serializer.Clone(_serializableRecord, SERIALIZABLE_RECORD_TYPE);


	[Benchmark, BenchmarkCategory(CLONE_CATEGORY_NAME)]
	public SerializableClass CloneClass_Generic() => GenericSerializer.Clone(_serializableClass);

	[Benchmark, BenchmarkCategory(CLONE_CATEGORY_NAME)]
	public SerializableStruct CloneStruct_Generic() => GenericSerializer.Clone(_serializableStruct);

	[Benchmark, BenchmarkCategory(CLONE_CATEGORY_NAME)]
	public SerializableRecord CloneRecord_Generic() => GenericSerializer.Clone(_serializableRecord);
}