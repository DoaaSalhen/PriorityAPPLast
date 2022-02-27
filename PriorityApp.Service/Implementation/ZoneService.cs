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
    public class ZoneService : IZoneService
    {
        private readonly IRepository<Zone, int> _repository;
        private readonly ILogger<ZoneService> _logger;
        private readonly IMapper _mapper;

        public ZoneService(IRepository<Zone, int> repository,
                            ILogger<ZoneService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        Task<bool> IZoneService.CreateZone(ZoneModel model)
        {
            var zone = _mapper.Map<Zone>(model);
            try
            {
               
                    _repository.Add(zone);
               
                return Task<bool>.FromResult<bool>(true);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return Task<bool>.FromResult<bool>(false);
        }

        bool IZoneService.DeleteZone(int id)
        {
            throw new NotImplementedException();
        }

        async Task<List<ZoneModel>> IZoneService.GetAllZones()
        {
            try
            {
                var Zones = _repository.Find(x => x.IsVisible == true, false, x => x.Territory).ToList();
                var models = new List<ZoneModel>();
                models = _mapper.Map<List<ZoneModel>>(Zones);
                return models;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        ZoneModel IZoneService.GetZone(int id)
        {
            try
            {
                Zone zone = _repository.Find(z => z.Id == id && z.IsVisible == true, false, z=>z.Territory).First();
                ZoneModel zoneModel = _mapper.Map<ZoneModel>(zone);
                return zoneModel;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        List<ZoneModel> IZoneService.GetListOfZonesByTerritoryId(int TerritoryId)
        {
            try
            {
                var zones = _repository.Find(x => x.TerritoryId == TerritoryId && x.IsVisible == true).ToList();
                List<ZoneModel> zoneModels = new List<ZoneModel>();
                zoneModels = _mapper.Map<List<ZoneModel>>(zones);
                return zoneModels;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public async Task<List<ZoneModel>> GetListOfZonesByTerritoryIds(List<int> TerritoryIds)
        {
            try
            {
                var zones = _repository.Findlist().Result.Where(z => TerritoryIds.Contains(z.TerritoryId) && z.IsVisible == true);
                var models = new List<ZoneModel>();
                models = _mapper.Map<List<ZoneModel>>(zones);
                return models;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public Task<bool> UpdateZone(ZoneModel model)
        {
            var zone = _mapper.Map<Zone>(model);
            try
            {

                _repository.Add(zone);

                return Task<bool>.FromResult<bool>(true);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return Task<bool>.FromResult<bool>(false);
        }

        public List<int> GetListOfTerritoryIdsByZoneIds(List<int> zoneIds)
        {
            try
            {
                List<int> territoryIds = _repository.Findlist().Result.Where(z => zoneIds.Contains(z.Id) && z.IsVisible == true).Select(c => c.TerritoryId).ToList();

                return territoryIds;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }
    }
}
