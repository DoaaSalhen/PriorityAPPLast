using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriorityApp.Service.Models.MasterModels
{
    public class ItemModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool QutaCalc { get; set; }
       
        public bool HMP { get; set; }
        [Required]
        public int OldItemNumber { get; set; }
        public bool IsDelted { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string SearchName { get; set; }
        public List<ItemModel> itemModels { get; set; }
        public ItemModel itemModel { get; set; }
        public bool SearchHPM { get; set; }
        public DateTime SearchCreatedDate { get; set; }
        public DateTime SearchUpdatedDate { get; set; }

        public float Quantity { get; set; }

        public string type { get; set; }


    }
}
