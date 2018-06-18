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
            if (FakeContext == null)
                FakeContext = new List<SampleModel> {
                    new SampleModel { SampleID = 1, Title = "Sample title 01", Description = "Sample description 01", Items=new List<ItemSampleModel>{ } },
                    new SampleModel { SampleID = 2, Title = "Sample title 02", Description = "Sample description 02", Items=new List<ItemSampleModel>{ new ItemSampleModel { ItemSampleID = 1, Description = "Sample 02 - Item 01" }, new ItemSampleModel { ItemSampleID = 2, Description = "Sample 02 - Item 02" } } },
                    new SampleModel { SampleID = 3, Title = "Sample title 03", Description = "Sample description 03", Items=new List<ItemSampleModel>{ } },
                    new SampleModel { SampleID = 4, Title = "Sample title 04", Description = "Sample description 04", Items=new List<ItemSampleModel>{ } },
                    new SampleModel { SampleID = 5, Title = "Sample title 05", Description = "Sample description 05", Items=new List<ItemSampleModel>{ } }
                };
        }

        // GET api/samples
        [HttpGet]
        public IEnumerable<SampleModel> Get()
        {
            return FakeContext.ToList();
        }

        // GET api/samples/5
        [HttpGet("{id}")]
        public SampleModel Get(int id)
        {
            return FakeContext.Where(x => x.SampleID == id).SingleOrDefault();
        }

        // GET api/samples
        [HttpGet("{id}/items")]
        public IEnumerable<ItemSampleModel> GetItems(int id)
        {
            return FakeContext.Where(x => x.SampleID == id).SingleOrDefault().Items.ToList();
        }

        // GET api/samples/5/Items/1
        [HttpGet("{id}/items/{item_id}")]
        public ItemSampleModel GetItems(int id, int item_id)
        {
            return FakeContext.Where(x => x.SampleID == id).SingleOrDefault().Items.Where(x=>x.ItemSampleID == item_id).SingleOrDefault();
        }



        // POST api/samples
        [HttpPost]
        public void Post(SampleModel value)
        {
            value.SampleID = FakeContext.Max(x => x.SampleID)+1;
            FakeContext.Add(value);
        }

        // PUT api/samples/5
        [HttpPut("{id}")]
        public void Put(int id, Wrap<SampleModel> value)
        {
            var sample = FakeContext.Where(x => x.SampleID == id).SingleOrDefault();
            value.Put(sample).SetID(id);
        }

        // PATCH api/samples/5
        [HttpPatch("{id}")]
        public void Patch(int id, Wrap<SampleModel> value)
        {
            var sample = FakeContext.Where(x => x.SampleID == id).SingleOrDefault();
            value.Patch(sample);
        }

        // DELETE api/samples/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var sample = FakeContext.Where(x => x.SampleID == id).SingleOrDefault();
            FakeContext.Remove(sample);
        }
    }
}
