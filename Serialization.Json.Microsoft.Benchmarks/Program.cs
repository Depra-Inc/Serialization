﻿// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using BenchmarkDotNet.Validators;

namespace Depra.Serialization.Json.Microsoft.Benchmarks;

internal static class Program
{
	private static void Main() =>
		BenchmarkRunner.Run(typeof(Program).Assembly, DefaultConfig.Instance
			.AddValidator(JitOptimizationsValidator.FailOnError)
			.AddJob(Job.ShortRun.WithToolchain(InProcessNoEmitToolchain.Instance))
			.AddDiagnoser(MemoryDiagnoser.Default)
			.WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest)));
}