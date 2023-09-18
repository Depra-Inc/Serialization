// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Extensions
{
	public static class SerializerExtensions
	{
		/// <summary>
		/// Clones the specified <paramref name="input"/> from give instance.
		/// </summary>
		/// <param name="serializer">Helper serializer.</param>
		/// <param name="input">The object to be serialized.</param>
		/// <typeparam name="TSerializer">The type of serializer.</typeparam>
		/// <typeparam name="TOut">The type of the object to be cloned.</typeparam>
		/// <returns>The cloned object of specified type.</returns>
		public static TOut Clone<TSerializer, TOut>(this TSerializer serializer, TOut input)
			where TSerializer : IStreamSerializer, IRawSerializer =>
			serializer.DeserializeBytes<TOut>(serializer.Serialize(input));

		/// <summary>
		/// Clones the specified <paramref name="input"/> from give instance.
		/// </summary>
		/// <param name="serializer">Helper serializer.</param>
		/// <param name="input">The object to be serialized.</param>
		/// <param name="inputType">The type of the object to be cloned.</param>
		/// <returns>The cloned object of specified type.</returns>
		public static object Clone<TSerializer>(this TSerializer serializer, object input, Type inputType)
			where TSerializer : IRawSerializer, IStreamSerializer =>
			serializer.DeserializeBytes(serializer.Serialize(input, inputType), inputType);

		/// <summary>
		/// Deserializes the specified object from given <see cref="byte"/>[].
		/// </summary>
		/// <param name="self">Helper serializer.</param>
		/// <param name="serializedObject">The serialized object as <see cref="byte"/>[].</param>
		/// <typeparam name="TOut">The type of the object to be deserialized.</typeparam>
		/// <returns>The deserialized object of specified type.</returns>
		private static TOut DeserializeBytes<TOut>(this IStreamSerializer self, byte[] serializedObject)
		{
			using var memoryStream = new MemoryStream(serializedObject);
			return self.Deserialize<TOut>(memoryStream);
		}

		/// <summary>
		/// Deserializes the specified object from given <see cref="byte"/>[].
		/// </summary>
		/// <param name="self">Helper serializer.</param>
		/// <param name="serializedObject">The serialized object as <see cref="byte"/>[].</param>
		/// <param name="outputType">The type of the object to be deserialized.</param>
		/// <returns>The deserialized object of specified type.</returns>
		private static object DeserializeBytes(this IStreamSerializer self, byte[] serializedObject, Type outputType)
		{
			using var memoryStream = new MemoryStream(serializedObject);
			return self.Deserialize(memoryStream, outputType);
		}
	}
}