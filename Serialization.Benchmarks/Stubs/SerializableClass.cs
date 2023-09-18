// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Xml.Serialization;
using Depra.Serialization.Json.Newtonsoft;

namespace Depra.Serialization.Benchmarks.Stubs;

/// <summary>
/// Must be public to <see cref="XmlSerializer"/>.
/// </summary>
[Serializable]
public class SerializableClass
{
	internal static readonly Type Type = typeof(SerializableClass);

	/// <summary>
	/// Property can be a field.
	/// Cannot be private and internal to <see cref="XmlSerializer"/> and <see cref="NewtonsoftJsonSerializer"/>
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// Required for <see cref="XmlSerializer"/>
	/// </summary>
	public SerializableClass() { }

	public SerializableClass(string id) => Id = id;

	public override string ToString() => Id;
}