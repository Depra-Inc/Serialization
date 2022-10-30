using BenchmarkDotNet.Attributes;
using Depra.Serialization.Json.Newtonsoft.Benchmarks.SerializableTypes;
using Newtonsoft.Json;

namespace Depra.Serialization.Json.Newtonsoft.Benchmarks;

public class NewtonsoftJsonSerializerBenchmarks
{
    private Type _typeCache = null!;
    private string _testObjectAsJson = null!;
    private TestSerializableClass _testClass = null!;

    [GlobalSetup]
    public void Setup()
    {
        _testClass = new TestSerializableClass(nameof(TestSerializableClass));
        _typeCache = typeof(TestSerializableClass);
        _testObjectAsJson = JsonConvert.SerializeObject(_testClass);
    }

    [Benchmark]
    public string SerializeUsingGeneric() => JsonConvert.SerializeObject(_testClass);

    [Benchmark]
    public TestSerializableClass DeserializeUsingType() =>
        (TestSerializableClass) JsonConvert.DeserializeObject(_testObjectAsJson, typeof(TestSerializableClass))!;

    [Benchmark]
    public TestSerializableClass DeserializeUsingCachedType() =>
        (TestSerializableClass) JsonConvert.DeserializeObject(_testObjectAsJson, _typeCache)!;

    [Benchmark]
    public TestSerializableClass DeserializeUsingGeneric() =>
        JsonConvert.DeserializeObject<TestSerializableClass>(_testObjectAsJson)!;
}