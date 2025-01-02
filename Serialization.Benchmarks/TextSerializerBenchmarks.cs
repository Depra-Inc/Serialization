// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Depra.Serialization.Benchmarks.Stubs;
using Depra.Serialization.Binary;
using Depra.Serialization.Json;
using Depra.Serialization.Json.Microsoft;
using Depra.Serialization.Json.Newtonsoft;
using Depra.Serialization.Xml;

namespace Depra.Serialization.Benchmarks
{
	[CategoriesColumn, GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
	public class TextSerializerBenchmarks
	{
		public static IEnumerable<ITextSerializer> Serializers()
		{
			// Binary.
			yield return new BinarySerializer();

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
		public ITextSerializer Serializer { get; set; }

		[GlobalSetup]
		public void Setup()
		{
			_serializableClass = new SerializableClass();
			_serializableStruct = new SerializableStruct();
			_serializableRecord = new SerializableRecord();
		}

		[Benchmark, BenchmarkCategory(Category.SERIALIZE_TO_STRING_CATEGORY_NAME)]
		public string SerializeClass_ToString() =>
			Serializer.SerializeToString(_serializableClass, SerializableClass.Type);

		[Benchmark, BenchmarkCategory(Category.SERIALIZE_TO_STRING_CATEGORY_NAME)]
		public string SerializeStruct_ToString() =>
			Serializer.SerializeToString(_serializableStruct, SerializableStruct.Type);

		[Benchmark, BenchmarkCategory(Category.SERIALIZE_TO_STRING_CATEGORY_NAME)]
		public string SerializeRecord_ToString() =>
			Serializer.SerializeToString(_serializableRecord, SerializableRecord.Type);

		[Benchmark, BenchmarkCategory(Category.SERIALIZE_TO_STRING_CATEGORY_NAME)]
		public string SerializeClass_ToString_Generic() => Serializer.SerializeToString(_serializableClass);

		[Benchmark, BenchmarkCategory(Category.SERIALIZE_TO_STRING_CATEGORY_NAME)]
		public string SerializeStruct_ToString_Generic() => Serializer.SerializeToString(_serializableStruct);

		[Benchmark, BenchmarkCategory(Category.SERIALIZE_TO_STRING_CATEGORY_NAME)]
		public string SerializeRecord_ToString_Generic() => Serializer.SerializeToString(_serializableRecord);
	}
}