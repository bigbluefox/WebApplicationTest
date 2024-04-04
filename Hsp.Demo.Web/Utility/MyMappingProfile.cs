using AutoMapper;
using Hsp.Demo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hsp.Demo.Web.Utility
{
    public class MyMappingProfile : Profile
    {
        // https://cloud.tencent.com/developer/article/1817891?from=15425
        // https://docs.automapper.org/en/latest/

        public MyMappingProfile()
        {
            //CreateMap<SourceClass, DestinationClass>();


            // SourceClass -> DestinationClass
            CreateMap<SourceClass, DestinationClass>()
            // 左边是 DestinationClass 的字段，右边是为字段赋值的逻辑
            //.ForMember(b => b.Id, cf => cf.MapFrom(a => a.Id))
            //.ForMember(b => b.Name, cf => cf.MapFrom(a => a.Name))
            .ForMember(b => b.KeyId, cf => cf.MapFrom(a => Guid.Parse(a.KeyId)));

            ;
        }
    }

}