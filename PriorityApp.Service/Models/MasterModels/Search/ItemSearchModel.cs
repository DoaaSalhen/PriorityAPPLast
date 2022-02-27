using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Service.Models.MasterModels.Search
{
    public class ItemSearchModel
    {
        public string SearchName { get; set; }
        public IEnumerable<ItemModel> ItemModels { get; set; }
        public ItemModel itemModel { get; set; }
    }
}
