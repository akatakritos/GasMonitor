using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

using GasMonitor.WebApi.Models;

namespace GasMonitor.WebApi.Controllers
{
    public class StatusController : ApiController
    {
        private static Lazy<string> _gitHash;
        public static string GitHash => _gitHash.Value;

        static StatusController()
        {
            _gitHash = new Lazy<string>(ComputeGitHash);
        }

        private static string ComputeGitHash()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GasMonitor.WebApi.githash.txt"))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd().Trim();
            }
        }

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
