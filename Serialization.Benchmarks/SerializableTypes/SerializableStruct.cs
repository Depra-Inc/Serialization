// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Xml.Serialization;
using Depra.Serialization.Json.Newtonsoft;

namespace Depra.Serialization.Benchmarks.SerializableTypes;

/// <summary>
/// Must be public to <see cref="XmlSerializer"/>.
/// </summary>
[Serializable]
public struct SerializableStruct
{
    /// <summary>
    /// Property can be a field.
    /// Cannot be private and internal to <see cref="XmlSerializer"/> and <see cref="NewtonsoftJsonSerializer"/>
    /// </summary>
    public string Id { get; set; }

    public override string ToString() => Id;
}