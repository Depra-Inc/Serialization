// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Depra.Serialization.Application.Binary;
using Depra.Serialization.Application.Extensions;
using Depra.Serialization.Application.Services;
using Depra.Serialization.Application.UnitTests.Types;
using FluentAssertions;
using NUnit.Framework;

namespace Depra.Serialization.Application.UnitTests
{
    [TestFixture]
    public class SerializationServiceTests
    {
        private ISerializationService _serializationService;

        [OneTimeSetUp]
        public void Setup()
        {
            var serializer = new BinarySerializer();
            _serializationService = new SerializationService(serializer, serializer);
        }

        [Test]
        public void WhenClone_AndSourceIsNotNull_ThenClonedObjectEqualsSource(
            [ValueSource(typeof(SerializationTestsFactory), nameof(SerializationTestsFactory.GetInput))]
            TestInput input)
        {
            // Arrange.
            var service = _serializationService;
            var sourceObject = input.Source;
            var sourceType = input.SourceType;

            // Act.
            var clonedObject = service.Clone(sourceObject, sourceType);

            // Assert.
            clonedObject.Should().BeEquivalentTo(sourceObject);
            
            Helper.PrintDebugCloneResult(sourceObject, clonedObject);
        }
    }
}