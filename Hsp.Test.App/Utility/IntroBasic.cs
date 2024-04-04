using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsp.Test.App.Utility
{
    public class IntroBasic
    {
        // And define a method with the Benchmark attribute
        [Benchmark]
        public void Sleep() => Thread.Sleep(10);

        // You can write a description for your method.
        [Benchmark(Description = "Thread.Sleep(10)")]
        public void SleepWithDescription() => Thread.Sleep(10);
    }
}
