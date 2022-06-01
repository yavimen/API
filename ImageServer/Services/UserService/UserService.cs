using ImageServer.Data;
using ImageServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageServer.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext dataContext;
        public UserService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<ServiceResponce<List<User>>> AddUser(User user)
        {
            dataContext.Users.Add(user);
            dataContext.SaveChanges();
            var serviceResponce = new ServiceResponce<List<User>>();
            serviceResponce.Data = await dataContext.Users.ToListAsync();
            return serviceResponce;
        }

        public async Task<ServiceResponce<List<User>>> GetAllUsers()
        {
            var serviceResponce = new ServiceResponce<List<User>>();
            serviceResponce.Data = await dataContext.Users.ToListAsync();
            return serviceResponce;
        }

        public async Task<ServiceResponce<User>> GetUserByLogin(string login)
        {
            var serviceResponce = new ServiceResponce<User>();
            serviceResponce.Data = await dataContext.Users.FirstOrDefaultAsync(item => item.Login == login);
            if (serviceResponce.Data == null) 
            {
                serviceResponce.Message = "User is not found";
            }
            return serviceResponce;
        }
        public async Task<ServiceResponce<List<User>>> DeleteUserByLogin(string login)
        {
            var item = await dataContext.Users.FirstOrDefaultAsync(user => user.Login == login);
            if(item == default(User))
                return  new ServiceResponce<List<User>> { Success = false, Message = "We havn't this user" };
            dataContext.Users.Remove(item);
            await dataContext.SaveChangesAsync();
            var serviceResponce = new ServiceResponce<List<User>>();
            serviceResponce.Data = await dataContext.Users.ToListAsync();
            return serviceResponce;
        }
        public async Task<ServiceResponce<List<User>>> UpdateUser(User user) 
        {
            var updatingItem = await dataContext.Users.FirstOrDefaultAsync(item => item.Login == user.Login);
            if (updatingItem == null)
                return new ServiceResponce<List<User>> { Success = false, Message = "This user not found" };
            updatingItem.Email = user.Email;
            updatingItem.Password = user.Password;
            updatingItem.UserPermission = user.UserPermission;

            return new ServiceResponce<List<User>> { Data = dataContext.Users.ToList() };
        }
    }
}
