// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Extensions
{
	public static class GenericSerializerExtensions
	{
		/// <summary>
		/// Clones the specified <paramref name="input"/> from give instance.
		/// </summary>
		/// <param name="serializer">Helper serializer.</param>
		/// <param name="input">The object to be serialized.</param>
		/// <typeparam name="TOut">The type of the object to be cloned.</typeparam>
		/// <returns>The cloned object of specified type.</returns>
		public static TOut Clone<TOut>(this IGenericSerializer serializer, TOut input) =>
			serializer.DeserializeBytes<TOut>(serializer.Serialize(input));

		/// <summary>
		/// Deserializes the specified object from given <see cref="byte"/>[].
		/// </summary>
		/// <param name="self">Helper serializer.</param>
		/// <param name="serializedObject">The serialized object as <see cref="byte"/>[].</param>
		/// <typeparam name="TOut">The type of the object to be deserialized.</typeparam>
		/// <returns>The deserialized object of specified type.</returns>
		private static TOut DeserializeBytes<TOut>(this IGenericSerializer self, byte[] serializedObject)
		{
			using var memoryStream = new MemoryStream(serializedObject);
			return self.Deserialize<TOut>(memoryStream);
		}
	}
}