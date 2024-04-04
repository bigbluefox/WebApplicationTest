using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsp.Test.App.Utility
{
    [ShortRunJob]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn(NumeralSystem.Arabic)]
    [RankColumn(NumeralSystem.Roman)]
    [RankColumn(NumeralSystem.Stars)]
    public class IntroRankColumn
    {
        [Params(1, 2)]
        public int Factor;

        [Benchmark]
        public void Foo() => Thread.Sleep(Factor * 100);

        [Benchmark]
        public void Bar() => Thread.Sleep(Factor * 200);
    }
}
