// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using System.Text;
using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Extensions
{
    public static partial class SerializerExtensions
    {
        /// <summary>
        /// Serializes the given <paramref name="input"/> into <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> for <paramref name="input"/>.</param>
        /// <param name="input">The object to be serialized.</param>
        /// <returns>The serialized <paramref name="input"/> as <see cref="MemoryStream"/>.</returns>
        public static MemoryStream SerializeToMemoryStream<TIn>(this ISerializer serializer, TIn input)
        {
            var memoryStream = new MemoryStream();
            serializer.Serialize(memoryStream, input);

            return memoryStream;
        }

        internal static byte[] SerializeToBytes<TIn>(ISerializer serializer, TIn obj)
        {
            using var memoryStream = new MemoryStream();
            serializer.Serialize(memoryStream, obj);
            var bytes = memoryStream.ToArray();
            
            return bytes;
        }

        internal static string SerializeToString<TIn>(ISerializer serializer, TIn input, Encoding encoding)
        {
            using var stream = serializer.SerializeToMemoryStream(input);
            var bytes = stream.ToArray();
            var stringFromStream = encoding.GetString(bytes);
            
            return stringFromStream;
        }

        internal static TOut DeserializeFromString<TOut>(ISerializer serializer, string input, Encoding encoding)
        {
            var bytes = encoding.GetBytes(input);
            using var stream = new MemoryStream(bytes);
            var deserializedObject = serializer.Deserialize<TOut>(stream);
            
            return deserializedObject;
        }
    }
}