// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Xml.Serialization;

namespace Depra.Serialization.Application.UnitTests.Types;

/// <summary>
/// Cannot be private and internal to <see cref="XmlSerializer"/>.
/// </summary>
[Serializable]
public struct SerializableStruct
{
    /// <summary>
    /// Property can be a field.
    /// Cannot be private and internal to <see cref="XmlSerializer"/>.
    /// </summary>
    public string Id { get; set; }

    public SerializableStruct(string id) => Id = id;

    public override string ToString() => Id;
}