﻿// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Depra.Serialization.UnitTests.Stubs;

/// <summary>
/// Cannot be private and internal to <see cref="XmlSerializer"/>.
/// </summary>
[Serializable]
[KnownType(typeof(SerializableStruct))]
public struct SerializableStruct
{
	/// <summary>
	/// Property can be a field.
	/// Cannot be private and internal to <see cref="XmlSerializer"/>.
	/// </summary>
	public string Id { get; set; }

	public SerializableStruct(string id) => Id = id;

	public SerializableStruct() => Id = Guid.NewGuid().ToString();

	public override string ToString() => Id;
}