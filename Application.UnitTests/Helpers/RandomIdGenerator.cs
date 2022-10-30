using System;
using System.Linq;

namespace Depra.Serialization.Application.UnitTests.Helpers;

internal static class RandomIdGenerator
{
    internal static string Generate()
    {
        const int length = 10;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}