// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using System.Text;
using Depra.Serialization.Domain.Extensions;
using Depra.Serialization.Domain.Interfaces;

namespace Depra.Serialization.Application.Helpers
{
    internal static class MemorySerialization
    {
        public static byte[] SerializeToBytes<TIn>(ISerializer serializer, TIn obj)
        {
            using var memoryStream = new MemoryStream();
            serializer.Serialize(memoryStream, obj);
            var bytes = memoryStream.ToArray();
            
            return bytes;
        }

        public static string SerializeToString<TIn>(ISerializer serializer, TIn input, Encoding encoding)
        {
            using var stream = serializer.SerializeToMemoryStream(input);
            var bytes = stream.ToArray();
            var stringFromStream = encoding.GetString(bytes);
            
            return stringFromStream;
        }

        public static TOut DeserializeFromString<TOut>(ISerializer serializer, string input, Encoding encoding)
        {
            var bytes = encoding.GetBytes(input);
            using var stream = new MemoryStream(bytes);
            var deserializedObject = serializer.Deserialize<TOut>(stream);
            
            return deserializedObject;
        }
    }
}