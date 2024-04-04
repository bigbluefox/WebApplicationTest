using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.App.Utility
{

    public class Test2
    {
        [Benchmark] public void TestEmpty() => string.IsNullOrEmpty("");
        [Benchmark] public void TestWhiteSpace() => string.IsNullOrWhiteSpace("");
    }
}
