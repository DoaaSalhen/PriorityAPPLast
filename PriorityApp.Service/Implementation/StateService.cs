using PriorityApp.Service.Contracts;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Repository;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Linq;
using PriorityApp.Models.Models.MasterModels;

namespace PriorityApp.Service.Implementation
{
    public class StateService : IStateService
    {
        private readonly IRepository<State, int> _repository;
        private readonly ILogger<IStateService> _logger;
        private readonly IMapper _mapper;

        public StateService(IRepository<State, int> repository,
            ILogger<IStateService> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

   

        Task<bool> IStateService.CreateState(StateModel model)
        {
            var state = _mapper.Map<State>(model);
            try
            {
                    _repository.Add(state);
             
                return Task<bool>.FromResult<bool>(true);
            }
            catch(Exception e)
            {
                return Task<bool>.FromResult<bool>(false);
            }
            
        }

        bool IStateService.DeleteState(int id)
        {
            try
            {
                var response = _repository.DeleteById(id);
                return response;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return false;
        }

        async Task<List<StateModel>> IStateService.GetAllStates()
        {
            try
            {
                var States=_repository.Find(x => x.IsVisible == true, false, x => x.Subregion).ToList();
                var models = new List<StateModel>();
                 models = _mapper.Map<List<StateModel>>(States);
                return models;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        StateModel IStateService.GetState(int id)
        {
            try
            {
                var state=_repository.Find(x => x.Id == id && x.IsVisible == true, false, x => x.Subregion).First();
                StateModel model = _mapper.Map<StateModel>(state);
                return model;
            }
            catch(Exception e)
            {
              _logger.LogError(e.ToString());

            }
            return null;
        }

        public async Task<List<StateModel>> GetStatesBySubRegionId(int id)
        {
            try
            {
                var States = _repository.Find(x => x.SubregionId == id && x.IsVisible == true, true, x => x.Subregion).ToList();
                var models = new List<StateModel>();
                models = _mapper.Map<List<StateModel>>(States);
                return models;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public Task<bool> UpdateState(StateModel model)
        {
            var state = _mapper.Map<State>(model);
            try
            {
                _repository.Update(state);

                return Task<bool>.FromResult<bool>(true);
            }
            catch (Exception e)
            {
                return Task<bool>.FromResult<bool>(false);
            }
        }
    }
}
