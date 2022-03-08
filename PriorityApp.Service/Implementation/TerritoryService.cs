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
   public class TerritoryService : ITerritoryService
    {
        private readonly IRepository<Territory, int> _repository;
        private readonly ILogger<TerritoryService> _logger;
        private readonly IMapper _mapper;

        public TerritoryService(IRepository<Territory, int> repository,
            ILogger<TerritoryService> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        Task<bool> ITerritoryService.CreateTerritory(TerritoryModel model)
        {
            var territory = _mapper.Map<Territory>(model);
            try
            {
              
                    _repository.Add(territory);
               
                return Task<bool>.FromResult<bool>(true);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return Task<bool>.FromResult<bool>(false);
        }

        bool ITerritoryService.DeleteTerritory(int id)
        {
            try
            {
                var response = _repository.DeleteById(id);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return false;
        }

        async Task<List<TerritoryModel>> ITerritoryService.GetAllTeritories()
        {
            try
            {
                var territories = _repository.Find(t => t.IsDelted == false && t.IsVisible == true).ToList();
                var models = new List<TerritoryModel>();
                models = _mapper.Map<List<TerritoryModel>>(territories);
                return models;
            }

            catch (Exception e)

            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        TerritoryModel ITerritoryService.GetTerritory(int id)
        {
            try
            {
                Territory territory = _repository.Find(i => i.Id == id && i.IsVisible == true, false, i=>i.State).First();
                TerritoryModel model = _mapper.Map<TerritoryModel>(territory);
                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public List<TerritoryModel> GetTerritoryByUserId(string UserId)
        {
            try
            {
                List<Territory> territory = _repository.Find(x => x.userId == UserId, false, x=>x.State).ToList();
                List<TerritoryModel> models = _mapper.Map<List<TerritoryModel>>(territory);
                return models;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public async Task<List<int>> GetTerritoryIdsByStateId(int StateId)
        {
            try
            {
                List<int> territoryIds = _repository.Find(t => t.StateId == StateId && t.IsDelted==false && t.IsVisible==true).Select(t => t.Id).ToList();
                
                return territoryIds;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public  async Task<List<TerritoryModel>> GetAllTerritoriesByStateId(int StateId)
        {
            try
            {
                List<Territory> territories = _repository.Find(t => t.StateId == StateId && t.IsDelted == false && t.IsVisible == true).ToList();
                List<TerritoryModel> territoryModels = new List<TerritoryModel>();
                territoryModels = _mapper.Map<List<TerritoryModel>>(territories);
                return territoryModels;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public Task<bool> UpdateTerritory(TerritoryModel model)
        {
            var territory = _mapper.Map<Territory>(model);
            try
            {

                _repository.Update(territory);

                return Task<bool>.FromResult<bool>(true);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return Task<bool>.FromResult<bool>(false);
        }

        public async Task<List<TerritoryModel>> GetAllTerritoriesByListOfStateIds(List<int> StateIds)
        {
            try
            {
                var territories = _repository.Findlist().Result.Where(t => StateIds.Contains(t.StateId) && t.IsDelted == false && t.IsVisible == true);
                var models = new List<TerritoryModel>();
                models = _mapper.Map<List<TerritoryModel>>(territories);
                return models;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }
    }
}
