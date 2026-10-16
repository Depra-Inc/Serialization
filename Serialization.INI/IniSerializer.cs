using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Depra.Serialization.Errors;
using SharpConfig;

namespace Depra.Serialization.INI
{
	public sealed class IniSerializer : ITextSerializer
	{
		public string SerializeToString<TIn>(TIn input)
		{
			Guard.AgainstNull(input, nameof(input));
			return SerializeToString(input, input.GetType());
		}

		public string SerializeToString(object input, Type inputType)
		{
			Guard.AgainstNull(input, nameof(input));
			Guard.AgainstNull(inputType, nameof(inputType));

			var sectionName = GetSectionName(inputType) ??
			                  GetSectionName(input.GetType()) ?? Section.DefaultSectionName;

			var configuration = new Configuration();
			if (input is IDictionary dictionary)
			{
				var section = GetSection(configuration, sectionName);
				foreach (DictionaryEntry entry in dictionary)
				{
					section[(string)entry.Key].SetValue(entry.Value);
				}
			}
			else
			{
				configuration.Add(Section.FromObject(sectionName, input));
			}

			return configuration.SaveToString();
		}

		public TOut Deserialize<TOut>(string input) => (TOut)Deserialize(input, typeof(TOut));

		public object Deserialize(string input, Type outputType)
		{
			Guard.AgainstNull(input, nameof(input));
			Guard.AgainstNull(outputType, nameof(outputType));

			var configuration = Configuration.LoadFromString(input);
			var sectionName = GetSectionName(outputType);
			Section section;

			if (string.IsNullOrEmpty(sectionName))
			{
				section = configuration.DefaultSection;
			}
			else
			{
				if (!configuration.Contains(sectionName))
				{
					throw new FormatException($"INI section '{sectionName}' was not found.");
				}

				section = configuration[sectionName];
			}

			if (outputType != typeof(Dictionary<string, object>)
			    && !typeof(IDictionary).IsAssignableFrom(outputType))
			{
				return section.ToObject(outputType);
			}

			var result = (IDictionary)Activator.CreateInstance(outputType);
			foreach (var setting in section)
			{
				result.Add(setting.Name, setting.StringValue);
			}

			return result;
		}

		private static string GetSectionName(Type type) =>
			type.GetCustomAttribute<SerializationSectionAttribute>()?.Name;

		private static Section GetSection(Configuration configuration, string sectionName) =>
			sectionName == Section.DefaultSectionName
				? configuration.DefaultSection
				: configuration[sectionName];
	}
}