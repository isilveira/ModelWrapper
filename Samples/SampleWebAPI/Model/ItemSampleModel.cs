using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebAPI.Model
{
    public class ItemSampleModel
    {
        public int ItemSampleID { get; set; }
        public string Description { get; set; }

        public ItemSampleModel SetID(int itemSampleID)
        {
            this.ItemSampleID = itemSampleID;
            return this;
        }
    }
}
