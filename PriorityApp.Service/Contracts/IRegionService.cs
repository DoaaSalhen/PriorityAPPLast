using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Contracts
{
    public interface IRegionService
    {
        Task<List<MainRegionModel>> GetAllIMainRegions();
        Task<bool> CreateMainRegion(MainRegionModel model);
        Task<bool> UpdateMainRegion(MainRegionModel model);

        bool DeleteMainRegion(int id);
        MainRegionModel GetMainRegion(int id);

        //=============subRegion================
        Task<List<SubRegionModel>> GetAllISubRegions();
        Task<bool> CreateSubRegion(SubRegionModel model);
        Task<bool> UpdateSubRegion(SubRegionModel model);

        bool DeleteSubRegion(int id);
        SubRegionModel GetSubRegion(int id);
        Task<List<SubRegionModel>> GetSubRegionByMainRegionId(int id);
    }
}
