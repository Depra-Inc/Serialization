// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Xml.Serialization;
using Depra.Serialization.Json.Newtonsoft;

namespace Depra.Serialization.Benchmarks.Stubs
{
	/// <summary>
	/// Must be public to <see cref="XmlSerializer"/>.
	/// </summary>
	[Serializable]
	public struct SerializableStruct
	{
		internal static readonly Type Type = typeof(SerializableStruct);

		/// <summary>
		/// Property can be a field.
		/// Cannot be private and internal to <see cref="XmlSerializer"/> and <see cref="NewtonsoftJsonSerializer"/>
		/// </summary>
		public string Id { get; set; }

		public override string ToString() => Id;
	}
}