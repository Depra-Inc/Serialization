// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using Depra.Serialization.Application.Binary;
using Depra.Serialization.Application.Json;
using Depra.Serialization.Application.Xml;
using Depra.Serialization.Domain.Interfaces;

namespace Depra.Serialization.Application.UnitTests.Helpers;

internal static class SerializationTestsFactory
{
    public static IEnumerable<ISerializer> GetSerializers()
    {
        // Binary.
#pragma warning disable CS0612
        yield return new BinarySerializer();
#pragma warning restore CS0612

        // XML.
        yield return new StandardXmlSerializer();
        yield return new DataContractXmlSerializer();

        // Json.
        yield return new DataContractJsonSerializerAdapter();

        // Add more serializers here if needed.
    }
}