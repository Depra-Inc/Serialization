// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using System.Text;
using Depra.Serialization.Domain.Extensions;
using Depra.Serialization.Domain.Serializers;

namespace Depra.Serialization.Application.Helpers
{
    internal static class SerializationHelper
    {
        public static byte[] SerializeToBytes<TIn>(ISerializer serializer, TIn obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                serializer.Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        public static string SerializeToString<TIn>(ISerializer serializer, TIn input, Encoding encoding)
        {
            using (var stream = serializer.SerializeToStream(input))
            {
                return encoding.GetString(stream.ToArray());
            }
        }

        public static TOut DeserializeFromString<TOut>(ISerializer serializer, string input, Encoding encoding)
        {
            var byteArray = encoding.GetBytes(input);
            using (var stream = new MemoryStream(byteArray))
            {
                return serializer.Deserialize<TOut>(stream);
            }
        }
    }
}