using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Contracts
{
    public interface IPriorityService
    {
        Task<List<PriorityModel>> GetAllPriorities();
        Task<List<PriorityModel>> GetAllPrioritiesExceptExtra();

        Task<bool> CreatePriority(PriorityModel model);
        Task<bool> UpdatePriority(PriorityModel model);
        bool DeletePriority(int id);
        ItemModel GetPriority(int id);
        Task<List<PriorityModel>> GetPriorityByName(PriorityModel model);
    }
}
