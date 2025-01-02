// SPDX-License-Identifier: Apache-2.0
// © 2022-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Serialization
{
	public interface IPrettyTextSerializer
	{
		/// <summary>
		/// Serializes the given <paramref name="input"/> into a pretty <see cref="string"/>.
		/// </summary>
		/// <param name="input">The object to be serialized.</param>
		/// <typeparam name="TIn">The type of the object to be serialized.</typeparam>
		/// <returns>The serialized <paramref name="input"/> as pretty (or not) <see cref="string"/>.</returns>
		string SerializeToPrettyString<TIn>(TIn input);

		/// <summary>
		/// Serializes the given <paramref name="input"/> into a pretty <see cref="string"/>.
		/// </summary>
		/// <param name="input">The object to be serialized.</param>
		/// <param name="inputType">The type of the object to be serialized.</param>
		/// <returns>The serialized <paramref name="input"/> as pretty (or not) <see cref="string"/>.</returns>
		string SerializeToPrettyString(object input, Type inputType);
	}
}