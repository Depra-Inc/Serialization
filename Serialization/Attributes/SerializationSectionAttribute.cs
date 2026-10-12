using System;

namespace Depra.Serialization
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class SerializationSectionAttribute : Attribute
	{
		public SerializationSectionAttribute(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("Section name cannot be empty.", nameof(name));
			}

			Name = name;
		}

		public string Name { get; }
	}
}