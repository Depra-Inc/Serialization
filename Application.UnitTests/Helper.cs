// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Depra.Serialization.Application.UnitTests
{
    internal static class Helper
    {
        public static void PrintDebugCloneResult<T>(T sourceObject, T clonedObject)
        {
            Console.WriteLine($"{nameof(sourceObject)} : {sourceObject}\n" +
                              $"{nameof(clonedObject)} : {clonedObject}");
        }
    }
}