using PriorityApp.Models.Models.MasterModels;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Data.Repository;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PriorityApp.Service.Implementation
{
    public class ItemService : IItemService
    {
        private readonly IRepository<Item, long> _ItemRepository;
        private readonly ILogger<ItemService> _logger;
        private readonly IMapper _mapper;


        public ItemService(IRepository<Item, long> ItemRepository,
            ILogger<ItemService> logger,IMapper mapper)
        {
            _ItemRepository = ItemRepository;
            _logger = logger;
            _mapper = mapper;
        }
        Task<bool> IItemService.CreateItem(ItemModel model)
        {
            var Item = _mapper.Map<Item>(model);

            try
            {
                
                    _ItemRepository.Add(Item);
                                
                return Task<bool>.FromResult<bool>(true);   
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return Task<bool>.FromResult<bool>(false);


        }

        bool IItemService.DeleteItem(int id)
        {
           var response= _ItemRepository.DeleteById(id);
            return response;
        }

        ItemModel IItemService.GetItem(int id)
        {
            try
            {
                var item = _ItemRepository.Find(i => i.Id == id && i.IsVisible == true);
                ItemModel model = _mapper.Map<ItemModel>(item);
                return model;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        async Task<List<ItemModel>> IItemService.GetAllItems()
        {
            try
            {
                var items = _ItemRepository.Find(i => i.IsVisible == true).ToList();
                var models = new List<ItemModel>();
                models = _mapper.Map<List<ItemModel>>(items);
                return models;
            }
            catch(Exception e)

            {
                _logger.LogError(e.ToString());
            }
            return null;

        }

        async Task<List<ItemModel>> IItemService.GetItemsByType(string type)
        {
            try
            {
                var items = _ItemRepository.Find(i => i.type == type && i.IsVisible == true).ToList();
            var models = new List<ItemModel>();
            models = _mapper.Map<List<ItemModel>>(items);
                return models;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        async Task<ItemModel> IItemService.GetItemByName(string name)
        {
            try
            {
                 Item item =_ItemRepository.Find(i => EF.Functions.Like(i.Name, "%" + name) && i.IsVisible == true).First();
                ItemModel itemModel = _mapper.Map<ItemModel>(item);
                return itemModel;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }




        async Task<List<ItemModel>> IItemService.GetItemByName(ItemModel model)
        {
            var models = new List<ItemModel>();

            if (model.SearchName != null && model.SearchCreatedDate.Date.ToString() != "1/1/0001 12:00:00 AM" && model.SearchUpdatedDate.Date.ToString() != "1/1/0001 12:00:00 AM")
            {
               var items = _ItemRepository.Find(x => EF.Functions.Like(x.Name, "%" + model.SearchName), true)
                 .Where(x => x.HMP == model.SearchHPM)
                 .Where(x => x.CreatedDate == model.SearchCreatedDate)
                 .Where(x => x.UpdatedDate == model.SearchCreatedDate);
                 models = _mapper.Map<List<ItemModel>>(items);

            }
            else if (model.SearchName != null && model.SearchCreatedDate.Date.ToString() != "1/1/0001 12:00:00 AM")
            {
                var items = _ItemRepository.Find(x => EF.Functions.Like(x.Name, "%" + model.SearchName), true)
                .Where(x => x.HMP == model.SearchHPM)
                .Where(x => x.CreatedDate.Date == model.SearchCreatedDate.Date);
                models = _mapper.Map<List<ItemModel>>(items);

            }
            else if(model.SearchName != null && model.SearchUpdatedDate.Date.ToString() != "1/1/0001 12:00:00 AM")
            {
                 var items = _ItemRepository.Find(x => EF.Functions.Like(x.Name, "%" + model.SearchName), true)
                .Where(x => x.HMP == model.SearchHPM)
                .Where(x => x.UpdatedDate.Date == model.SearchUpdatedDate.Date);
                    models = _mapper.Map<List<ItemModel>>(items);
            }
            else if(model.SearchCreatedDate.Date.ToString() != "1/1/0001 12:00:00 AM" && model.SearchUpdatedDate.Date.ToString() != "1/1/0001 12:00:00 AM")
            {
                var items = _ItemRepository.Find(x =>x.CreatedDate==model.SearchCreatedDate, true)
                 .Where(x => x.HMP == model.SearchHPM)
                 .Where(x => x.UpdatedDate.Date == model.SearchUpdatedDate.Date);
                models = _mapper.Map<List<ItemModel>>(items);
            }
            else if (model.SearchName != null)
            {
                var items = _ItemRepository.Find(x => EF.Functions.Like(x.Name, "%" + model.SearchName), true);
                models = _mapper.Map<List<ItemModel>>(items);
            }
            else if (model.SearchCreatedDate.Date.ToString() != "1/1/0001 12:00:00 AM")
            {
                var items = _ItemRepository.Find(x=>x.CreatedDate.Date==model.SearchCreatedDate.Date, true);
                models = _mapper.Map<List<ItemModel>>(items);
            }
            else if (model.SearchUpdatedDate.Date.ToString() != "1/1/0001 12:00:00 AM")
            {
                var items = _ItemRepository.Find(x => x.UpdatedDate.Date == model.SearchCreatedDate.Date, true);
                models = _mapper.Map<List<ItemModel>>(items);
            }

            return models;
        }

        public Task<bool> UpdateItem(ItemModel model)
        {
            var Item = _mapper.Map<Item>(model);

            try
            {
                _ItemRepository.Update(Item);

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
