using PriorityApp.Service.Models;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Contracts
{
    public interface IPickUpCustomerService
    {

        public DataTable PreprocessInsertedPickOrders(DataTable dt, int itemStartIndex);
    }
}
