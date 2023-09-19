// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using System.IO;
using System.Runtime.CompilerServices;

namespace Depra.Serialization.Extensions
{
	public static class StreamExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SeekIfAtEnd(this Stream stream)
		{
			if (stream.Position == stream.Length)
			{
				stream.Seek(0, SeekOrigin.Begin);
			}
		}
	}
}