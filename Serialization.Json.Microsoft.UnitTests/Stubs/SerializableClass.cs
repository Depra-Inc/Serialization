// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Serialization.Json.Microsoft.UnitTests.Stubs;

internal class SerializableClass
{
    /// <summary>
    /// Property can be a field.
    /// </summary>
    public string Id { get; set; }

    public SerializableClass(string id) => Id = id;

    public SerializableClass() => Id = Guid.NewGuid().ToString();

    public override string ToString() => Id;
}