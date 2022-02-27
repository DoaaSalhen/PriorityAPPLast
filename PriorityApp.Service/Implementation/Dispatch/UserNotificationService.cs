using AutoMapper;
using Data.Repository;
using Microsoft.Extensions.Logging;
using PriorityApp.Models.Models.MasterModels;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Models.Dispatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Implementation.Dispatch
{
    public class UserNotificationService : IUserNotificationService
    {
        private readonly IRepository<UserNotification, long> _userNotificationRepository;

        private readonly ILogger<ItemService> _logger;
        private readonly IMapper _mapper;


        public UserNotificationService(IRepository<UserNotification, long> userNotificationRepository,
            ILogger<ItemService> logger, IMapper mapper)
        {
            _userNotificationRepository = userNotificationRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<bool> CreateUserNotification(List<UserNotificationModel> models)
        {
            try
            {
                List<UserNotification> userNotifications = _mapper.Map<List<UserNotification>>(models);
                _userNotificationRepository.AddRange(userNotifications);
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return false;
        }

        public  List<UserNotificationModel> GetUserNotification(string  userId, long submitNotificationId)
        {
            try
            {
               var userNotifications =  _userNotificationRepository.Find(n => n.userId == userId && n.submitNotificationId <= submitNotificationId, false).Where(n=>n.Seen== false).ToList();
                List<UserNotificationModel> userNotificationModels = _mapper.Map<List<UserNotificationModel>>(userNotifications);
                return userNotificationModels;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public List<UserNotificationModel> GetUnseenNotifications(long submitNotificationId, string userId)
        {
            try
            {
                _userNotificationRepository.Find(n=>n.submitNotificationId == submitNotificationId && n.userId == userId, false).Where(n=>n.Seen == false).ToList();
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public async Task<bool> UpdateUserNotification(UserNotificationModel model)
        {
            try
            {
                var userNotification = _mapper.Map<UserNotification>(model);
                var response = _userNotificationRepository.Update(userNotification);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return false;
        }

        public List<UserNotificationModel> GetAllUnseenNotificationsForUser(string userId)
        {
            var userNotifications =_userNotificationRepository.Find(n => n.userId == userId && n.Seen == false,false, u=>u.submitNotification).ToList();
            List<UserNotificationModel> UserNotificationModels = new List<UserNotificationModel>();
            UserNotificationModels = _mapper.Map<List<UserNotificationModel>>(userNotifications);
            return UserNotificationModels;
        }

    }
}
