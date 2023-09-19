// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using BenchmarkDotNet.Attributes;
using Depra.Serialization.Json.Newtonsoft.Benchmarks.Stubs;
using Newtonsoft.Json;

namespace Depra.Serialization.Json.Newtonsoft.Benchmarks;

public class NewtonsoftJsonSerializerBenchmarks
{
	private Type _typeCache = null!;
	private string _serializableClassAsJson = null!;
	private SerializableClass _serializableClass = null!;

	[GlobalSetup]
	public void Setup()
	{
		_serializableClass = new SerializableClass(nameof(SerializableClass));
		_typeCache = typeof(SerializableClass);
		_serializableClassAsJson = JsonConvert.SerializeObject(_serializableClass);
	}

	[Benchmark]
	public string SerializeUsingGeneric() => JsonConvert.SerializeObject(_serializableClass);

	[Benchmark]
	public SerializableClass DeserializeUsingType() =>
		(SerializableClass)JsonConvert.DeserializeObject(_serializableClassAsJson, typeof(SerializableClass))!;

	[Benchmark]
	public SerializableClass DeserializeUsingCachedType() =>
		(SerializableClass)JsonConvert.DeserializeObject(_serializableClassAsJson, _typeCache)!;

	[Benchmark]
	public SerializableClass DeserializeUsingGeneric() =>
		JsonConvert.DeserializeObject<SerializableClass>(_serializableClassAsJson)!;
}