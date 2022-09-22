// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Depra.Serialization.Application.Serializers
{
    public interface IAsyncSerializer
    {
        Task SerializeAsync(object obj, Stream stream, CancellationToken cancellationToken);

        Task<object> DeserializeAsync(Stream stream, CancellationToken cancellationToken);
    }
}