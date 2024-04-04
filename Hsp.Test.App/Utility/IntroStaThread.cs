using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsp.Test.App.Utility
{
    public class IntroStaThread
    {
        [Benchmark, System.STAThread]
        public void CheckForSTA()
        {
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
            {
                throw new ThreadStateException(
                    "The current threads apartment state is not STA");
            }
        }
    }
}
