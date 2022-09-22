// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Xml.Serialization;
using Depra.Serialization.Application.Serializers;

namespace Depra.Serialization.Infrastructure.Serializers.Xml
{
    public struct StandardXmlSerializer : ISerializer
    {
        public void Serialize(object obj, Stream stream, Type objType)
        {
            var serializer = new XmlSerializer(objType);
            serializer.Serialize(stream, obj);
        }

        public object Deserialize(Stream stream, Type objType)
        {
            var serializer = new XmlSerializer(objType);
            var deserialized = serializer.Deserialize(stream);
            
            return deserialized;
        }

        public override string ToString() => nameof(XmlSerializer);
    }
}