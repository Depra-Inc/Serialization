﻿// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Serialization.Json.Microsoft.UnitTests.Stubs;

internal struct SerializableStruct
{
	/// <summary>
	/// Property can be a field.
	/// </summary>
	public string Id { get; set; }

	public SerializableStruct(string id) => Id = id;

	public SerializableStruct() => Id = Guid.NewGuid().ToString();

	public override string ToString() => Id;
}