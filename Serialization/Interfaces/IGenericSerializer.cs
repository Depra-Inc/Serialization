using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Depra.Serialization.Interfaces
{
	public interface IGenericSerializer
	{
		/// <summary>
		/// Serializes the given <paramref name="input"/> into a <see cref="byte"/> array.
		/// </summary>
		/// <param name="input">The object to be serialized.</param>
		/// <typeparam name="TIn">The type of the object to be serialized.</typeparam>
		/// <returns>The serialized <paramref name="input"/> as <see cref="byte"/>[].</returns>
		byte[] Serialize<TIn>(TIn input);

		/// <summary>
		/// Serializes the given <paramref name="input"/> into a <see cref="Stream"/>.
		/// </summary>
		/// <param name="outputStream">Output <see cref="Stream"/> for specified serialized object.</param>
		/// <param name="input">The object to be serialized.</param>
		/// <typeparam name="TIn">The type of the object to be serialized.</typeparam>
		void Serialize<TIn>(Stream outputStream, TIn input);

		/// <summary>
		/// Serializes the given <paramref name="input"/> into a <see cref="Stream"/> asynchronously.
		/// </summary>
		/// <param name="outputStream">Output <see cref="Stream"/> for serialized <paramref name="input"/>.</param>
		/// <param name="input">The object to be serialized.</param>
		/// <typeparam name="TIn">The type of the object to be serialized.</typeparam>
		/// <returns>Serialization <see cref="Task"/>.</returns>
		Task SerializeAsync<TIn>(Stream outputStream, TIn input);

		/// <summary>
		/// Serializes the given <paramref name="input"/> to <see cref="string"/>.
		/// </summary>
		/// <param name="input">The object to be serialized.</param>
		/// <typeparam name="TIn">The type of the object to be serialized.</typeparam>
		/// <returns>The serialized <paramref name="input"/> as <see cref="string"/>.</returns>
		string SerializeToString<TIn>(TIn input);

		/// <summary>
		/// Serializes the given <paramref name="input"/> into a pretty <see cref="string"/>.
		/// </summary>
		/// <param name="input">The object to be serialized.</param>
		/// <typeparam name="TIn">The type of the object to be serialized.</typeparam>
		/// <returns>The serialized <paramref name="input"/> as pretty (or not) <see cref="string"/>.</returns>
		string SerializeToPrettyString<TIn>(TIn input);

		/// <summary>
		/// Deserializes the specified object from given <see cref="string"/>.
		/// </summary>
		/// <param name="input">The <see cref="string"/> to be deserialized.</param>
		/// <typeparam name="TOut">The type of the object to be deserialized.</typeparam>
		/// <returns>The deserialized object of specified type.</returns>
		TOut Deserialize<TOut>(string input);

		/// <summary>
		/// Deserializes the specified object from given <see cref="Stream"/>.
		/// </summary>
		/// <param name="inputStream">The <see cref="Stream"/> to be deserialized.</param>
		/// <typeparam name="TOut">The type of the object to be deserialized.</typeparam>
		/// <returns>The deserialized object of specified type.</returns>
		TOut Deserialize<TOut>(Stream inputStream);

		/// <summary>
		/// Deserializes the specified object from given <see cref="Stream"/> asynchronously.
		/// </summary>
		/// <param name="inputStream">The <see cref="Stream"/> to be deserialized.</param>
		/// <param name="cancellationToken">For cancellation ability.</param>
		/// <typeparam name="TOut">The type of the object to be deserialized.</typeparam>
		/// <returns>The deserialized object of specified type.</returns>
		ValueTask<TOut> DeserializeAsync<TOut>(Stream inputStream, CancellationToken cancellationToken = default);
	}
}