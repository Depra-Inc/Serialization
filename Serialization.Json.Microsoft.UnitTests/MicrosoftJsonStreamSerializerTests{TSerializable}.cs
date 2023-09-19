// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Depra.Serialization.Json.Microsoft.UnitTests;

[TestFixture(typeof(SerializableClass))]
[TestFixture(typeof(SerializableStruct))]
[TestFixture(typeof(SerializableRecord))]
internal sealed class MicrosoftJsonStreamSerializerTests<TSerializable> where TSerializable : new()
{
	private IStreamSerializer _serializer;

	[SetUp]
	public void Setup() => _serializer = new MicrosoftJsonSerializer();

	[Test]
	public void SerializeToStream_AndDeserializeToInputType_ThenResultEqualsInput()
	{
		// Arrange.
		var input = new TSerializable();
		using var stream = new MemoryStream();

		// Act.
		_serializer.Serialize(stream, input);
		var deserialized = _serializer.Deserialize<TSerializable>(stream);

		// Assert.
		deserialized.Should().BeEquivalentTo(input);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}

	[Test]
	public async Task SerializeToStreamAsync_AndDeserializeAsyncToInputType_ThenResultEqualsInput()
	{
		// Arrange.
		var input = new TSerializable();
		await using var stream = new MemoryStream();

		// Act.
		await _serializer.SerializeAsync(stream, input);
		var deserialized = await _serializer.DeserializeAsync<TSerializable>(stream);

		// Assert.
		deserialized.Should().BeEquivalentTo(input);

		// Debug.
		TestContext.WriteLine($"{nameof(input)} : {input}\n" +
		                      $"{nameof(deserialized)} : {deserialized}");
	}
}