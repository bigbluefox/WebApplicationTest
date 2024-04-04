using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.App.Utility
{
    [Config(typeof(Config))]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    public class IntroInProcess
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                AddJob(Job.MediumRun
                    .WithLaunchCount(1)
                    .WithId("OutOfProc"));

                AddJob(Job.MediumRun
                    .WithLaunchCount(1)
                    .WithToolchain(InProcessEmitToolchain.Instance)
                    .WithId("InProcess"));
            }
        }

        [Benchmark(Description = "new byte[10kB]")]
        public byte[] Allocate()
        {
            return new byte[10000];
        }

        [Benchmark(Description = "stackalloc byte[10kB]")]
        public unsafe void AllocateWithStackalloc()
        {
            var array = stackalloc byte[10000];
            Consume(array);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static unsafe void Consume(byte* input)
        {
        }
    }
}
