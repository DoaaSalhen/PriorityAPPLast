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
    public class WarehouseService : IWarehouseService
    {
        private readonly IRepository<Warehouse, long> _repository;
        private readonly ILogger<WarehouseService> _logger;
        private readonly IMapper _mapper;

        public WarehouseService(
            IRepository<Warehouse, long> repository,
           ILogger<WarehouseService> logger,
           IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> CreateWarehouse(WarehouseModel model)
        {
            try
            {
                Warehouse warehouse =  _mapper.Map<Warehouse>(model);
                _repository.Add(warehouse);
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return false;
        }

        public bool DeleteWarehouse(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<WarehouseModel>> GetAllWarehouse()
        {
            try
            {
                List<Warehouse> warehouses = _repository.Find(w=>w.IsVisible==true,false,w=>w.State).ToList();
                var models = new List<WarehouseModel>();
                models = _mapper.Map<List<WarehouseModel>>(warehouses);
                return models;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public WarehouseModel GetWarehouse(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<WarehouseModel>> GetWarehouseByName(WarehouseModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateWarehouse(WarehouseModel model)
        {
            throw new NotImplementedException();
        }
    }
}
