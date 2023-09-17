// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Depra.Serialization.Extensions
{
	public static class SerializationInfoExtension
	{
		public static void AddListValue<T>(this SerializationInfo self, string dataName, IList<T> data)
		{
			var count = data.Count;
			self.AddValue(dataName + "_Count", count);

			for (var index = 0; index < count; ++index)
			{
				self.AddValue(dataName + "_[" + index + "]", data[index]);
			}
		}

		public static IList<T> GetListValue<T>(this SerializationInfo self, string dataName)
		{
			var result = new List<T>();
			int? count = null;
			try
			{
				count = self.GetInt32(dataName + "_Count");
			}
			catch
			{
				// ignored
			}

			if (count.HasValue)
			{
				for (var index = 0; index < count.Value; ++index)
				{
					result.Add((T)self.GetValue(dataName + "_[" + index + "]", typeof(T)));
				}
			}
			else
			{
				// Backward compatible.
				try
				{
					result.AddRange((IList<T>)self.GetValue(dataName, typeof(IList<T>)));
				}
				catch
				{
					// ignored
				}
			}

			return result;
		}
	}
}