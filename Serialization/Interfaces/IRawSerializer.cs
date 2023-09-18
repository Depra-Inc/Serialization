using System;

namespace Depra.Serialization.Interfaces
{
	public interface IRawSerializer
	{
		/// <summary>
		/// Serializes the given <paramref name="input"/> into a <see cref="byte"/> array.
		/// </summary>
		/// <param name="input">The object to be serialized.</param>
		/// <typeparam name="TIn">The type of the object to be serialized.</typeparam>
		/// <returns>The serialized <paramref name="input"/> as <see cref="byte"/>[].</returns>
		byte[] Serialize<TIn>(TIn input);

		/// <summary>
		/// Serializes the given <paramref name="input"/> into a <see cref="byte"/> array.
		/// </summary>
		/// <param name="input">The object to be serialized.</param>
		/// <param name="inputType">The type of the object to be serialized.</param>
		/// <returns>The serialized <paramref name="input"/> as <see cref="byte"/>[].</returns>
		byte[] Serialize(object input, Type inputType);

		TOut Deserialize<TOut>(byte[] input);

		object Deserialize(byte[] input, Type outputType);
	}
}