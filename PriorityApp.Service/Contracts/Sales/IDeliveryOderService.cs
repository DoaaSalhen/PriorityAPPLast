using PriorityApp.Models.Models;
using PriorityApp.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Contracts.Sales
{
    interface IDeliveryOderService
    {
        public Task<List<OrderModel>> GetAllOrders();
    }
}
