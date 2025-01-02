// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.IO;

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
			where TSerializer : IStreamSerializer, ISerializer
		{
			using var memoryStream = new MemoryStream(serializer.Serialize(input));
			return serializer.Deserialize<TOut>(memoryStream);
		}

		/// <summary>
		/// Clones the specified <paramref name="input"/> from give instance.
		/// </summary>
		/// <param name="serializer">Helper serializer.</param>
		/// <param name="input">The object to be serialized.</param>
		/// <param name="inputType">The type of the object to be cloned.</param>
		/// <returns>The cloned object.</returns>
		public static object Clone<TSerializer>(this TSerializer serializer, object input, Type inputType)
			where TSerializer : ISerializer, IStreamSerializer
		{
			using var memoryStream = new MemoryStream(serializer.Serialize(input, inputType));
			return serializer.Deserialize(memoryStream, inputType);
		}
	}
}