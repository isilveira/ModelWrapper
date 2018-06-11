using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelWrapper;
using SampleWebAPI.Model;

namespace SampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class SamplesController : Controller
    {
        private static List<SampleModel> FakeContext { get; set; }

        public SamplesController()
        {
            if (FakeContext == null) FakeContext = new List<SampleModel> { new SampleModel { SampleID = 1, Description = "Sample 01" }, new SampleModel { SampleID = 2, Description = "Sample 02" } };
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<SampleModel> Get()
        {
            return FakeContext.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public SampleModel Get(int id)
        {
            return FakeContext.Where(x => x.SampleID == id).SingleOrDefault();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]SampleModel value)
        {
            value.SampleID = FakeContext.Max(x => x.SampleID)+1;
            FakeContext.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromForm]Wrap<SampleModel> value)
        {
            var sample = FakeContext.Where(x => x.SampleID == id).SingleOrDefault();
            value.Put(sample);
        }

        // PATCH api/values/5
        [HttpPatch("{id}")]
        public void Patch(int id, [FromBody]Wrap<SampleModel> value)
        {
            var sample = FakeContext.Where(x => x.SampleID == id).SingleOrDefault();
            value.Patch(sample);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var sample = FakeContext.Where(x => x.SampleID == id).SingleOrDefault();
            FakeContext.Remove(sample);
        }
    }
}
