using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private static List<string> FakeContext { get; set; }

        public ValuesController()
        {
            if (FakeContext == null) FakeContext = new List<string> { "Teste 01" ,"Teste 02", "Teste 03" };
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return FakeContext.ToList();
        }
    }
}
