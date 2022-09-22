// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Runtime.Serialization;
using Depra.Serialization.Application.Serializers;

namespace Depra.Serialization.Infrastructure.Serializers.Xml
{
    public struct DataContractXmlSerializer : ISerializer
    {
        public void Serialize(object obj, Stream stream, Type objType)
        {
            var serializer = new DataContractSerializer(objType);
            serializer.WriteObject(stream, obj);
        }

        public object Deserialize(Stream stream, Type objType)
        {
            var serializer = new DataContractSerializer(objType);
            var deserialized = serializer.ReadObject(stream);

            return deserialized;
        }

        public override string ToString() => "DC_XMLSerializer";
    }
}