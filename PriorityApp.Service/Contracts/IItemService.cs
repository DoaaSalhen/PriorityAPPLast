using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Contracts
{
    public interface IItemService
    {
        Task<List<ItemModel>> GetAllItems();
        Task<bool> CreateItem(ItemModel model);
        Task<bool> UpdateItem(ItemModel model);
        bool DeleteItem(int id);
        ItemModel GetItem(int id);
        Task<List<ItemModel>> GetItemByName(ItemModel model);
        Task<ItemModel> GetItemByName(string name);
        Task<List<ItemModel>> GetItemsByType(string type);


    }
}
