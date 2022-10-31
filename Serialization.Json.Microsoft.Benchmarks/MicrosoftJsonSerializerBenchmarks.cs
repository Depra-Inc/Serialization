// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json;
using BenchmarkDotNet.Attributes;
using Depra.Serialization.Json.Microsoft.Benchmarks.SerializableTypes;

namespace Depra.Serialization.Json.Microsoft.Benchmarks;

public class MicrosoftJsonSerializerBenchmarks
{
    private Type _typeCache = null!;
    private string _serializableClassAsJson = null!;
    private SerializableClass _serializableClass = null!;

    [GlobalSetup]
    public void Setup()
    {
        _serializableClass = new SerializableClass(nameof(SerializableClass));
        _typeCache = typeof(SerializableClass);
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
        (SerializableClass) JsonSerializer.Deserialize(_serializableClassAsJson, typeof(SerializableClass))!;

    [Benchmark]
    public SerializableClass DeserializeUsingCachedType() =>
        (SerializableClass) JsonSerializer.Deserialize(_serializableClassAsJson, _typeCache)!;

    [Benchmark]
    public SerializableClass DeserializeUsingGeneric() =>
        JsonSerializer.Deserialize<SerializableClass>(_serializableClassAsJson)!;
}