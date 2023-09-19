// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using System.IO;

namespace Depra.Serialization.Unsafe
{
	public interface IArrayStreamSerializer
	{
		void Serialize<TIn>(Stream outputStream, TIn[] input);

		TOut[] Deserialize<TOut>(Stream inputStream);
	}
}