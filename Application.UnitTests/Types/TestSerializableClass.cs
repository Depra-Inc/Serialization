// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Xml.Serialization;
using Depra.Serialization.Infrastructure.Newtonsoft.Json;

namespace Depra.Serialization.Application.UnitTests.Types
{
    /// <summary>
    /// Must be public to <see cref="XmlSerializer"/>.
    /// </summary>
    [Serializable]
    public class TestSerializableClass
    {
        /// <summary>
        /// Property can be a field.
        /// Must be public to <see cref="XmlSerializer"/> and <see cref="NewtonsoftJsonSerializer"/>
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Required for <see cref="XmlSerializer"/>
        /// </summary>
        public TestSerializableClass()
        {
            Id = SerializersTests.BadId;
        }

        public TestSerializableClass(string id) => Id = id;

        public override string ToString() => Id;
    }
}