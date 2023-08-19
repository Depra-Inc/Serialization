// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Xml.Serialization;

namespace Depra.Serialization.UnitTests.Stubs;

/// <summary>
/// Must be public to <see cref="XmlSerializer"/>.
/// </summary>
[Serializable]
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
    public SerializableClass() => Id = string.Empty;

    public SerializableClass(string id) => Id = id;

    public override string ToString() => Id;
}