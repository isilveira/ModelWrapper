using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebAPI.Model
{
    public class SampleModel
    {
        public int SampleID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ItemSampleModel> Items { get; set; }

        public SampleModel SetID(int sampleID)
        {
            this.SampleID = sampleID;
            return this;
        }
    }
}
