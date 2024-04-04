using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.App.Models
{
    internal class FooBar
    {
    }

    public class Foo
    {
        public int UI { get; set; }

        public string Core { get; set; }
    }

    public class FooDto : Foo
    {
    }



    public class Bar
    {
    }


    public class Source
    {
        public int Value { get; set; }
        public int Ävíator { get; set; }
        public int SubAirlinaFlight { get; set; }

        public int frmValue { get; set; }
        public int frmValue2 { get; set; }

        public int SomeValue { get; set; }

        public int Value1 { get; set; }
        public int Value2 { get; set; }
    }

    public class Destination
    {
        public int Value { get; set; }
        public int Aviator { get; set; }
        public int SubAirlineFlight { get; set; }

        public int SomeValuefff { get; set; }

        public int Total { get; set; }
    }

    public class Dest
    {
        public int Value { get; set; }
        public int Value2 { get; set; }
    }

    public class CalendarEvent
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
    }

    public class CalendarEventForm
    {
        public DateTime EventDate { get; set; }
        public int EventHour { get; set; }
        public int EventMinute { get; set; }
        public string Title { get; set; }
    }



}
