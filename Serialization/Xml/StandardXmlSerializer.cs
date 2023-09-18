// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Depra.Serialization.Errors;
using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Xml
{
	/// <summary>
	/// Serializer using <see cref="XmlSerializer"/>.
	/// </summary>
	public readonly partial struct StandardXmlSerializer : IStreamSerializer
	{
		public void Serialize<TIn>(Stream outputStream, TIn input)
		{
			Guard.AgainstNull(input, nameof(input));
			Guard.AgainstNullOrEmpty(outputStream, nameof(outputStream));

			new XmlSerializer(typeof(TIn)).Serialize(outputStream, input);
		}

		public void Serialize(Stream outputStream, object input, Type inputType)
		{
			Guard.AgainstNull(input, nameof(input));
			Guard.AgainstNull(inputType, nameof(inputType));
			Guard.AgainstNullOrEmpty(outputStream, nameof(outputStream));

			new XmlSerializer(inputType).Serialize(outputStream, input);
		}

		public Task SerializeAsync<TIn>(Stream outputStream, TIn input)
		{
			var serializer = this;
			return Task.Run(() => serializer.Serialize(outputStream, input));
		}

		public Task SerializeAsync(Stream outputStream, object input, Type inputType)
		{
			var serializer = this;
			return Task.Run(() => serializer.Serialize(outputStream, input, inputType));
		}

		public TOut Deserialize<TOut>(Stream inputStream) =>
			(TOut)Deserialize(inputStream, typeof(TOut));

		public object Deserialize(Stream inputStream, Type outputType)
		{
			Guard.AgainstNull(outputType, nameof(outputType));
			Guard.AgainstNullOrEmpty(inputStream, nameof(inputStream));

			var serializer = new XmlSerializer(outputType);
			inputStream.Seek(0, SeekOrigin.Begin);

			return serializer.Deserialize(inputStream);
		}

		public ValueTask<TOut> DeserializeAsync<TOut>(Stream inputStream, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return new ValueTask<TOut>(Deserialize<TOut>(inputStream));
		}

		public ValueTask<object> DeserializeAsync(Stream inputStream, Type outputType,
			CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return new ValueTask<object>(Deserialize(inputStream, outputType));
		}

		/// <summary>
		/// Just for tests and benchmarks.
		/// </summary>
		/// <returns>Returns the pretty name of the serializer.</returns>
		public override string ToString() => nameof(XmlSerializer);
	}
}