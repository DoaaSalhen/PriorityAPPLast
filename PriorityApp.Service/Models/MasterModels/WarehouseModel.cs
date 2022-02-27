using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriorityApp.Service.Models.MasterModels
{
    public class WarehouseModel
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public StateModel State { get; set; }

        public int StateId { get; set; }

        public bool IsDelted { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }


    public class WarehouseModel2
        {
        public List<TerritoryModel> TerritoryModels { get; set; }
        public List<ItemModel> itemModels { get; set; }
        public List<PriorityModel> priorityModels { get; set; }
        public List<WarehouseModel> WarehouseModels { get; set; }
        public int TerritorySelectedId { get; set; }
        public long  WarehouseSelectedId { get; set; }
        public int PrioritySelectedId { get; set; }
        public int Line { get; set; }
        public string Comment { get; set; }
        public int truck { get; set; }

        public bool IsDelted { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}