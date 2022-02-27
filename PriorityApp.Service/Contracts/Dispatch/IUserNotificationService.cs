using PriorityApp.Service.Models.Dispatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Contracts
{
    public interface IUserNotificationService
    {
        Task<bool> CreateUserNotification(List<UserNotificationModel> models);

        List<UserNotificationModel> GetUserNotification(string userId, long submitNotificationId);

        List<UserNotificationModel> GetUnseenNotifications(long submitNotificationId, string userId);
        Task<bool> UpdateUserNotification(UserNotificationModel model);
        List<UserNotificationModel> GetAllUnseenNotificationsForUser(string userId);

    }
}
