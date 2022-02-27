using PriorityApp.Models.Models.MasterModels;
using AutoMapper;
using Data.Repository;
using Microsoft.Extensions.Logging;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace PriorityApp.Service.Implementation
{
    public class RegionService:IRegionService
    {
        private readonly IRepository<MainRegion,int> _MainRegionrepository;
        private readonly IRepository<SubRegion, int> _SubRegionrepository;
        private readonly ILogger<RegionService> _logger;
        private readonly IMapper _mapper;
        public RegionService(IRepository<MainRegion, int> MainRegionrepository, 
            ILogger<RegionService> logger,
            IMapper mapper, IRepository<SubRegion, int> SubRegionrepository)
        {
            _MainRegionrepository = MainRegionrepository;
            _logger = logger;
            _mapper = mapper;
            _SubRegionrepository = SubRegionrepository;
        }

        Task<bool> IRegionService.CreateMainRegion(MainRegionModel model)
        {
            var MainRegion = _mapper.Map<MainRegion>(model);
            try
            {
              
                    _MainRegionrepository.Add(MainRegion);
               
                return Task<bool>.FromResult<bool>(true);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return Task<bool>.FromResult<bool>(false);
        }

        Task<bool> IRegionService.CreateSubRegion(SubRegionModel model)
        {
            var SubRegion = _mapper.Map<SubRegion>(model);
            try
            {
                    _SubRegionrepository.Add(SubRegion);
             
                return Task<bool>.FromResult<bool>(true);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return Task<bool>.FromResult<bool>(false);
        }

        bool IRegionService.DeleteMainRegion(int id)
        {
            try
            {
                var response=_MainRegionrepository.DeleteById(id);
                return response;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return false;
        }

        bool IRegionService.DeleteSubRegion(int id)
        {
            try
            {
                var response = _SubRegionrepository.DeleteById(id);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return false;
        }

        async Task<List<MainRegionModel>> IRegionService.GetAllIMainRegions()
        {
            try
            {
                var MainRegions = _MainRegionrepository.Find(i => i.IsVisible == true).ToList();
                var models = new List<MainRegionModel>();
                models = _mapper.Map<List<MainRegionModel>>(MainRegions);
                return models;
            }

            catch (Exception e)

            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        async Task<List<SubRegionModel>> IRegionService.GetAllISubRegions()
        
        {
            try
            {
                var SubRegions = _SubRegionrepository.Find(x=>x.IsVisible==true,true,x=>x.MainRegion).ToList();
                var models = new List<SubRegionModel>();
                models = _mapper.Map<List<SubRegionModel>>(SubRegions);
                return models;
            }

            catch (Exception e)

            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        MainRegionModel IRegionService.GetMainRegion(int id)
        {
            try
            {
                var MainRegion = _MainRegionrepository.Find(i => i.Id == id && i.IsVisible == true);
                MainRegionModel model = _mapper.Map<MainRegionModel>(MainRegion);
                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        SubRegionModel IRegionService.GetSubRegion(int id)
        {
            try
            {
                var SubRegion = _SubRegionrepository.Find(x => x.Id == id && x.IsVisible == true, false, x => x.MainRegion).First();
                SubRegionModel model = _mapper.Map<SubRegionModel>(SubRegion);
                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public async Task<List<SubRegionModel>> GetSubRegionByMainRegionId(int id)
        {
            try
            {
                var SubRegions = _SubRegionrepository.Find(x => x.MainRegionId == id && x.IsVisible == true, true, x => x.MainRegion).ToList();
                var models = new List<SubRegionModel>();
                models = _mapper.Map<List<SubRegionModel>>(SubRegions);
                return models;
            }

            catch (Exception e)

            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public Task<bool> UpdateMainRegion(MainRegionModel model)
        {
            var MainRegion = _mapper.Map<MainRegion>(model);
            try
            {

                _MainRegionrepository.Update(MainRegion);

                return Task<bool>.FromResult<bool>(true);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return Task<bool>.FromResult<bool>(false);
        }

        public Task<bool> UpdateSubRegion(SubRegionModel model)
        {
            var SubRegion = _mapper.Map<SubRegion>(model);
            try
            {
                _SubRegionrepository.Update(SubRegion);

                return Task<bool>.FromResult<bool>(true);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return Task<bool>.FromResult<bool>(false);
        }
    }
}
