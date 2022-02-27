using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Contracts
{
    public interface IZoneService
    {
        Task<List<ZoneModel>> GetAllZones();
        Task<bool> CreateZone(ZoneModel model);
        Task<bool> UpdateZone(ZoneModel model);

        bool DeleteZone(int id);
        ZoneModel GetZone(int id);
        List<ZoneModel> GetListOfZonesByTerritoryId(int TerritoryId);

        Task<List<ZoneModel>> GetListOfZonesByTerritoryIds(List<int> TerritoryIds);
        List<int> GetListOfTerritoryIdsByZoneIds(List<int> zoneIds);




    }
}
