// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using Depra.Serialization.Application.Serializers;

namespace Depra.Serialization.Infrastructure.Serializers.Json
{
    public class DataContractJsonSerializer : ISerializer
    {
        public void Serialize(object obj, Stream stream, Type objType)
        {
            var serializer = CreateSerializer(objType);
            serializer.WriteObject(stream, obj);
        }

        public object Deserialize(Stream stream, Type objType)
        {
            var serializer = CreateSerializer(objType);
            var deserializedObject = serializer.ReadObject(stream);

            return deserializedObject;
        }

        private static System.Runtime.Serialization.Json.DataContractJsonSerializer CreateSerializer(Type objectType) =>
            new System.Runtime.Serialization.Json.DataContractJsonSerializer(objectType);

        public override string ToString() => "DC_JsonSerializer";
    }
}