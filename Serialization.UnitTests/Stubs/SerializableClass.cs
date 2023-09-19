// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Depra.Serialization.UnitTests.Stubs;

/// <summary>
/// Must be public to <see cref="XmlSerializer"/>.
/// </summary>
[Serializable]
[KnownType(typeof(SerializableClass))]
public class SerializableClass
{
	/// <summary>
	/// Property can be a field.
	/// Cannot be private and internal to <see cref="XmlSerializer"/>.
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// Required for <see cref="XmlSerializer"/>
	/// </summary>
	public SerializableClass() => Id = Guid.NewGuid().ToString();

	public SerializableClass(string id) => Id = id;

	public override string ToString() => Id;
}