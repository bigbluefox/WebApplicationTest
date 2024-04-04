using AgileObjects.ReadableExpressions;
using AutoMapper;
using BenchmarkDotNet.Running;
using Hsp.Test.App.Models;
using Hsp.Test.App.Utility;
using Microsoft.Diagnostics.Tracing.StackSources;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Hsp.Test.App
{
    internal class Program
    {
        [Fact]
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, AutoMapper");
            Console.WriteLine("");

            {
                //BenchmarkRunner.Run<Test1>();

                //BenchmarkRunner.Run<IntroRankColumn>();

                //BenchmarkRunner.Run(typeof(Test2));
            }

            {
                // Demonstrates how to view an AutoMapper execution plan using the ReadableExpressions 
                // NuGet package. For a full write-up, visit:
                // https://agileobjects.co.uk/view-automapper-execution-plan-readableexpressions


                // Create a MapperConfiguration instance and initialize configuration via the constructor:
                var config = new MapperConfiguration(cfg =>
                {
                    // Profiles can be added to the main mapper configuration in a number of ways, either directly:
                    cfg.CreateMap<Foo, Bar>();
                    cfg.AddProfile<FooProfile>();
                });

                // The MapperConfiguration instance can be stored statically, in a static field
                // or in a dependency injection container. Once created it cannot be changed/modified.

                var configuration1 = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Foo, Bar>();
                    cfg.AddProfile<FooProfile>();
                });

                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Wedding, WeddingDto>();
                });

                // This retrieves the execution plan Expression which can best be viewed
                // in Visual Studio by installing the ReadableExpressions Debugger Visualizers from
                // https://marketplace.visualstudio.com/items?itemName=vs-publisher-1232914.ReadableExpressionsVisualizers,
                // mousing over the 'executionPlan' variable and clicking the magnifying glass:
                var executionPlan = configuration.BuildExecutionPlan(typeof(Wedding), typeof(WeddingDto));

                // This generates a string version of the execution plan which is printed
                // in the output when you run this fiddle:
                var description = executionPlan.ToReadableString();

                //Console.WriteLine(description);

                //var expression = context.Entities.ProjectTo<Dto>().Expression;


            }

            {
                //or by automatically scanning for profiles:

                /*
                // Scan for all profiles in an assembly
                // ... using instance approach:
                var config = new MapperConfiguration(cfg => {
                    cfg.AddMaps(myAssembly);
                });
                var configuration = new MapperConfiguration(cfg => cfg.AddMaps(myAssembly));

                // Can also use assembly names:
                var configuration = new MapperConfiguration(cfg =>
                    cfg.AddMaps(new[] {
                        "Foo.UI",
                        "Foo.Core"
                                    });
                );

                // Or marker types for assemblies:
                var configuration = new MapperConfiguration(cfg =>
                    cfg.AddMaps(new[] {
                    typeof(HomeController),
                    typeof(Entity)
                    });
                );
                */

                // AutoMapper will scan the designated assemblies for classes inheriting from Profile and add them to the configuration.



            }


            {
                // Replacing characters
                //You can also replace individual characters or entire words in source members during member name matching:

                var configuration = new MapperConfiguration(c =>
                {
                    c.ReplaceMemberName("Ä", "A");
                    c.ReplaceMemberName("í", "i");
                    c.ReplaceMemberName("Airlina", "Airline");
                });

            }

            {
                // Recognizing pre/postfixes
                // Sometimes your source/destination properties will have common pre/postfixes that cause you to
                // have to do a bunch of custom member mappings because the names don’t match up. To address this,
                // you can recognize pre/postfixes:


                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.RecognizePrefixes("frm");
                    cfg.CreateMap<Source, Dest>();
                });
                configuration.AssertConfigurationIsValid();

                // By default AutoMapper recognizes the prefix “Get”, if you need to clear the prefix:

                var configuration1 = new MapperConfiguration(cfg =>
                {
                    cfg.ClearPrefixes();
                    cfg.RecognizePrefixes("tmp");
                });


            }

            {
                // Global property/field filtering
                // By default, AutoMapper tries to map every public property/field. You can filter out properties/fields with the property/field filters:

                var configuration = new MapperConfiguration(cfg =>
                {
                    // don't map any fields
                    cfg.ShouldMapField = fi => false;

                    // map properties with a public or private getter
                    cfg.ShouldMapProperty = pi =>
                        pi.GetMethod != null && (pi.GetMethod.IsPublic || pi.GetMethod.IsPrivate);
                });

            }

            {
                var configuration = new MapperConfiguration(cfg =>
                  cfg.CreateMap<Source, Destination>()
                    .ForMember(dest => dest.SomeValuefff, opt => opt.Ignore())
                );

            }

            {
                //var configuration = new MapperConfiguration(cfg =>
                //   cfg.CreateMap<Source, Destination>()
                //     .ForMember(dest => dest.Total, opt => opt.MapFrom<CustomResolver>()));

                //configuration.AssertConfigurationIsValid();

                //var source = new Source
                //{
                //    Value1 = 5,
                //    Value2 = 7
                //};

                //var result = mapper.Map<Source, Destination>(source);

                //result.Total.ShouldEqual(12);

                //var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Source, Destination>()
                //.ForMember(dest => dest.Total,
                //    opt => opt.MapFrom(new CustomResolver())
                //));

                //var configuration = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<Source, Destination>()
                //        .ForMember(dest => dest.Total,
                //            opt => opt.MapFrom<CustomResolver, decimal>(src => src.SubTotal));
                //    cfg.CreateMap<OtherSource, OtherDest>()
                //        .ForMember(dest => dest.OtherTotal,
                //            opt => opt.MapFrom<CustomResolver, decimal>(src => src.OtherSubTotal));
                //});
            }

            {
                // Model
                var calendarEvent = new CalendarEvent
                {
                    Date = new DateTime(2008, 12, 15, 20, 30, 0),
                    Title = "Company Holiday Party"
                };

                // Configure AutoMapper
                var configuration = new MapperConfiguration(cfg =>
                  cfg.CreateMap<CalendarEvent, CalendarEventForm>()
                    .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.Date.Date))
                    .ForMember(dest => dest.EventHour, opt => opt.MapFrom(src => src.Date.Hour))
                    .ForMember(dest => dest.EventMinute, opt => opt.MapFrom(src => src.Date.Minute)));

                IMapper mapper = configuration.CreateMapper();

                // Perform mapping
                CalendarEventForm form = mapper.Map<CalendarEvent, CalendarEventForm>(calendarEvent);

                //form.EventDate.ShouldEqual(new DateTime(2008, 12, 15));
                //form.EventHour.ShouldEqual(20);
                //form.EventMinute.ShouldEqual(30);
                //form.Title.ShouldEqual("Company Holiday Party");

            }

            {

                // Lists and Arrays
                // AutoMapper only requires configuration of element types, not of any array
                // or list type that might be used.For example, we might have a simple source and destination type:
                // All the basic generic collection types are supported:

                var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Source, Destination>());

                var sources = new[]
                    {
                        new Source { Value = 5 },
                        new Source { Value = 6 },
                        new Source { Value = 7 }
                    };

                IMapper mapper = configuration.CreateMapper();

                IEnumerable<Destination> ienumerableDest = mapper.Map<Source[], IEnumerable<Destination>>(sources);
                ICollection<Destination> icollectionDest = mapper.Map<Source[], ICollection<Destination>>(sources);
                IList<Destination> ilistDest = mapper.Map<Source[], IList<Destination>>(sources);
                List<Destination> listDest = mapper.Map<Source[], List<Destination>>(sources);
                Destination[] arrayDest = mapper.Map<Source[], Destination[]>(sources);

                // To be specific, the source collection types supported include:
                // IEnumerable，IEnumerable<T>，ICollection，ICollection<T>，IList，IList<T>，List<T>，Arrays

                // Handling null collections

                var configuration1 = new MapperConfiguration(cfg => {
                    cfg.AllowNullCollections = true;
                    cfg.CreateMap<Source, Destination>();
                });

            }

            {
                // Polymorphic element types in collections
                // 集合中的多态元素类型
                // 很多时候，我们可能在源类型和目标类型中都有一个类型层次结构。
                // AutoMapper支持多态数组和集合，以便在找到派生的源/目标类型时使用。
                // AutoMapper仍然需要显式配置子映射，因为AutoMapper无法“猜测”要使用哪个特定的子目标映射。

                var configuration = new MapperConfiguration(c => {
                    c.CreateMap<ParentSource, ParentDestination>()
                         .Include<ChildSource, ChildDestination>();
                    c.CreateMap<ChildSource, ChildDestination>();
                });

                var sources = new[]
                    {
                        new ParentSource(),
                        new ChildSource(),
                        new ParentSource()
                    };

                IMapper mapper = configuration.CreateMapper();

                var destinations = mapper.Map<ParentSource[], ParentDestination[]>(sources);

                //destinations[0].ShouldBeInstanceOf<ParentDestination>();
                //destinations[1].ShouldBeInstanceOf<ChildDestination>();
                //destinations[2].ShouldBeInstanceOf<ParentDestination>();

            }

            {
                //// Complex model

                //var customer = new Customer
                //{
                //    Name = "George Costanza"
                //};
                //var order = new Order
                //{
                //    Customer = customer
                //};
                //var bosco = new Product
                //{
                //    Name = "Bosco",
                //    Price = 4.99m
                //};
                //order.AddOrderLineItem(bosco, 15);

                //// Configure AutoMapper

                //var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDto>());

                //// Perform mapping
                //IMapper mapper = configuration.CreateMapper();
                //OrderDto dto = mapper.Map<Order, OrderDto>(order);

                ////var expected = dto.CustomerName;
                ////var actual = "George Costanza";
                ////Assert.Equal(expected, actual);

                ////Assert.Equal(dto.Total, 74.85m);

                ////dto.CustomerName.ShouldEqual("George Costanza");
                ////dto.Total.ShouldEqual(74.85m);
            }








            Console.WriteLine("Bye, AutoMapper");
            Console.WriteLine();
            Console.ReadKey();
        }




        // Source types:
        class Wedding
        {
            public DateTime Date { get; set; }

            public Person Bride { get; set; }

            public Person Groom { get; set; }
        }

        class Person
        {
            public Title Title { get; set; }

            public string Name { get; set; }

            public Address Address { get; set; }
        }

        class Address
        {
            public string Line1 { get; set; }
        }

        enum Title
        {
            Other, Mr, Ms, Miss, Mrs, Dr
        }

        // Target type:
        class WeddingDto
        {
            public DateTime Date { get; set; }

            public string BrideTitle { get; set; }

            public string BrideName { get; set; }

            public string BrideAddressLine1 { get; set; }

            public string GroomTitle { get; set; }

            public string GroomName { get; set; }

            public string GroomAddressLine1 { get; set; }
        }
    }







}

/*
 * BenchmarkDotNet 的主要功能包括: 简化基准测试的编写, 通过属性来标记要测试的方法 支持多种基准测试模式, 
 * 如平均时间、内存分配等 自动进行基准测试的运行和统计 生成详细的基准报告, 包括表格、图形等 支持基准结果的持久化, 
 * 可以比较不同版本的性能 丰富的配置选项, 可以自定义基准测试的细节
 * 
 * 
Method(测试方法的名称为CreateTuple)。
Mean（测试运行的平均时间为420.7纳秒）。
Error(测试运行的标准误差为16.96纳秒)。
StdDev(所有测试运行的标准偏差为1630纳秒)。
Median(所有测试运行的中位数为300纳秒)。

Method: 测试方法的名称。
Mean: 所有测试运行的平均时间。
Error: 测试运行的标准误差，标准误差是测试结果的离散程度的度量，标准误差越小，表示测试结果越稳定。
StdDev: 所有测试运行的标准偏差，标准偏差是测试结果的离散程度的度量，标准偏差越小，表示测试结果越接近平均值。
Median: 所有测试运行的中位数。中位数是测试结果的中间值，如果测试结果的个数为奇数，则中位数为中间的那个值；如果测试结果的个数为偶数，则中位数为中间两个值的平均值。
Ratio: 每个测试运行的平均时间与基准测试运行的平均时间的比值。基准测试是性能最好的测试，它的比值为 1.0。其他测试的比值表示它们相对于基准测试的性能表现，比值越小，表示性能越好。
RatioSD: 所有测试运行的比值的标准偏差。标准偏差越小，表示比值的离散程度越小，测试结果更稳定。
Gen 0: 所有测试运行期间生成的第 0 代垃圾回收的次数。垃圾回收是 .NET 运行时自动回收不再使用的内存的机制，Generational Garbage Collection 是 .NET 中的一种垃圾回收算法。
Gen 1: 所有测试运行期间生成的第 1 代垃圾回收的次数。
Gen 2: 所有测试运行期间生成的第 2 代垃圾回收的次数。
Allocated: 所有测试运行期间分配的内存总量。

*/
