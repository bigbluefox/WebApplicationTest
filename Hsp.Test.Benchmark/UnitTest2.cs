using AutoMapper;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Hsp.Test.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Hsp.Test.Tests
{
    [TestClass]
    [SimpleJob(runtimeMoniker: RuntimeMoniker.Net472)]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod2()
        {
            //Assert.Fail();
            // Complex model

            var customer = new Customer
            {
                Name = "George Costanza"
            };
            var order = new Order
            {
                Customer = customer
            };
            var bosco = new Product
            {
                Name = "Bosco",
                Price = 4.99m
            };
            order.AddOrderLineItem(bosco, 15);

            // Configure AutoMapper

            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDto>());

            // Perform mapping
            IMapper mapper = configuration.CreateMapper();
            OrderDto dto = mapper.Map<Order, OrderDto>(order);

            var expected = dto.CustomerName;
            var actual = "George Costanza";
            Assert.AreEqual(expected, actual);

            Assert.AreEqual(dto.Total, 74.85m);
        }

        /*
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
        */
    }
}
