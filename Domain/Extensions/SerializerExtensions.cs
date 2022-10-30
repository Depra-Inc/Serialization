// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using Depra.Serialization.Domain.Serializers;

namespace Depra.Serialization.Domain.Extensions
{
    public static class SerializerExtensions
    {
        /// <summary>
        /// Serializes the given object into memory stream.
        /// </summary>
        /// <param name="serializer">Serializer for object.</param>
        /// <param name="input">The object to be serialized.</param>
        /// <returns>The serialized object as memory stream.</returns>
        public static MemoryStream SerializeToStream<TIn>(this ISerializer serializer, TIn input)
        {
            var memoryStream = new MemoryStream();
            serializer.Serialize(memoryStream, input);

            return memoryStream;
        }

        public static TOut DeserializeBytes<TOut>(this ISerializer serializer, byte[] serializedObject)
        {
            TOut deserializedObject;
            using (var memoryStream = new MemoryStream(serializedObject))
            {
                deserializedObject = serializer.Deserialize<TOut>(memoryStream);
            }

            return deserializedObject;
        }

        public static T Clone<T>(this ISerializer serializer, T input)
        {
            var bytes = serializer.Serialize(input);
            var deserializedObject = serializer.DeserializeBytes<T>(bytes);

            return deserializedObject;
        }
    }
}