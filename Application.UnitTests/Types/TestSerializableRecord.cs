// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Xml.Serialization;

namespace Depra.Serialization.Application.UnitTests.Types
{
    /// <summary>
    /// Must be public to <see cref="XmlSerializer"/>.
    /// </summary>
    /// <param name="Id"></param>
    [Serializable]
    public record TestSerializableRecord(string Id)
    {
        /// <summary>
        /// Required for <see cref="XmlSerializer"/>
        /// </summary>
        public TestSerializableRecord() : this(SerializersTests.BadId) { }
    }
}