// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using System.Text.Json;
using BenchmarkDotNet.Attributes;
using Depra.Serialization.Json.Microsoft.Benchmarks.Stubs;

namespace Depra.Serialization.Json.Microsoft.Benchmarks;

public class MicrosoftJsonSerializerBenchmarks
{
	private Type _typeCache = null!;
	private string _serializableClassAsJson = null!;
	private SerializableClass _serializableClass = null!;

	[GlobalSetup]
	public void Setup()
	{
		_typeCache = typeof(SerializableClass);
		_serializableClass = new SerializableClass(nameof(SerializableClass));
		_serializableClassAsJson = JsonSerializer.Serialize(_serializableClass);
	}

	[Benchmark]
	public string SerializeUsingType() => JsonSerializer.Serialize(_serializableClass, typeof(SerializableClass));

	[Benchmark]
	public string SerializeUsingCachedType() => JsonSerializer.Serialize(_serializableClass, _typeCache);

	[Benchmark]
	public string SerializeUsingGeneric() => JsonSerializer.Serialize(_serializableClass);

	[Benchmark]
	public SerializableClass DeserializeUsingType() =>
		(SerializableClass)JsonSerializer.Deserialize(_serializableClassAsJson, typeof(SerializableClass))!;

	[Benchmark]
	public SerializableClass DeserializeUsingCachedType() =>
		(SerializableClass)JsonSerializer.Deserialize(_serializableClassAsJson, _typeCache)!;

	[Benchmark]
	public SerializableClass DeserializeUsingGeneric() =>
		JsonSerializer.Deserialize<SerializableClass>(_serializableClassAsJson)!;
}