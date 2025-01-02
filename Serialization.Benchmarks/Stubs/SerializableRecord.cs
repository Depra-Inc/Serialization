// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Xml.Serialization;

namespace Depra.Serialization.Benchmarks.Stubs
{
	/// <summary>
	/// Must be public to <see cref="XmlSerializer"/>.
	/// </summary>
	[Serializable]
	public record SerializableRecord(string Id)
	{
		internal static readonly Type Type = typeof(SerializableRecord);

		public string Id { get; } = Id;

		/// <summary>
		/// Required for <see cref="XmlSerializer"/>
		/// </summary>
		public SerializableRecord() : this("") { }
	}
}