using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Core.Managers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDD.Api.Controllers
{
    public class ValuesController : BaseController
    {
        private readonly IProfileManager _profileManager;

        public ValuesController(ILogger logger, IHostingEnvironment env) : base(logger, env)
        {

        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
    }
}
