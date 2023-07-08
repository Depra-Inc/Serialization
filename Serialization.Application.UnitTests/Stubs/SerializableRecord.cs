// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Xml.Serialization;

namespace Depra.Serialization.Application.UnitTests.Stubs;

/// <summary>
/// Must be public to <see cref="XmlSerializer"/>.
/// </summary>
/// <param name="Id"></param>
[Serializable]
public record SerializableRecord(string Id)
{
    /// <summary>
    /// Required for <see cref="XmlSerializer"/>
    /// </summary>
    public SerializableRecord() : this(string.Empty) { }
}