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
    public class OrderCategoryService : IOrderCategoryService
    {
        private readonly IRepository<OrderCategory, int> _repository;
        private readonly ILogger<OrderCategoryService> _logger;
        private readonly IMapper _mapper;

        public OrderCategoryService(IRepository<OrderCategory, int> repository,
            ILogger<OrderCategoryService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        public Task<bool> CreateOrderCategory(OrderCategoryModel model)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOrderCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderCategoryModel>> GetAllOrderCategories()
        {
            try
            {
                var orderCategories = _repository.GetAll().ToList();
                var models = new List<OrderCategoryModel>();
                models = _mapper.Map<List<OrderCategoryModel>>(orderCategories);
                return models;
            }
            catch (Exception e)

            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public ItemModel GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderCategoryModel>> GetOrderCategoryByName(OrderCategoryModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOrderCategory(OrderCategoryModel model)
        {
            throw new NotImplementedException();
        }
    }
}
