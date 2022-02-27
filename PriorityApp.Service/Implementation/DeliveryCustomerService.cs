using AutoMapper;
using Data.Repository;
using Microsoft.Extensions.Logging;
using PriorityApp.Models.Models.MasterModels;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Implementation
{

    public class DeliveryCustomerService : IDeliveryCustomerService
    {
        private readonly IRepository<Customer, long> _repository;
        private readonly ILogger<DeliveryCustomerService> _logger;
        private readonly IMapper _mapper;

        public DeliveryCustomerService(IRepository<Customer, long> repository,
            ILogger<DeliveryCustomerService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        Task<bool> IDeliveryCustomerService.CreateDeliveryCustomer(CustomerModel model)
        {
            try
            {
                var customer = _mapper.Map<Customer>(model);
                var response = _repository.Add(customer);
                return Task<bool>.FromResult<bool>(true);
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
                
            }
            return Task<bool>.FromResult<bool>(false);
        }

        bool IDeliveryCustomerService.DeleteDeliveryCustomer(long CutomerNumber)
        {
            throw new NotImplementedException();
        }

        async Task<List<CustomerModel>> IDeliveryCustomerService.GetAllDeliveryCustomer()
        {
            try
            {
                var DeliveryCustomers = _repository.Find(i => i.IsVisible == true,false,i=>i.zone.Territory.State).ToList();
                List<CustomerModel> models = new List<CustomerModel>();
                models = _mapper.Map<List<CustomerModel>>(DeliveryCustomers);
                return models;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        CustomerModel IDeliveryCustomerService.GetDeliveryCustomer(long CutomerNumber)
        {
            try
            {
                Customer customer = _repository.Find(c => c.Id == CutomerNumber && c.IsVisible == true,false, c=>c.zone.Territory).First();
                CustomerModel model = _mapper.Map<CustomerModel>(customer);
                return model;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        Task<bool> IDeliveryCustomerService.UpdateDeliveryCustomer(CustomerModel model)
        {
            try
            {
                Customer customer = _mapper.Map<Customer>(model);
                var response = _repository.Update(customer);
                return Task<bool>.FromResult<bool>(response);

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return Task<bool>.FromResult<bool>(false);

        }
        async Task<List<CustomerModel>> IDeliveryCustomerService.GetCutomersByZoneId(int zoneId)
        {
            try
            {
                var customers = _repository.Find(c => c.ZoneId == zoneId && c.IsVisible == true).ToList();
                var models = new List<CustomerModel>();
                models = _mapper.Map<List<CustomerModel>>(customers);
                return models;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public async Task<List<CustomerModel>> GetCutomersByListOfZoneIds(List<int> zoneIds)
        {
            try
            {
                var customers = _repository.Findlist().Result.Where(c => zoneIds.Contains(c.ZoneId) && c.IsVisible == true);
                var models = new List<CustomerModel>();
                models = _mapper.Map<List<CustomerModel>>(customers);
                return models;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public List<int> GetZoneIdsByListOfCustomerIds(List<long> customerIds)
        {
            try
            {
                List<int> zoneIds = _repository.Findlist().Result.Where(c => customerIds.Contains(c.Id) && c.IsVisible == true).Select(c=>c.ZoneId).ToList();
               
                return zoneIds;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }
    }
}
