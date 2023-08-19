// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Extensions
{
	public static partial class SerializerExtensions
	{
		internal static Task SerializeAsync<TIn>(this ISerializer self, Stream outputStream, TIn input) =>
			Task.Run(() => self.Serialize(outputStream, input));

		internal static ValueTask<TOut> DeserializeAsync<TOut>(this ISerializer self, Stream inputStream,
			CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();
			var deserializedObject = self.Deserialize<TOut>(inputStream);

			return new ValueTask<TOut>(deserializedObject);
		}
	}
}