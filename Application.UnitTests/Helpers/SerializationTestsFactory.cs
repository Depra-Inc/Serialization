// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using Depra.Serialization.Application.Binary;
using Depra.Serialization.Application.Json;
using Depra.Serialization.Application.Xml;
using Depra.Serialization.Domain.Serializers;

namespace Depra.Serialization.Application.UnitTests.Helpers;

internal static class SerializationTestsFactory
{
    public static IEnumerable<ISerializer> GetSerializers()
    {
        // Binary.
        yield return new BinarySerializer();

        // XML.
        yield return new StandardXmlSerializer();
        yield return new DataContractXmlSerializer();

        // Json.
        yield return new DataContractJsonSerializerAdapter();

        // Add more serializers here if needed.
    }
}