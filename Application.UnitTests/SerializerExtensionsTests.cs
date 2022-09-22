// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Depra.Serialization.Application.Extensions;
using Depra.Serialization.Application.Serializers;
using Depra.Serialization.Application.UnitTests.Types;
using FluentAssertions;
using NUnit.Framework;

namespace Depra.Serialization.Application.UnitTests
{
    [TestFixture]
    public class SerializerExtensionsTests
    {
        [Test]
        public void WhenSerializeToStream_AndDeserializeToSourceType_ThenClonedObjectEqualsSource(
            [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetInput))]
            TestInput input,
            [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
            ISerializer serializer)
        {
            // Arrange.
            var sourceData = input.Source;
            var sourceType = input.SourceType;

            // Act.
            object deserializedData;
            using (var stream = serializer.SerializeToStream(sourceData, sourceType))
            {
                deserializedData = serializer.DeserializeFromStream(stream, sourceType);
            }

            // Assert.
            deserializedData.Should().BeEquivalentTo(sourceData);

            Helper.PrintDebugCloneResult(sourceData, deserializedData);
        }

        [Test]
        public void WhenSerializeToBytes_AndDeserializeToSourceType_ThenClonedObjectEqualsSource(
            [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetInput))]
            TestInput input,
            [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
            ISerializer serializer)
        {
            // Arrange.
            var sourceData = input.Source;
            var sourceType = input.SourceType;

            // Act.
            var bytes = serializer.SerializeToBytes(sourceData, sourceType);
            var deserializedData = serializer.DeserializeBytes(bytes, sourceType);

            // Assert.
            deserializedData.Should().BeEquivalentTo(sourceData);

            Helper.PrintDebugCloneResult(sourceData, deserializedData);
        }
    }
}