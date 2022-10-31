// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Depra.Serialization.Json.Microsoft.Benchmarks.SerializableTypes;

public class SerializableClass
{
    public readonly string Id;
    
    public SerializableClass(string id) => Id = id;
    
    public override string ToString() => Id;
}