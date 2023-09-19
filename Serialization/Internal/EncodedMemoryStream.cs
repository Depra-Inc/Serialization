// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.IO;
using System.Text;

namespace Depra.Serialization.Internal
{
	internal readonly struct EncodedMemoryStream : IDisposable
	{
		public static implicit operator MemoryStream(EncodedMemoryStream self) => self._memoryStream;

		private readonly MemoryStream _memoryStream;

		public EncodedMemoryStream(Encoding encoding, string input) =>
			_memoryStream = new MemoryStream(encoding.GetBytes(input));

		void IDisposable.Dispose() => _memoryStream.Dispose();
	}
}