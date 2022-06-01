using ImageServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageServer.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponce<List<User>>> GetAllUsers();
        Task<ServiceResponce<User>> GetUserByLogin(string login);
        Task<ServiceResponce<List<User>>> DeleteUserByLogin(string login);
        Task<ServiceResponce<List<User>>> AddUser(User user);
        Task<ServiceResponce<List<User>>> UpdateUser(User user);
    }
}
