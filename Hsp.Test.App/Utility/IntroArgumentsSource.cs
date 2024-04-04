﻿using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsp.Test.App.Utility
{
    public class IntroArgumentsSource
    {
        [Benchmark]
        [ArgumentsSource(nameof(Numbers))]
        public double ManyArguments(double x, double y) => Math.Pow(x, y);

        public IEnumerable<object[]> Numbers() // for multiple arguments it's an IEnumerable of array of objects (object[])
        {
            yield return new object[] { 1.0, 1.0 };
            yield return new object[] { 2.0, 2.0 };
            yield return new object[] { 4.0, 4.0 };
            yield return new object[] { 10.0, 10.0 };
        }

        [Benchmark]
        [ArgumentsSource(nameof(TimeSpans))]
        public void SingleArgument(TimeSpan time) => Thread.Sleep(time);

        public IEnumerable<object> TimeSpans() // for single argument it's an IEnumerable of objects (object)
        {
            yield return TimeSpan.FromMilliseconds(10);
            yield return TimeSpan.FromMilliseconds(100);
        }
    }
}
