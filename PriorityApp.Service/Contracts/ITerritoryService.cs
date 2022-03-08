using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Contracts
{
    public interface ITerritoryService
    {
        Task<List<TerritoryModel>> GetAllTeritories();
        Task<bool> CreateTerritory(TerritoryModel model);
        Task<bool> UpdateTerritory(TerritoryModel model);

        bool DeleteTerritory(int id);
        TerritoryModel GetTerritory(int id);

        List<TerritoryModel> GetTerritoryByUserId(string UserId);
        Task<List<int>> GetTerritoryIdsByStateId(int StateId);

        Task<List<TerritoryModel>> GetAllTerritoriesByStateId(int StateId);
        Task<List<TerritoryModel>> GetAllTerritoriesByListOfStateIds(List<int> StateIds);


    }
}
