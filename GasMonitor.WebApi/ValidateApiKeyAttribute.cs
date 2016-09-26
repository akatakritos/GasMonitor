using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GasMonitor.WebApi
{
    public class ValidateApiKeyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.GetValues("X-Api-Key").FirstOrDefault();
            if (authHeader == null || authHeader != ConfigurationManager.AppSettings["ApiKey"])
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("Not authorized. Did you forget to include the right X-Api-Key header?")
                };
            }

        }
    }
}