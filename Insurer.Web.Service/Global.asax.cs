using Insurer.Data;
using Insurer.Web.Service;
using Insurer.Web.Service.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Insurer.Web.Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Init database
            System.Data.Entity.Database.SetInitializer(new InsurerSeedData());

            //GlobalConfiguration.Configure(WebApiConfig.Register);

            //GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);


            AutoMapperConfiguration.Configure();
        }
    }
}
