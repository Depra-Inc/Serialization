// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using BenchmarkDotNet.Validators;

namespace Depra.Serialization.Benchmarks
{
	public class Program
	{
		public void Main(string[] args)
		{
			var benchmark = BenchmarkSwitcher.FromTypes(new[]
			{
				typeof(SerializationBenchmarks),
			});

			IConfig configuration = DefaultConfig.Instance
				.AddJob(Job.Default.WithToolchain(InProcessEmitToolchain.Instance))
				.AddValidator(JitOptimizationsValidator.FailOnError)
				.WithOptions(ConfigOptions.DisableOptimizationsValidator)
				.WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest));

			if (args.Length > 0)
			{
				benchmark.Run(args, configuration);
			}
			else
			{
				benchmark.RunAll(configuration);
			}
		}
	}
}