// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Linq;

namespace Depra.Serialization.Extensions
{
	public static class ByteEnumerableExtensions
	{
		public static string Flatten(this IEnumerable<byte> bytes) =>
			bytes.Aggregate(string.Empty, (current, @byte) => current + @byte);
	}
}