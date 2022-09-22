// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BenchmarkDotNet.Attributes;
using Depra.Serialization.Application.Binary;
using Depra.Serialization.Application.Extensions;
using Depra.Serialization.Application.Serializers;
using Depra.Serialization.Infrastructure.Newtonsoft.Json;
using Depra.Serialization.Infrastructure.Serializers.Json;
using Depra.Serialization.Infrastructure.Serializers.Xml;
using Newtonsoft.Json;

namespace Depra.Serialization.Benchmarks
{
    [MemoryDiagnoser]
    public class SerializationBenchmarks
    {
        /// <summary>
        /// Must be public to <see cref="XmlSerializer"/>.
        /// </summary>
        [Serializable]
        public class TestSerializableClass
        {
            /// <summary>
            /// Property can be a field.
            /// Cannot be private and internal to <see cref="XmlSerializer"/> and <see cref="NewtonsoftJsonSerializer"/>
            /// </summary>
            public string Id { get; set; }

            /// <summary>
            /// Required for <see cref="XmlSerializer"/>
            /// </summary>
            public TestSerializableClass() { }

            public TestSerializableClass(string id) => Id = id;

            public override string ToString() => Id;
        }

        /// <summary>
        /// Must be public to <see cref="XmlSerializer"/>.
        /// </summary>
        [Serializable]
        public struct TestSerializableStruct
        {
            /// <summary>
            /// Property can be a field.
            /// Cannot be private and internal to <see cref="XmlSerializer"/> and <see cref="NewtonsoftJsonSerializer"/>
            /// </summary>
            public string Id { get; set; }

            public override string ToString() => Id;
        }

        /// <summary>
        /// Must be public to <see cref="XmlSerializer"/>.
        /// </summary>
        [Serializable]
        public record TestSerializableRecord(string Id)
        {
            /// <summary>
            /// Required for <see cref="XmlSerializer"/>
            /// </summary>
            public TestSerializableRecord() : this("") { }
        }

        [ParamsSource(nameof(Serializers))] public ISerializer Serializer { get; set; }
        
        [Benchmark]
        public void SerializeClass() => Serializer.SerializeToBytes(new TestSerializableClass());

        [Benchmark]
        public void SerializeStruct() => Serializer.SerializeToBytes(new TestSerializableStruct());

        [Benchmark]
        public void SerializeRecord() => Serializer.SerializeToBytes(new TestSerializableRecord());

        [Benchmark]
        public object CloneClass() => Serializer.Clone(new TestSerializableClass());
        
        [Benchmark]
        public void CloneStruct() => Serializer.Clone(new TestSerializableStruct());

        [Benchmark]
        public void CloneRecord() => Serializer.Clone(new TestSerializableRecord());

        public static IEnumerable<ISerializer> Serializers()
        {
            // Binary.
            yield return new BinarySerializer();

            // XML.
            yield return new StandardXmlSerializer();
            yield return new DataContractXmlSerializer();

            // Json.
            yield return new DataContractJsonSerializer();
            yield return new NewtonsoftJsonSerializer(JsonSerializer.CreateDefault());

            // Add more serializers here if needed.
        }
    }
}