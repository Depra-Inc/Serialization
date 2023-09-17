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
		/// <param name="inputType">The type of the object to be cloned.</param>
		/// <returns>The cloned object of specified type.</returns>
		public static object Clone(this ISerializer serializer, object input, Type inputType) =>
			serializer.DeserializeBytes(serializer.Serialize(input, inputType), inputType);

		/// <summary>
		/// Deserializes the specified object from given <see cref="byte"/>[].
		/// </summary>
		/// <param name="self">Helper serializer.</param>
		/// <param name="serializedObject">The serialized object as <see cref="byte"/>[].</param>
		/// <param name="outputType">The type of the object to be deserialized.</param>
		/// <returns>The deserialized object of specified type.</returns>
		private static object DeserializeBytes(this ISerializer self, byte[] serializedObject, Type outputType)
		{
			using var memoryStream = new MemoryStream(serializedObject);
			return self.Deserialize(memoryStream, outputType);
		}
	}
}