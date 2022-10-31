// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Depra.Serialization.Application.Binary;
using Depra.Serialization.Application.Json;
using Depra.Serialization.Application.Xml;
using Depra.Serialization.Benchmarks.SerializableTypes;
using Depra.Serialization.Domain.Extensions;
using Depra.Serialization.Domain.Interfaces;
using Depra.Serialization.Json.Microsoft;
using Depra.Serialization.Json.Newtonsoft;

namespace Depra.Serialization.Benchmarks;

[CategoriesColumn, GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class SerializationBenchmarks
{
    private const string SerializeToBytesCategoryName = "To Bytes";
    private const string SerializeToStringCategoryName = "To String";
    private const string CloneCategoryName = "Clone";

    private SerializableClass _serializableClass;
    private SerializableStruct _serializableStruct;
    private SerializableRecord _serializableRecord;
    
    [ParamsSource(nameof(Serializers))] public ISerializer Serializer { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _serializableClass = new SerializableClass();
        _serializableStruct = new SerializableStruct();
        _serializableRecord = new SerializableRecord();
    }

    [Benchmark, BenchmarkCategory(SerializeToBytesCategoryName)]
    public void SerializeClassToBytes() => Serializer.Serialize(_serializableClass);
    
    [Benchmark, BenchmarkCategory(SerializeToBytesCategoryName)]
    public void SerializeStructToBytes() => Serializer.Serialize(_serializableStruct);
        
    [Benchmark, BenchmarkCategory(SerializeToBytesCategoryName)]
    public void SerializeRecordToBytes() => Serializer.Serialize(_serializableRecord);
    

    [Benchmark, BenchmarkCategory(SerializeToStringCategoryName)]
    public void SerializeClassToString() => Serializer.SerializeToString(_serializableClass);
    
    [Benchmark, BenchmarkCategory(SerializeToStringCategoryName)]
    public void SerializeStructToString() => Serializer.SerializeToString(_serializableStruct);
    
    [Benchmark, BenchmarkCategory(SerializeToStringCategoryName)]
    public void SerializeRecordToString() => Serializer.SerializeToString(_serializableRecord);
    
    
    [Benchmark, BenchmarkCategory(CloneCategoryName)]
    public object CloneClass() => Serializer.Clone(_serializableClass);
        
    [Benchmark, BenchmarkCategory(CloneCategoryName)]
    public void CloneStruct() => Serializer.Clone(_serializableStruct);
        
    [Benchmark, BenchmarkCategory(CloneCategoryName)]
    public void CloneRecord() => Serializer.Clone(_serializableRecord);
    

    public static IEnumerable<ISerializer> Serializers()
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
}