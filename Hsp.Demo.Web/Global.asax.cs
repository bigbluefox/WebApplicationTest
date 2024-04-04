using AutoMapper;
using Hsp.Demo.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Hsp.Demo.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //// ◊¢≤·”≥…‰≈‰÷√
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<MyMappingProfile>();
            //});

            //// ≥ı ºªØAutoMapper
            //config.AssertConfigurationIsValid();
            //config.Initialize();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
