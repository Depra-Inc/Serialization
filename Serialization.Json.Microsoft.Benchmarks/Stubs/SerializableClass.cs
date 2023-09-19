// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Serialization.Json.Microsoft.Benchmarks.Stubs;

public class SerializableClass
{
	public readonly string Id;

	public SerializableClass(string id) => Id = id;

	public override string ToString() => Id;
}