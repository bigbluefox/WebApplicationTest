using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsp.Test.App.Utility
{
    public class IntroBenchmarkBaseline
    {
        [Benchmark]
        public void Time50() => Thread.Sleep(50);

        [Benchmark(Baseline = true)]
        public void Time100() => Thread.Sleep(100);

        [Benchmark]
        public void Time150() => Thread.Sleep(150);
    }
}
