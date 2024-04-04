using AutoMapper;
using Hsp.Demo.Web.Models;
using Hsp.Demo.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Hsp.Demo.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";


            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MyMappingProfile>();
            });

            IMapper mapper = configuration.CreateMapper();

            

            // 创建源对象
            var source = new SourceClass { Id = 1, Name = "Test" };

            // 使用AutoMapper进行映射
            var destination = mapper.Map<DestinationClass>(source);
            //Mapper.Map<DestinationClass>(source);

            var abcd = destination;




            return View(destination);
        }
    }
}