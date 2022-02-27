using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PriorityApp.Models.Auth;
using PriorityApp.Models.Models.MasterModels;
using PriorityApp.Service.Contracts.Auth;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;


namespace PriorityApp.Service.Implementation.Auth
{
   public class UserService : IUserService
    {
        private readonly UserManager<AspNetUser> _userManager;

        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(UserManager<AspNetUser> userManager,
        ILogger<UserService> logger, 
        IMapper mapper)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }
        async Task<List<UserModel>> IUserService.GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            var models = new List<UserModel>();
             models = _mapper.Map<List<UserModel>>(users);
            return models;
        }

     public   async Task<bool> CreateUser(UserModel model)
        {
            var user = new AspNetUser();
            //user=_mapper.Map<AspNetUser>(model);
            user.UserName = model.Email;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
        
            try
            {
                    var result=  await _userManager.CreateAsync(user, model.Password);
                     //IEnumerable<string> RolesName = model.AsignedRolesNames;
                    result = await _userManager.AddToRolesAsync(user, model.AsignedRolesNames);
                
                return(true);
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());

            }
            return (false);

        }

        async Task<bool> IUserService.DeleteUser(string id)
        {
            try
            {
                //var roles = await _userManager.GetRolesAsync();
                var user = await _userManager.FindByIdAsync(id);
                //var roles = await _userManager.GetRolesAsync(user);
               
                var response = _userManager.DeleteAsync(user);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
    
        }

        async Task<UserModel> IUserService.GetUser(string id)
        {
            AspNetUser user = await _userManager.FindByIdAsync(id);
            var model = _mapper.Map<UserModel>(user);
            return model;
        }

        async Task <UserModel> IUserService.GetUserByUserName(string userName)
        {
            try
            {
                AspNetUser user = await _userManager.FindByNameAsync(userName);
                var model = _mapper.Map<UserModel>(user);
                return model;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        async Task<bool> IUserService.UpdateUser(UserModel model)
        {
            try
            {
                AspNetUser user = await _userManager.FindByIdAsync(model.id);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                var response = await _userManager.UpdateAsync(user);
                response = await _userManager.AddToRolesAsync(user, model.AsignedRolesNames);
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return false;
        }

       public   List<string> GetUserRoles(UserModel model)
        {
            AspNetUser user = new AspNetUser();
            user = _mapper.Map<AspNetUser>(model);
            List<string> rolesName =  (List<string>)_userManager.GetRolesAsync(user).Result;
            return rolesName;
        }

        Task<List<UserModel>> GetUsersByRole(RoleModel role)
        {
            try
            {
                
            }
            catch
            {

            }
            return null;
        }

        Task<List<UserModel>> IUserService.GetUsersByRole(RoleModel role)
        {
            throw new NotImplementedException();
        }
    }
}
