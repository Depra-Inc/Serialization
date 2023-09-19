// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using Depra.Serialization.Benchmarks;

var benchmark = BenchmarkSwitcher.FromTypes(new[] {
	typeof(RawSerializationBenchmarks),
});

IConfig configuration = DefaultConfig.Instance.WithOptions(ConfigOptions.DisableOptimizationsValidator)
	.AddJob(Job.ShortRun.WithToolchain(InProcessEmitToolchain.Instance));

if (args.Length > 0)
{
	benchmark.Run(args, configuration);
}
else
{
	benchmark.RunAll(configuration);
}