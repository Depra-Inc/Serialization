// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Extensions
{
    public static partial class SerializerExtensions
    {
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
        /// Deserializes the specified object from given <see cref="byte"/>[].
        /// </summary>
        /// <param name="serializer">Helper serializer.</param>
        /// <param name="serializedObject">The serialized object as <see cref="byte"/>[].</param>
        /// <typeparam name="TOut">The type of the object to be deserialized.</typeparam>
        /// <returns>The deserialized object of specified type.</returns>
        private static TOut DeserializeBytes<TOut>(this ISerializer serializer, byte[] serializedObject)
        {
            using var memoryStream = new MemoryStream(serializedObject);
            var deserializedObject = serializer.Deserialize<TOut>(memoryStream);

            return deserializedObject;
        }
    }
}