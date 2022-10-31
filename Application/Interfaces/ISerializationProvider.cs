// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Depra.Serialization.Domain.Serializers;

namespace Depra.Serialization.Application.Interfaces
{
    /// <summary>
    /// Facade for <see cref="ISerializer"/>.
    /// Needed to associate the application layer with the infrastructure layer.
    /// </summary>
    internal interface ISerializationProvider : ISerializer { }
}