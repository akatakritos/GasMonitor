using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GasMonitor.WebApi
{
    public class ValidateApiKeyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            IEnumerable<string> values;
            var found = actionContext.Request.Headers.TryGetValues("X-Api-Key", out values);
            if (!found || values == null || values.FirstOrDefault() != ConfigurationManager.AppSettings["ApiKey"])
            {
                actionContext.Response = Unauthorized();
            }

            base.OnActionExecuting(actionContext);
        }

        private HttpResponseMessage Unauthorized()
        {
            return new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new StringContent("Not authorized. Did you forget to include the right X-Api-Key header?")
            };
        }
    }
}