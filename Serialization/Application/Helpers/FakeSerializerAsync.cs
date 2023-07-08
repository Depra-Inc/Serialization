// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Depra.Serialization.Domain.Interfaces;

namespace Depra.Serialization.Application.Helpers
{
    internal static class FakeSerializerAsync
    {
        public static Task SerializeAsync<TIn>(ISerializer serializer, Stream outputStream, TIn input) =>
            Task.Run(() => serializer.Serialize(outputStream, input));

        public static ValueTask<TOut> DeserializeAsync<TOut>(ISerializer serializer, Stream inputStream,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var deserializedObject = serializer.Deserialize<TOut>(inputStream);
            
            return new ValueTask<TOut>(deserializedObject);
        }
    }
}