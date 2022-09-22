// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Depra.Serialization.Application.Serializers;

namespace Depra.Serialization.Application.Services
{
    public interface ISerializationService : ISerializer, IAsyncSerializer { }
}