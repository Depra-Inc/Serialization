// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Depra.Serialization.Errors
{
    internal static class Guard
    {
        private const string CANT_BE_NULL = "{0} can't be null or empty.";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AgainstNullOrEmpty(string argumentValue, string argumentName)
        {
            if (string.IsNullOrEmpty(argumentValue))
            {
                throw new ArgumentException(string.Format(CANT_BE_NULL, argumentName));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AgainstNullOrEmpty(Stream argumentValue, string argumentName)
        {
            if (argumentValue is null || argumentValue.Length == 0)
            {
                throw new ArgumentException(string.Format(CANT_BE_NULL, argumentName));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AgainstEmpty<T>(ReadOnlyMemory<T> argumentValue, string argumentName)
        {
            if (argumentValue.Length == 0)
            {
                throw new ArgumentException(string.Format(CANT_BE_NULL, argumentName));
            }
        }
    }
}