using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Models.MasterModels
{
    public class MainRegionModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        public bool IsDelted { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public static implicit operator MainRegionModel(Task<List<MainRegionModel>> v)
        {
            throw new NotImplementedException();
        }
    }
}
