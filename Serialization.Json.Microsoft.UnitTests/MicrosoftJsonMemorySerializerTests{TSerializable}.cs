// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Serialization.Json.Microsoft.UnitTests;

[TestFixture(typeof(SerializableClass))]
[TestFixture(typeof(SerializableStruct))]
[TestFixture(typeof(SerializableRecord))]
internal sealed class MicrosoftJsonMemorySerializerTests<TSerializable> where TSerializable : new()
{
	private IMemoryDeserializer _serializer;

	[SetUp]
	public void Setup() => _serializer = new MicrosoftJsonSerializer();
}