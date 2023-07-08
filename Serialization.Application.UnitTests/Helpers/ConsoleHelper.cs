// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Depra.Serialization.Application.UnitTests.Helpers;

internal static class ConsoleHelper
{
    internal static void PrintResults<T>(T input, string inputArgumentName,
        T result, string resultArgumentName) =>
        Console.WriteLine($"{inputArgumentName} : {input}\n" +
                          $"{resultArgumentName} : {result}");

    internal static void PrintResults<TIn, TOut>(TIn input, string inputArgumentName,
        TOut result, string resultArgumentName) =>
        Console.WriteLine($"{inputArgumentName} : {input}\n" +
                          $"{resultArgumentName} : {result}");

    internal static void PrintResults<TIn>(TIn input, string inputArgumentName,
        IEnumerable<byte> bytes, string resultArgumentName)
    {
        var result = bytes.Aggregate(string.Empty, (current, @byte) => current + @byte);
        Console.WriteLine($"{inputArgumentName} : {input}\n" +
                          $"{resultArgumentName} : {result}");
    }
}