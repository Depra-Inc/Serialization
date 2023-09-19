// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Serialization.Json.Newtonsoft.UnitTests.Stubs;

internal struct SerializableStruct
{
	/// <summary>
	/// Property can be a field.
	/// Cannot be private and internal to <see cref="NewtonsoftJsonSerializer"/>.
	/// </summary>
	public string Id { get; set; }

	public SerializableStruct(string id) => Id = id;

	public SerializableStruct() => Id = Guid.NewGuid().ToString();

	public override string ToString() => Id;
}