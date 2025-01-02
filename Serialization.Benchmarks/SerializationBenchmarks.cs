// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Depra.Serialization.Benchmarks.Stubs;
using Depra.Serialization.Binary;
using Depra.Serialization.Json;
using Depra.Serialization.Json.Microsoft;
using Depra.Serialization.Json.Newtonsoft;
using Depra.Serialization.Xml;
using static Depra.Serialization.Benchmarks.Category;

namespace Depra.Serialization.Benchmarks
{
	public class CollectionBenchmarks
	{
		private Implementation[] _implementations;

		[GlobalSetup]
		public void Setup()
		{
			_implementations = new Implementation[1000];
			for (var index = 0; index < _implementations.Length; index++)
			{
				_implementations[index] = new Implementation();
			}
		}

		[Benchmark]
		public IEnumerable<IInterface> Cast()
		{
			return _implementations.Cast<IInterface>();
		}

		public interface IInterface { }

		public sealed class Implementation : IInterface { }
	}

	[CategoriesColumn, GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
	public class SerializationBenchmarks
	{
		public static IEnumerable<ISerializer> Serializers()
		{
			// Binary.
			yield return new BinarySerializer();

			// XML.
			//yield return new StandardXmlSerializer();
			//yield return new DataContractXmlSerializer();

			// Json.
			//yield return new DataContractJsonSerializerAdapter();
			//yield return new NewtonsoftJsonSerializer();
			//yield return new MicrosoftJsonSerializer();

			// Add more serializers here if needed.
		}

		private SerializableClass _serializableClass;
		private SerializableStruct _serializableStruct;
		private SerializableRecord _serializableRecord;

		[ParamsSource(nameof(Serializers))]
		public ISerializer Serializer { get; set; }

		[GlobalSetup]
		public void Setup()
		{
			_serializableClass = new SerializableClass();
			_serializableStruct = new SerializableStruct();
			_serializableRecord = new SerializableRecord();
		}

		[Benchmark, BenchmarkCategory(SERIALIZE_TO_BYTES_CATEGORY_NAME)]
		public byte[] SerializeClass_ToBytes() => Serializer.Serialize(_serializableClass, SerializableClass.Type);

		[Benchmark, BenchmarkCategory(SERIALIZE_TO_BYTES_CATEGORY_NAME)]
		public byte[] SerializeStruct_ToBytes() => Serializer.Serialize(_serializableStruct, SerializableStruct.Type);

		[Benchmark, BenchmarkCategory(SERIALIZE_TO_BYTES_CATEGORY_NAME)]
		public byte[] SerializeRecord_ToBytes() => Serializer.Serialize(_serializableRecord, SerializableRecord.Type);

		[Benchmark, BenchmarkCategory(SERIALIZE_TO_BYTES_CATEGORY_NAME)]
		public byte[] SerializeClass_ToBytes_Generic() => Serializer.Serialize(_serializableClass);

		[Benchmark, BenchmarkCategory(SERIALIZE_TO_BYTES_CATEGORY_NAME)]
		public byte[] SerializeStruct_ToBytes_Generic() => Serializer.Serialize(_serializableStruct);

		[Benchmark, BenchmarkCategory(SERIALIZE_TO_BYTES_CATEGORY_NAME)]
		public byte[] SerializeRecord_ToBytes_Generic() => Serializer.Serialize(_serializableRecord);
	}
}