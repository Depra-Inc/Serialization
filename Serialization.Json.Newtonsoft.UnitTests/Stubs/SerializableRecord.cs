// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Serialization.Json.Newtonsoft.UnitTests.Stubs;

internal record SerializableRecord(string Id)
{
	public SerializableRecord() : this(Guid.NewGuid().ToString()) { }
}