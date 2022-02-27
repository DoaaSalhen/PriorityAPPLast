using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Contracts
{
    public interface IOrderCategoryService
    {

        Task<List<OrderCategoryModel>> GetAllOrderCategories();
        Task<bool> CreateOrderCategory(OrderCategoryModel model);
        Task<bool> UpdateOrderCategory(OrderCategoryModel model);
        bool DeleteOrderCategory(int id);
        ItemModel GetCategory(int id);
        Task<List<OrderCategoryModel>> GetOrderCategoryByName(OrderCategoryModel model);
    }
}
