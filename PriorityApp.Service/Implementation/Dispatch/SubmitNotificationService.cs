using AutoMapper;
using Data.Repository;
using Microsoft.Extensions.Logging;
using PriorityApp.Models.Models.Dispatch;
using PriorityApp.Models.Models.MasterModels.Dispatch;
using PriorityApp.Service.Contracts.Dispatch;
using PriorityApp.Service.Models.Dispatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Implementation.Dispatch
{
   public class SubmitNotificationService : ISubmitNotificationService
    {
        private readonly IRepository<SubmitNotification, long> _submitNotificationRepository;
        private readonly IRepository<OrderNotification, long> _orderNotificationRepository;

        private readonly ILogger<ItemService> _logger;
        private readonly IMapper _mapper;


        public SubmitNotificationService(IRepository<SubmitNotification, long> submitNotificationRepository,
            IRepository<OrderNotification, long> orderNotificationRepository,
            ILogger<ItemService> logger, IMapper mapper)
        {
            _submitNotificationRepository = submitNotificationRepository;
            _orderNotificationRepository = orderNotificationRepository;
            _logger = logger;
            _mapper = mapper;
        }


        public SubmitNotificationModel CreateSubmitNotification(SubmitNotificationModel model)
        {
            try
            {
                SubmitNotification submitNotification = _mapper.Map<SubmitNotification>(model);
                submitNotification = _submitNotificationRepository.Add(submitNotification);
                SubmitNotificationModel Newmodel = _mapper.Map<SubmitNotificationModel>(submitNotification);
                return Newmodel;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;

        }

        public Task<bool> CreateOrderNotification(OrderNotificationModel model)
        {
            try
            {
                OrderNotification orderNotification = _mapper.Map<OrderNotification>(model);
                _orderNotificationRepository.Add(orderNotification);
                return Task<bool>.FromResult<bool>(true);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return Task<bool>.FromResult<bool>(false);
        }


        public List<SubmitNotificationModel> GetUnseenNotifications()
        {
            try
            {
                //var submitNotification = _submitNotificationRepository.Find(n => n.Seen == false, false);
                //List<SubmitNotificationModel> submitNotificationModels = _mapper.Map<List<SubmitNotificationModel>>(submitNotification);
                //return submitNotificationModels;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public SubmitNotificationModel GetSubmitNotification(int id)
        {
            try
            {
                SubmitNotification submitNotification = _submitNotificationRepository.Find(s => s.Id == id).First();
                SubmitNotificationModel submitNotificationModel = _mapper.Map<SubmitNotificationModel>(submitNotification);
                return submitNotificationModel;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public async Task<bool> UpdateSubmitNotification(SubmitNotificationModel model)
        {
            try
            {
                SubmitNotification submitNotification = _mapper.Map<SubmitNotification>(model);
                 _submitNotificationRepository.Update(submitNotification);
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return false;
        }
    }
}
