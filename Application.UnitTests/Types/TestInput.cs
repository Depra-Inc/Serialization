// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Depra.Serialization.Application.UnitTests.Types
{
    public readonly struct TestInput
    {
        public object Source { get; }

        public Type SourceType { get; }

        public TestInput(object source, Type sourceType)
        {
            Source = source;
            SourceType = sourceType;
        }

        public override string ToString() => SourceType.Name;
    }
}