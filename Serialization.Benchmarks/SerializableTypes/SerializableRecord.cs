// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Xml.Serialization;

namespace Depra.Serialization.Benchmarks.SerializableTypes;

/// <summary>
/// Must be public to <see cref="XmlSerializer"/>.
/// </summary>
[Serializable]
public record SerializableRecord(string Id)
{
	/// <summary>
	/// Required for <see cref="XmlSerializer"/>
	/// </summary>
	public SerializableRecord() : this("") { }
}