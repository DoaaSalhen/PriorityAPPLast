using PriorityApp.Models.Auth;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Contracts.Auth
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUsers();
        Task<bool> CreateUser(UserModel model);
        Task<bool> UpdateUser(UserModel model);
        Task<bool> DeleteUser(string id);
        Task<UserModel> GetUser(string id);
        Task<UserModel> GetUserByUserName(string userName);
        public List<string> GetUserRoles(UserModel model);
        Task<List<UserModel>> GetUsersByRole(RoleModel role);




    }
}
