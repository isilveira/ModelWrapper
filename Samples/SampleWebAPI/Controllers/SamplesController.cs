using Microsoft.AspNetCore.Mvc;
using ModelWrapper;
using SampleWebAPI.Model;
using System.Collections.Generic;
using System.Linq;

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
        [HttpGet("{sampleID}")]
        public SampleModel Get(int sampleID)
        {
            return FakeContext.SingleOrDefault(x => x.SampleID == sampleID);
        }

        // POST api/samples
        [HttpPost]
        public void Post([FromBody]SampleModel value)
        {
            int? lastID = (FakeContext.Any() ? FakeContext.Max(x => x.SampleID) : 0);
            value.SampleID = (lastID.HasValue ? lastID.Value : 0) + 1;

            if (value.Items != null && value.Items.Count > 0)
            {
                int? lastItemID = (FakeContext.SelectMany(x => x.Items).Any() ? FakeContext.SelectMany(x => x.Items).Max(x => x.ItemSampleID) : 0);

                value.Items.ForEach(x =>
                {
                    lastItemID = (lastItemID.HasValue ? lastItemID.Value : 0) + 1;
                    x.ItemSampleID = lastItemID.Value;
                });
            }

            FakeContext.Add(value);
        }

        // PUT api/samples/5
        [HttpPut("{sampleID}")]
        public void Put(int sampleID, [FromBody]Wrap<SampleModel> value)
        {
            var model = FakeContext.SingleOrDefault(x => x.SampleID == sampleID);
            value.Put(model).SetID(sampleID);
        }

        // PATCH api/samples/5
        [HttpPatch("{sampleID}")]
        public void Patch(int sampleID, [FromBody]Wrap<SampleModel> value)
        {
            var model = FakeContext.SingleOrDefault(x => x.SampleID == sampleID);
            
            value.Patch(model);
        }

        // DELETE api/samples/5
        [HttpDelete("{sampleID}")]
        public void Delete(int sampleID)
        {
            var model = FakeContext.SingleOrDefault(x => x.SampleID == sampleID);
            FakeContext.Remove(model);
        }

        // GET api/samples
        [HttpGet("{sampleID}/items")]
        public IEnumerable<ItemSampleModel> GetItems(int sampleID)
        {
            return FakeContext.SingleOrDefault(x => x.SampleID == sampleID).Items.ToList();
        }

        // GET api/samples/5/Items/1
        [HttpGet("{sampleID}/items/{itemSampleID}")]
        public ItemSampleModel GetItems(int sampleID, int itemSampleID)
        {
            return FakeContext.SingleOrDefault(x => x.SampleID == sampleID).Items.SingleOrDefault(x => x.ItemSampleID == itemSampleID);
        }

        // POST api/samples/5/items
        [HttpPost("{sampleID}/items")]
        public void PostItems(int sampleID, [FromBody]ItemSampleModel value)
        {
            int? lastID = (FakeContext.SelectMany(x => x.Items).Any() ? FakeContext.SelectMany(x => x.Items).Max(x => x.ItemSampleID) : 0);
            value.ItemSampleID = (lastID.HasValue ? lastID.Value : 0) + 1;
            FakeContext.SingleOrDefault(x => x.SampleID == sampleID).Items.Add(value);
        }

        // PUT api/samples/5/items/2
        [HttpPut("{sampleID}/items/{itemSampleID}")]
        public void Put(int sampleID, int itemSampleID, [FromBody]Wrap<ItemSampleModel> value)
        {
            var model = FakeContext.SingleOrDefault(x => x.SampleID == sampleID).Items.SingleOrDefault(x => x.ItemSampleID == itemSampleID);
            value.Put(model).SetID(itemSampleID);
        }

        // PATCH api/samples/5/items/2
        [HttpPatch("{sampleID}/items/{itemSampleID}")]
        public void Patch(int sampleID, int itemSampleID, [FromBody]Wrap<ItemSampleModel> value)
        {
            var model = FakeContext.SingleOrDefault(x => x.SampleID == sampleID).Items.SingleOrDefault(x => x.ItemSampleID == itemSampleID);
            value.Patch(model);
        }

        // DELETE api/samples/5/items/2
        [HttpDelete("{sampleID}/items/{itemSampleID}")]
        public void Delete(int sampleID, int itemSampleID)
        {
            var model = FakeContext.SingleOrDefault(x => x.SampleID == sampleID).Items.SingleOrDefault(x => x.ItemSampleID == itemSampleID);
            FakeContext.SingleOrDefault(x => x.SampleID == sampleID).Items.Remove(model);
        }
    }
}
