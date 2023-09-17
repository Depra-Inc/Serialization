// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using System.Text;
using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Extensions
{
    public static partial class GenericSerializerExtensions
    {
        /// <summary>
        /// Serializes the given <paramref name="input"/> into <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="serializer"><see cref="IGenericSerializer"/> for <paramref name="input"/>.</param>
        /// <param name="input">The object to be serialized.</param>
        /// <returns>The serialized <paramref name="input"/> as <see cref="MemoryStream"/>.</returns>
        internal static MemoryStream SerializeToMemoryStream<TIn>(this IGenericSerializer serializer, TIn input)
        {
            var memoryStream = new MemoryStream();
            serializer.Serialize(memoryStream, input);

            return memoryStream;
        }

        internal static byte[] SerializeToBytes<TIn>(IGenericSerializer serializer, TIn obj)
        {
            using var memoryStream = new MemoryStream();
            serializer.Serialize(memoryStream, obj);

            return memoryStream.ToArray();
        }

        internal static string SerializeToString<TIn>(IGenericSerializer serializer, TIn input, Encoding encoding)
        {
            using var stream = serializer.SerializeToMemoryStream(input);
            var bytes = stream.ToArray();

            return encoding.GetString(bytes);
        }

        internal static TOut DeserializeFromString<TOut>(IGenericSerializer serializer, string input, Encoding encoding)
        {
            var bytes = encoding.GetBytes(input);
            using var stream = new MemoryStream(bytes);

            return serializer.Deserialize<TOut>(stream);
        }
    }
}