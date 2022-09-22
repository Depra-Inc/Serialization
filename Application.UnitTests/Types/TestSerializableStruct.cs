// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Xml.Serialization;
using Depra.Serialization.Infrastructure.Newtonsoft.Json;

namespace Depra.Serialization.Application.UnitTests.Types
{
    /// <summary>
    /// Cannot be private and internal to <see cref="XmlSerializer"/>.
    /// </summary>
    [Serializable]
    public struct TestSerializableStruct
    {
        /// <summary>
        /// Property can be a field.
        /// Must be public to to <see cref="XmlSerializer"/> and <see cref="NewtonsoftJsonSerializer"/>
        /// </summary>
        public string Id { get; set; }

        public override string ToString() => Id;
    }
}