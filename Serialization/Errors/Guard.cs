// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace Depra.Serialization.Errors
{
	internal static class Guard
	{
		private const string DEBUG_CONDITION = "DEBUG";
		private const string CANT_BE_NULL = "{0} can't be null or empty.";

		[Conditional(DEBUG_CONDITION)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Against(bool condition, Func<Exception> exception)
		{
			if (condition)
			{
				throw exception();
			}
		}

		[Conditional(DEBUG_CONDITION)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AgainstNull<T>(T value, string argumentName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(argumentName);
			}
		}

		[Conditional(DEBUG_CONDITION)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AgainstNullOrEmpty(string argumentValue, string argumentName)
		{
			if (string.IsNullOrEmpty(argumentValue))
			{
				throw new ArgumentException(string.Format(CANT_BE_NULL, argumentName));
			}
		}

		[Conditional(DEBUG_CONDITION)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AgainstNullOrEmpty(Stream argumentValue, string argumentName)
		{
			if (argumentValue is null || argumentValue.Length == 0)
			{
				throw new ArgumentException(string.Format(CANT_BE_NULL, argumentName));
			}
		}

		[Conditional(DEBUG_CONDITION)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AgainstNullOrEmpty<T>(T[] argumentValue, string argumentName)
		{
			if (argumentValue is null || argumentValue.Length == 0)
			{
				throw new ArgumentException(string.Format(CANT_BE_NULL, argumentName));
			}
		}

		[Conditional(DEBUG_CONDITION)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AgainstEmpty<T>(ReadOnlyMemory<T> argumentValue, string argumentName)
		{
			if (argumentValue.Length == 0)
			{
				throw new ArgumentException(string.Format(CANT_BE_NULL, argumentName));
			}
		}
	}
}