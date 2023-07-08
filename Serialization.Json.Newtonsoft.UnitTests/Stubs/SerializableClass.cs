// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Depra.Serialization.Json.Newtonsoft.UnitTests.Stubs
{
    internal class SerializableClass
    {
        /// <summary>
        /// Property can be a field.
        /// Cannot be private and internal to <see cref="NewtonsoftJsonSerializer"/>.
        /// </summary>
        public string Id { get; set; }

        public SerializableClass(string id) => Id = id;

        public override string ToString() => Id;
    }
}