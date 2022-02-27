using PriorityApp.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Contracts
{
    public interface IWarehouseOrderHoldService
    {
        Task<bool> CreateWarehouseOrderHold(WarehouseOrderHoldModel model);
        WarehouseOrderHoldModel GetWarehouseOrderHold(long id);
        List<WarehouseOrderHoldModel> GetWarehouseOrderHold(string userId, DateTime PriorityDate);

    }
}
