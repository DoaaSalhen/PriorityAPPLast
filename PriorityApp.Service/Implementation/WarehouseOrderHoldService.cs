using AutoMapper;
using Data.Repository;
using Microsoft.Extensions.Logging;
using PriorityApp.Models.Models;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Implementation
{
    public class WarehouseOrderHoldService : IWarehouseOrderHoldService
    {
        private readonly IRepository<WarehouseOrderHold, long> _repository;
        private readonly ILogger<WarehouseOrderHoldService> _logger;
        private readonly IMapper _mapper;


        public WarehouseOrderHoldService(IRepository<WarehouseOrderHold, long> repository,
            ILogger<WarehouseOrderHoldService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public Task<bool> CreateWarehouseOrderHold(WarehouseOrderHoldModel model)
        {
            var Item = _mapper.Map<WarehouseOrderHold>(model);

            try
            {
                _repository.Add(Item);

                return Task<bool>.FromResult<bool>(true);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return Task<bool>.FromResult<bool>(false);
        }

        public WarehouseOrderHoldModel GetWarehouseOrderHold(long id)
        {
            try
            {
                var item = _repository.Find(WH => WH.OrderId == id );
                WarehouseOrderHoldModel model = _mapper.Map<WarehouseOrderHoldModel>(item);
                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public List<WarehouseOrderHoldModel> GetWarehouseOrderHold(string userId, DateTime PriorityDate)
        {
            try
            {
                List<WarehouseOrderHold> warehouseOrderHolds = _repository.Find(WH => WH.HolduserId == userId && WH.HoldPriorityDate == PriorityDate, false).ToList();
                List<WarehouseOrderHoldModel> models = _mapper.Map<List<WarehouseOrderHoldModel>>(warehouseOrderHolds);
                return models;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }
    }
}
