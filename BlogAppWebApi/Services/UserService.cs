using AutoMapper;
using BlogAppWebApi.Data;
using BlogAppWebApi.Helpers;
using BlogAppWebApi.Interfaces;
using BlogAppWebApi.Models;
//using BlogAppWebApi.Interfaces;
using BlogAppWebApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly BlogDbContext _context;

        public UserService(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<UserViewModel> Login(string username, string password)
        {
 
             var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
             if (user == null)
             {
                 return null;
             }

             if (VerifyPasswordHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
             {
                 return _mapper.Map<UserViewModel>(user);
             }

             return null;
       
        }

        public async Task<UserViewModel> CreateUser(UserViewModel input)
        {

            var entity = _mapper.Map<User>(input);

            byte[] passwordHash, passwordSalt;
            CreatePasswordHashHelper.CreatePasswordHash(input.Password, out passwordHash, out passwordSalt);

            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;

            var user = await _context.Users.AddAsync(entity);
            _context.SaveChanges();

            return _mapper.Map<UserViewModel>(user.Entity);
            
        }

        public async Task<UserViewModel> GetUserById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new Exception("User not found.");

            return _mapper.Map<UserViewModel>(user);

        }

        public async Task<List<UserViewModel>> GetAllUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return _mapper.Map<List<UserViewModel>>(users);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message) ;
            }

            return null;
        }

        public async Task<UserViewModel> UpdateUser(UserViewModel input)
        {
            var user = await _context.Users.FindAsync(input.Id);
            if (user == null)
                throw new Exception("User not found.");

            user = _mapper.Map<User>(input);
            var updatedUser = _context.Users.Update(user);
            _context.SaveChanges();

            return _mapper.Map<UserViewModel>(updatedUser.Entity);
        }

        public void DeleteUser(Guid id)
        {
            var user = _context.Users.Find(id);

            _context.Users.Remove(user);
            _context.SaveChanges();

        }
    }
}
