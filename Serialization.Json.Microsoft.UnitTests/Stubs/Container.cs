namespace Depra.Serialization.Json.Microsoft.UnitTests.Stubs;

internal sealed record Container(Type Type, object Value)
{
	public readonly Type Type = Type;
	public readonly object Value = Value;

	public override string ToString() => Type.Name;
}