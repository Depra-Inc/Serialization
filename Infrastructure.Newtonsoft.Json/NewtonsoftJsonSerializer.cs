// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Text;
using Depra.Serialization.Application.Serializers;
using Newtonsoft.Json;

namespace Depra.Serialization.Infrastructure.Newtonsoft.Json
{
    public class NewtonsoftJsonSerializer : ISerializer
    {
        private readonly bool _leaveStreamOpen;
        private readonly JsonSerializer _serializer;

        public void Serialize(object obj, Stream stream, Type objType)
        {
            using (var streamWriter = CreateStreamWriterLeaveOpen(stream, _leaveStreamOpen))
            {
                using (var jsonWriter = new JsonTextWriter(streamWriter))
                {
                    _serializer.Serialize(jsonWriter, obj);
                }
            }
        }

        public object Deserialize(Stream stream, Type objType)
        {
            using (var streamReader = new StreamReader(stream))
            {
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    var deserializedObject = _serializer.Deserialize(jsonReader, objType);
                    return deserializedObject;
                }
            }
        }

        public NewtonsoftJsonSerializer(JsonSerializer serializer, bool leaveStreamOpen = true)
        {
            _serializer = serializer;
            _leaveStreamOpen = leaveStreamOpen;
        }

        public override string ToString() => typeof(JsonSerializer).Namespace;

        private static StreamWriter CreateStreamWriterLeaveOpen(Stream stream, bool leaveOpen)
        {
            const int bufferSize = 1024;
            return new StreamWriter(stream, Encoding.UTF8, bufferSize, leaveOpen);
        }
    }
}