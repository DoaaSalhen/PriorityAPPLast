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
    public class PriorityService : IPriorityService
    {
        private readonly IRepository<Priority, int> _repository;
        private readonly ILogger<PriorityService> _logger;
        private readonly IMapper _mapper;

        public PriorityService(IRepository<Priority, int> repository,
            ILogger<PriorityService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public Task<bool> CreatePriority(PriorityModel model)
        {
            throw new NotImplementedException();
        }

        public bool DeletePriority(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PriorityModel>> GetAllPriorities()
        {
            try
            {
                var priorities = _repository.Find(i => i.IsVisible == true).ToList();
                var models = new List<PriorityModel>();
                models = _mapper.Map<List<PriorityModel>>(priorities);
                return models;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public ItemModel GetPriority(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PriorityModel>> GetPriorityByName(PriorityModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePriority(PriorityModel model)
        {
            throw new NotImplementedException();
        }
    }
}
