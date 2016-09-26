using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

using GasMonitor.Core.Database;
using GasMonitor.Core.Migrations;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace GasMonitor.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<GasMonitorContext, Configuration>());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfig.Configure();

            var config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
    }
}
