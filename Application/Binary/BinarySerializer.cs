// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using Depra.Serialization.Application.Serializers;

namespace Depra.Serialization.Application.Binary
{
    [Obsolete]
    public class BinarySerializer : ISerializer, IAsyncSerializer
    {
        private readonly BinaryFormatter _binaryFormatter;

        public void Serialize(object obj, Stream stream, Type objectType) => _binaryFormatter.Serialize(stream, obj);

        public object Deserialize(Stream stream, Type objType) => _binaryFormatter.Deserialize(stream);

        public async Task SerializeAsync(object obj, Stream stream, CancellationToken cancellationToken)
        {
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                _binaryFormatter.Serialize(memoryStream, obj);
            }
        }

        public async Task<object> DeserializeAsync(Stream stream, CancellationToken cancellationToken)
        {
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                var deserializedObject = _binaryFormatter.Deserialize(memoryStream);
                return deserializedObject;
            }
        }

        public BinarySerializer() => _binaryFormatter = new BinaryFormatter();

        public override string ToString() => nameof(BinarySerializer);
    }
}