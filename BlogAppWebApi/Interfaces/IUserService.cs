using BlogAppWebApi.Models;
using BlogAppWebApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> Login(string username, string password);
        Task<UserViewModel> CreateUser(UserViewModel input);

        Task<UserViewModel> GetUserById(Guid id);

        Task<List<UserViewModel>> GetAllUsers();

        Task<UserViewModel> UpdateUser(UserViewModel input);

        void DeleteUser(Guid id);
    }
}
