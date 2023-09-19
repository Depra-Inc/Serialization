// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Serialization.Json.Microsoft.UnitTests.Stubs;

internal sealed record Container(Type Type, object Value)
{
	public readonly Type Type = Type;
	public readonly object Value = Value;

	public override string ToString() => Type.Name;
}