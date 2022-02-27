using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Contracts
{
    public interface IWarehouseService
    {
        Task<List<WarehouseModel>> GetAllWarehouse();
        Task<bool> CreateWarehouse(WarehouseModel model);
        bool DeleteWarehouse(int id);
        WarehouseModel GetWarehouse(int id);
        Task<List<WarehouseModel>> GetWarehouseByName(WarehouseModel model);
        Task<bool> UpdateWarehouse(WarehouseModel model);

    }
}
