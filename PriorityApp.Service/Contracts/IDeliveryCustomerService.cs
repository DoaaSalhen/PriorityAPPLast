using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Contracts
{
    public interface IDeliveryCustomerService
    {
        Task<List<CustomerModel>> GetAllDeliveryCustomer();
        Task<bool> CreateDeliveryCustomer(CustomerModel model);

        Task<bool> UpdateDeliveryCustomer(CustomerModel model);
        bool DeleteDeliveryCustomer(long CutomerNumber);
        CustomerModel GetDeliveryCustomer(long CutomerNumber);

        Task<List<CustomerModel>> GetCutomersByZoneId(int id);

        Task<List<CustomerModel>> GetCutomersByListOfZoneIds(List<int> zoneIds);

        List<int> GetZoneIdsByListOfCustomerIds(List<long> customerIds);



    }
}
