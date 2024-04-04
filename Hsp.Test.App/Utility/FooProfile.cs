using AutoMapper;
using Hsp.Test.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.App.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class FooProfile : Profile
    {
        // A good way to organize your mapping configurations is with profiles.
        // Create classes that inherit from Profile and put the configuration in the constructor:

        public FooProfile()
        {
            CreateMap<Foo, FooDto>();
            // Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)










        }

        // How it was done in 4.x - as of 5.0 this is obsolete:
        // public class OrganizationProfile : Profile
        // {
        //     protected override void Configure()
        //     {
        //         CreateMap<Foo, FooDto>();
        //     }
        // }


    }

    /// <summary>
    /// To supply a custom value resolver, we’ll need to first create a type that implements IValueResolver:
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <typeparam name="TDestMember"></typeparam>
    public interface IValueResolver<in TSource, in TDestination, TDestMember>
    {
        TDestMember Resolve(TSource source, TDestination destination, TDestMember destMember, ResolutionContext context);
    }

    /// <summary>
    /// The ResolutionContext contains all of the contextual information for the current resolution operation, 
    /// such as source type, destination type, source value and so on. An example implementation:
    /// </summary>
    public class CustomResolver : IValueResolver<Source, Destination, int>
    {
        public int Resolve(Source source, Destination destination, int member, ResolutionContext context)
        {
            return source.Value1 + source.Value2;
        }
    }

    public class MultBy2Resolver : IValueResolver<object, object, int>
    {
        public int Resolve(object source, object dest, int destMember, ResolutionContext context)
        {
            return destMember * 2;
        }
    }

    //public class CustomResolver : IMemberValueResolver<object, object, decimal, decimal>
    //{
    //    public decimal Resolve(object source, object destination, decimal sourceMember, decimal destinationMember, ResolutionContext context)
    //    {
    //        // logic here
    //    }
    //}

}
