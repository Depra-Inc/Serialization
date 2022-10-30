// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER || NET5_0_OR_GREATER
#define CSHARP8_OR_GREATER
#endif
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Depra.Serialization.Application.Errors
{
    internal static class Guard
    {
        private const string CantBeNull = "{0} can't be null or empty.";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AgainstEmpty<T>(ArraySegment<T> argumentValue, string argumentName)
        {
            if (argumentValue.Count == 0)
            {
                throw new ArgumentException(string.Format(CantBeNull, argumentName));
            }
        }

#if CSHARP8_OR_GREATER
        public static void AgainstEmpty<T>(ReadOnlyMemory<T> argumentValue, string argumentName)
        {
            if (argumentValue.Length == 0)
            {
                throw new ArgumentException(string.Format(CantBeNull, argumentName));
            }
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AgainstNullOrEmpty(string argumentValue, string argumentName)
        {
            if (string.IsNullOrEmpty(argumentValue))
            {
                throw new ArgumentException(string.Format(CantBeNull, argumentName));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AgainstNullOrEmpty(Stream argumentValue, string argumentName)
        {
            if (argumentValue is null || argumentValue.Length == 0)
            {
                throw new ArgumentException(string.Format(CantBeNull, argumentName));
            }
        }
    }
}