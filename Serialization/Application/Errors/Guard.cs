// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Depra.Serialization.Application.Errors
{
    internal static class Guard
    {
        private const string CantBeNull = "{0} can't be null or empty.";

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AgainstEmpty<T>(ReadOnlyMemory<T> argumentValue, string argumentName)
        {
            if (argumentValue.Length == 0)
            {
                throw new ArgumentException(string.Format(CantBeNull, argumentName));
            }
        }
    }
}