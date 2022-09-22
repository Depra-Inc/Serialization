// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using Depra.Serialization.Application.Binary;
using Depra.Serialization.Application.Serializers;
using Depra.Serialization.Application.UnitTests.Types;
using Depra.Serialization.Infrastructure.Newtonsoft.Json;
using Depra.Serialization.Infrastructure.Serializers.Json;
using Depra.Serialization.Infrastructure.Serializers.Xml;

namespace Depra.Serialization.Application.UnitTests
{
    public static class SerializationTestsFactory
    {
        public static IEnumerable<ISerializer> GetSerializers()
        {
            // Binary.
            yield return new BinarySerializer();

            // XML.
            yield return new StandardXmlSerializer();
            yield return new DataContractXmlSerializer();

            // Json.
            yield return new DataContractJsonSerializer();
            yield return new NewtonsoftJsonSerializer(Newtonsoft.Json.JsonSerializer.CreateDefault());

            // Add more serializers here if needed.
        }

        public static IEnumerable<TestInput> GetInput()
        {
            yield return CreateTestInputString();
            yield return CreateTestInputClass();
            yield return CreateTestInputStruct();
            yield return CreateTestInputRecord();
        }

        private static string GenerateRandomId()
        {
            const int length = 10;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static TestInput CreateTestInputRecord()
        {
            var testStruct = new TestSerializableRecord(GenerateRandomId());
            return new TestInput(testStruct, testStruct.GetType());
        }

        private static TestInput CreateTestInputStruct()
        {
            var testStruct = new TestSerializableStruct { Id = GenerateRandomId() };
            return new TestInput(testStruct, testStruct.GetType());
        }

        private static TestInput CreateTestInputString()
        {
            var randomId = GenerateRandomId();
            return new TestInput(randomId, randomId.GetType());
        }

        private static TestInput CreateTestInputClass()
        {
            var testClass = new TestSerializableClass(GenerateRandomId());
            return new TestInput(testClass, testClass.GetType());
        }
    }
}