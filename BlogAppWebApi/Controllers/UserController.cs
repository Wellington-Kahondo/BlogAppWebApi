using BlogAppWebApi.Interfaces;
using BlogAppWebApi.Services;
using BlogAppWebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("create-user")]
        public async Task<ActionResult<UserViewModel>> CreateUser([FromBody] UserViewModel input)
        {
            var user = await _userService.CreateUser(input);
            return Ok(user);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _userService.Login(request.Username, request.Password);
            if (result != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("get-user-by-id/{id}")]
        public async Task<ActionResult<UserViewModel>> GetUserById(Guid id)
        {
            return Ok(await _userService.GetUserById(id));
        }

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var list = await _userService.GetAllUsers();//.ConfigureAwait(false).GetAwaiter().GetResult();
            return Ok(list);
        }

        [HttpPut("update-user")]
        public async Task<ActionResult<UserViewModel>> UpdateUserById([FromBody] UserViewModel input)
        {
            var updatedUser = await _userService.UpdateUser(input);
            return Ok(updatedUser);
        }

        [HttpDelete("delete-user-by-id/{id}")]
        public IActionResult DeleteUserById(Guid id)
        {
            _userService.DeleteUser(id);
            return Ok();
        }
    }
}
