// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using Depra.Serialization.Domain.Interfaces;

namespace Depra.Serialization.Domain.Extensions
{
    public static class SerializerExtensions
    {
        /// <summary>
        /// Deserializes the specified object from given <see cref="byte"/>[].
        /// </summary>
        /// <param name="serializer">Helper serializer.</param>
        /// <param name="serializedObject">The serialized object as <see cref="byte"/>[].</param>
        /// <typeparam name="TOut">The type of the object to be deserialized.</typeparam>
        /// <returns>The deserialized object of specified type.</returns>
        public static TOut DeserializeBytes<TOut>(this ISerializer serializer, byte[] serializedObject)
        {
            using var memoryStream = new MemoryStream(serializedObject);
            var deserializedObject = serializer.Deserialize<TOut>(memoryStream);

            return deserializedObject;
        }

        /// <summary>
        /// Clones the specified <paramref name="input"/> from give instance.
        /// </summary>
        /// <param name="serializer">Helper serializer.</param>
        /// <param name="input">The object to be serialized.</param>
        /// <typeparam name="T">The type of the object to be cloned.</typeparam>
        /// <returns>The cloned object of specified type.</returns>
        public static T Clone<T>(this ISerializer serializer, T input)
        {
            var bytes = serializer.Serialize(input);
            var deserializedObject = serializer.DeserializeBytes<T>(bytes);

            return deserializedObject;
        }

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
    }
}