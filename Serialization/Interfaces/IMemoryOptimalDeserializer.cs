// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Depra.Serialization.Interfaces
{
    /// <summary>
    /// Interface for deserializing bytes from regions of memory.
    /// </summary>
    public interface IMemoryOptimalDeserializer
    {
        /// <summary>
        /// Deserializes the specified object from given <see cref="ReadOnlyMemory{T}"/> with bytes.
        /// </summary>
        /// <param name="input">The <see cref="ReadOnlyMemory{T}"/> to be deserialized.</param>
        /// <typeparam name="TOut">The type of the object to be deserialized.</typeparam>
        /// <returns>The deserialized object of specified type.</returns>
        TOut Deserialize<TOut>(ReadOnlyMemory<byte> input);
    }
}