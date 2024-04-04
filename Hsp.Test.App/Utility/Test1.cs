using AutoMapper;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Hsp.Test.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.App.Utility
{
    [SimpleJob(runtimeMoniker: RuntimeMoniker.Net472)]
    public class Test1
    {

        private static readonly MapperConfiguration configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TestA, TestB>();
        });
        private static readonly IMapper mapper = configuration.CreateMapper();

        private readonly TestA a = new TestA
        {
            A = 1,
            B = "aaa",
            C = 1,
            D = "aaa",
            E = 1,
            F = "aaa",
            G = 1,
            H = "aaa",
        };
        [Benchmark]
        public TestB Get1()
        {
            return new TestB { A = a.A, B = a.B, C = a.C, D = a.D, E = a.E, F = a.F, G = a.G, H = a.H };
        }
        [Benchmark]
        public TestB Get2()
        {
            return mapper.Map<TestB>(a);
        }
        [Benchmark]
        public TestA Get3()
        {
            return mapper.Map<TestA>(a);
        }
    }
}
