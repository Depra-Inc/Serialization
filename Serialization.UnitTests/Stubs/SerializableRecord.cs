// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Depra.Serialization.UnitTests.Stubs;

/// <summary>
/// Must be public to <see cref="XmlSerializer"/>.
/// </summary>
/// <param name="Id"></param>
[Serializable]
[KnownType(typeof(SerializableRecord))]
public record SerializableRecord(string Id)
{
	/// <summary>
	/// Required for <see cref="XmlSerializer"/>
	/// </summary>
	public SerializableRecord() : this(Guid.NewGuid().ToString()) { }
}