using System;

namespace Depra.Serialization
{
	public interface ITextSerializer
	{
		/// <summary>
		/// Serializes the given <paramref name="input"/> to <see cref="string"/>.
		/// </summary>
		/// <param name="input">The object to be serialized.</param>
		/// <typeparam name="TIn">The type of the object to be serialized.</typeparam>
		/// <returns>The serialized <paramref name="input"/> as <see cref="string"/>.</returns>
		string SerializeToString<TIn>(TIn input);

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

		/// <summary>
		/// Deserializes the specified object from given <see cref="string"/>.
		/// </summary>
		/// <param name="input">The <see cref="string"/> to be deserialized.</param>
		/// <typeparam name="TOut">The type of the object to be deserialized.</typeparam>
		/// <returns>The deserialized object of specified type.</returns>
		TOut Deserialize<TOut>(string input);

		/// <summary>
		/// Deserializes the specified object from given <see cref="string"/>.
		/// </summary>
		/// <param name="input">The <see cref="string"/> to be deserialized.</param>
		/// <param name="outputType">The type of the object to be deserialized.</param>
		/// <returns>The deserialized object of specified type.</returns>
		object Deserialize(string input, Type outputType);
	}
}