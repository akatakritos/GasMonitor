using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;

using GasMonitor.WebApi.Models;

namespace GasMonitor.WebApi.Controllers
{
    public class StatusController : ApiController
    {
        private static readonly Lazy<string> _gitHash;
        public static string GitHash => _gitHash.Value;

        static StatusController()
        {
            _gitHash = new Lazy<string>(ComputeGitHash);
        }

        private static string ComputeGitHash()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GasMonitor.WebApi.githash.txt"))
            {
                Debug.Assert(stream != null, "githash was not included as embedded resource");
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd().Trim();
                }
            }
        }

        /// <summary>
        ///     Gets the status of the API.
        /// </summary>
        /// <remarks>This is a good endpoint to test your client against.</remarks>
        /// <returns></returns>
        /// <response code="200">Server is up and running</response>
        /// <response code="401">Missing or incorrect X-Api-Key header</response>
        [Route("status")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            var uptime = DateTime.Now - Process.GetCurrentProcess().StartTime;

            return Ok(new Status()
            {
                Uptime = uptime,
                Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                Commit = GitHash
            });
        }
    }
}