using PriorityApp.Service.Models.Dispatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Contracts.Dispatch
{
   public interface ISubmitNotificationService
    {
        SubmitNotificationModel CreateSubmitNotification(SubmitNotificationModel model);

        SubmitNotificationModel GetSubmitNotification(int id);

        Task<bool> UpdateSubmitNotification(SubmitNotificationModel model);


        Task<bool> CreateOrderNotification(OrderNotificationModel model);

        List<SubmitNotificationModel> GetUnseenNotifications();



    }
}
