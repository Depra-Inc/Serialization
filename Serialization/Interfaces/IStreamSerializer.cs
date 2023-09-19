// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Depra.Serialization
{
	public interface IStreamSerializer
	{
		/// <summary>
		/// Serializes the given <paramref name="input"/> into a <see cref="Stream"/>.
		/// </summary>
		/// <param name="outputStream">Output <see cref="Stream"/> for specified serialized object.</param>
		/// <param name="input">The object to be serialized.</param>
		/// <typeparam name="TIn">The type of the object to be serialized.</typeparam>
		void Serialize<TIn>(Stream outputStream, TIn input);

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
		/// <typeparam name="TIn">The type of the object to be serialized.</typeparam>
		/// <returns>Serialization <see cref="Task"/>.</returns>
		Task SerializeAsync<TIn>(Stream outputStream, TIn input);

		/// <summary>
		/// Serializes the given <paramref name="input"/> into a <see cref="Stream"/> asynchronously.
		/// </summary>
		/// <param name="outputStream">Output <see cref="Stream"/> for serialized <paramref name="input"/>.</param>
		/// <param name="input">The object to be serialized.</param>
		/// <param name="inputType">The type of the object to be serialized.</param>
		/// <returns>Serialization <see cref="Task"/>.</returns>
		Task SerializeAsync(Stream outputStream, object input, Type inputType);

		/// <summary>
		/// Deserializes the specified object from given <see cref="Stream"/>.
		/// </summary>
		/// <param name="inputStream">The <see cref="Stream"/> to be deserialized.</param>
		/// <typeparam name="TOut">The type of the object to be deserialized.</typeparam>
		/// <returns>The deserialized object of specified type.</returns>
		TOut Deserialize<TOut>(Stream inputStream);

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
		/// <typeparam name="TOut">The type of the object to be deserialized.</typeparam>
		/// <returns>The deserialized object of specified type.</returns>
		ValueTask<TOut> DeserializeAsync<TOut>(Stream inputStream, CancellationToken cancellationToken = default);

		/// <summary>
		/// Deserializes the specified object from given <see cref="Stream"/> asynchronously.
		/// </summary>
		/// <param name="inputStream">The <see cref="Stream"/> to be deserialized.</param>
		/// <param name="cancellationToken">For cancellation ability.</param>
		/// <param name="outputType">The type of the object to be deserialized.</param>
		/// <returns>The deserialized object of specified type.</returns>
		ValueTask<object> DeserializeAsync(Stream inputStream, Type outputType, CancellationToken cancellationToken = default);
	}
}