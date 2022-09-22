// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using Depra.Serialization.Application.Serializers;

namespace Depra.Serialization.Application.Extensions
{
    public static class SerializerExtensions
    {
        public static byte[] SerializeToBytes<T>(this ISerializer serializer, T obj) =>
            SerializeToBytes(serializer, obj, obj.GetType());

        public static T DeserializeBytes<T>(this ISerializer serializer, byte[] serializedObject) =>
            (T)DeserializeBytes(serializer, serializedObject, serializedObject.GetType());

        public static T Clone<T>(this ISerializer serializer, T source) =>
            (T)Clone(serializer, source, source.GetType());

        public static Stream SerializeToStream<T>(this ISerializer serializer, T obj) =>
            SerializeToStream(serializer, obj, obj.GetType());

        public static T DeserializeFromStream<T>(this ISerializer serializer, MemoryStream stream) =>
            (T)DeserializeFromStream(serializer, stream, typeof(T));

        public static byte[] SerializeToBytes(this ISerializer serializer, object obj, Type objType)
        {
            using (var memoryStream = new MemoryStream())
            {
                serializer.Serialize(obj, memoryStream, objType);
                return memoryStream.ToArray();
            }
        }

        public static object DeserializeBytes(this ISerializer serializer, byte[] serializedObject, Type objType)
        {
            object deserializedObject;
            using (var memoryStream = new MemoryStream(serializedObject))
            {
                deserializedObject = serializer.Deserialize(memoryStream, objType);
            }

            return deserializedObject;
        }

        public static object Clone(this ISerializer serializer, object source, Type sourceType)
        {
            var bytes = serializer.SerializeToBytes(source);
            var deserializedObject = serializer.DeserializeBytes(bytes, sourceType);

            return deserializedObject;
        }

        /// <summary>
        /// Serializes the given object into memory stream.
        /// </summary>
        /// <param name="serializer">Serializer for object.</param>
        /// <param name="obj">The object to be serialized.</param>
        /// <param name="objType">The type of the object being serialized.</param>
        /// <returns>The serialized object as memory stream.</returns>
        public static Stream SerializeToStream(this ISerializer serializer, object obj, Type objType)
        {
            var stream = new MemoryStream();
            serializer.Serialize(obj, stream, objType);

            return stream;
        }

        /// <summary>
        /// Deserializes as an object.
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="stream">The stream to deserialize.</param>
        /// <param name="objType">The type of the object being deserialized.</param>
        /// <returns>The deserialized object.</returns>
        public static object DeserializeFromStream(this ISerializer serializer, Stream stream, Type objType)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var deserializedObject = serializer.Deserialize(stream, objType);

            return deserializedObject;
        }
    }
}