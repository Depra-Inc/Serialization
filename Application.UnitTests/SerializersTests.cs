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
    public class SerializersTests
    {
        internal static readonly string BadId = string.Empty;

        [Test]
        public void WhenCloneObject_AndSourceIsNotNull_ThenClonedObjectEqualsSource(
            [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetInput))]
            TestInput input,
            [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetSerializers))]
            ISerializer serializer)
        {
            // Arrange.
            var sourceObject = input.Source;
            var sourceType = input.SourceType;

            // Act.
            var clonedObject = serializer.Clone(sourceObject, sourceType);

            // Assert.
            clonedObject.Should().BeEquivalentTo(sourceObject);

            Helper.PrintDebugCloneResult(sourceObject, clonedObject);
        }
    }
}