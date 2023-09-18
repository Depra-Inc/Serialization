// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Depra.Serialization.Interfaces
{
	public interface ISerializer
	{
		/// <summary>
		/// Serializes the given <paramref name="input"/> into a <see cref="byte"/> array.
		/// </summary>
		/// <param name="input">The object to be serialized.</param>
		/// <param name="inputType">The type of the object to be serialized.</param>
		/// <returns>The serialized <paramref name="input"/> as <see cref="byte"/>[].</returns>
		byte[] Serialize(object input, Type inputType);

		/// <summary>
		/// Serializes the given <paramref name="input"/> into a <see cref="Stream"/>.
		/// </summary>
		/// <param name="outputStream">Output <see cref="Stream"/> for specified serialized object.</param>
		/// <param name="input">The object to be serialized.</param>
		/// <param name="inputType">The type of the object to be serialized.</param>
		void Serialize(Stream outputStream, object input, Type inputType);

		/// <summary>
		/// Serializes the given <paramref name="input"/> into a <see cref="Stream"/> asynchronously.
		/// </summary>
		/// <param name="outputStream">Output <see cref="Stream"/> for serialized <paramref name="input"/>.</param>
		/// <param name="input">The object to be serialized.</param>
		/// <param name="inputType">The type of the object to be serialized.</param>
		/// <returns>Serialization <see cref="Task"/>.</returns>
		Task SerializeAsync(Stream outputStream, object input, Type inputType);

		/// <summary>
		/// Serializes the given <paramref name="input"/> to <see cref="string"/>.
		/// </summary>
		/// <param name="input">The object to be serialized.</param>
		/// <param name="inputType">The type of the object to be serialized.</param>
		/// <returns>The serialized <paramref name="input"/> as <see cref="string"/>.</returns>
		string SerializeToString(object input, Type inputType);

		/// <summary>
		/// Serializes the given <paramref name="input"/> into a pretty <see cref="string"/>.
		/// </summary>
		/// <param name="input">The object to be serialized.</param>
		/// <param name="inputType">The type of the object to be serialized.</param>
		/// <returns>The serialized <paramref name="input"/> as pretty (or not) <see cref="string"/>.</returns>
		string SerializeToPrettyString(object input, Type inputType);

		/// <summary>
		/// Deserializes the specified object from given <see cref="string"/>.
		/// </summary>
		/// <param name="input">The <see cref="string"/> to be deserialized.</param>
		/// <param name="outputType">The type of the object to be deserialized.</param>
		/// <returns>The deserialized object of specified type.</returns>
		object Deserialize(string input, Type outputType);

		/// <summary>
		/// Deserializes the specified object from given <see cref="Stream"/>.
		/// </summary>
		/// <param name="inputStream">The <see cref="Stream"/> to be deserialized.</param>
		/// <param name="outputType">The type of the object to be deserialized.</param>
		/// <returns>The deserialized object of specified type.</returns>
		object Deserialize(Stream inputStream, Type outputType);

		/// <summary>
		/// Deserializes the specified object from given <see cref="Stream"/> asynchronously.
		/// </summary>
		/// <param name="inputStream">The <see cref="Stream"/> to be deserialized.</param>
		/// <param name="cancellationToken">For cancellation ability.</param>
		/// <param name="outputType">The type of the object to be deserialized.</param>
		/// <returns>The deserialized object of specified type.</returns>
		ValueTask<object> DeserializeAsync(
			Stream inputStream,
			Type outputType,
			CancellationToken cancellationToken = default);
	}
}