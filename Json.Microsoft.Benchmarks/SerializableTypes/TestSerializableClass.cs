// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Depra.Serialization.Json.Microsoft.Benchmarks.SerializableTypes;

public class TestSerializableClass
{
    public string Id { get; set; }
    
    public TestSerializableClass(string id) => Id = id;
    
    public override string ToString() => Id;
}