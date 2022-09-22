// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Depra.Serialization.Application.Serializers;

namespace Depra.Serialization.Application.Services
{
    public class SerializationService : ISerializationService
    {
        private readonly ISerializer _serializerAdapter;
        private readonly IAsyncSerializer _asyncSerializer;

        public void Serialize<T>(T obj, Stream stream) => _serializerAdapter.Serialize(obj, stream, obj.GetType());

        void ISerializer.Serialize(object obj, Stream stream, Type objType) =>
            _serializerAdapter.Serialize(obj, stream, objType);

        public object Deserialize(Stream stream, Type objType) => 
            _serializerAdapter.Deserialize(stream, objType);

        public Task SerializeAsync(object obj, Stream stream, CancellationToken cancellationToken) =>
            _asyncSerializer.SerializeAsync(obj, stream, cancellationToken);

        public Task<object> DeserializeAsync(Stream stream, CancellationToken cancellationToken) =>
            _asyncSerializer.DeserializeAsync(stream, cancellationToken);

        public SerializationService(ISerializer serializerAdapter, IAsyncSerializer asyncSerializer)
        {
            _serializerAdapter = serializerAdapter;
            _asyncSerializer = asyncSerializer;
        }

        public override string ToString() => _serializerAdapter.ToString();
    }
}