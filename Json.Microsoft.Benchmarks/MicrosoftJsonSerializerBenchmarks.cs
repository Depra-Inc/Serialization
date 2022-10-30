using System.Text.Json;
using BenchmarkDotNet.Attributes;
using Depra.Serialization.Json.Microsoft.Benchmarks.SerializableTypes;

namespace Depra.Serialization.Json.Microsoft.Benchmarks;

public class MicrosoftJsonSerializerBenchmarks
{
    private Type _typeCache = null!;
    private string _testObjectAsJson = null!;
    private TestSerializableClass _testClass = null!;

    [GlobalSetup]
    public void Setup()
    {
        _testClass = new TestSerializableClass(nameof(TestSerializableClass));
        _typeCache = typeof(TestSerializableClass);
        _testObjectAsJson = JsonSerializer.Serialize(_testClass);
    }

    [Benchmark]
    public string SerializeUsingType() => JsonSerializer.Serialize(_testClass, typeof(TestSerializableClass));

    [Benchmark]
    public string SerializeUsingCachedType() => JsonSerializer.Serialize(_testClass, _typeCache);

    [Benchmark]
    public string SerializeUsingGeneric() => JsonSerializer.Serialize(_testClass);

    [Benchmark]
    public TestSerializableClass DeserializeUsingType() =>
        (TestSerializableClass) JsonSerializer.Deserialize(_testObjectAsJson, typeof(TestSerializableClass))!;

    [Benchmark]
    public TestSerializableClass DeserializeUsingCachedType() =>
        (TestSerializableClass) JsonSerializer.Deserialize(_testObjectAsJson, _typeCache)!;

    [Benchmark]
    public TestSerializableClass DeserializeUsingGeneric() =>
        JsonSerializer.Deserialize<TestSerializableClass>(_testObjectAsJson)!;
}