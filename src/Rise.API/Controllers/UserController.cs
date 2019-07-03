using Microsoft.AspNetCore.Mvc;
using Rise.Domain.User;
using Rise.Domain.User.Request;
using Rise.Service.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rise.API.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("login")]
        public async Task<GetUser> Login(LoginUserRequest loginUserRequest)
        {
            return await _userService.Login(loginUserRequest);
        }

        [HttpPost]
        [Route("register")]
        public async Task<GetUser> Register(CreateUserRequest createUserRequest)
        {
            return await _userService.Register(createUserRequest);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<GetUser> GetUserById(int id)
        {
            return await _userService.GetUserById(id);
        }

        [HttpGet]
        [Route("{isActive}/{skip}/{take}")]
        public async Task<List<ListUser>> GetAllUsers(ListUserRequest listUserRequest)
        {
            return await _userService.GetAllUsers(listUserRequest);
        }

        [HttpPut]
        public async Task UpdateUser(UpdateUserRequest updateUserRequest)
        {
            await _userService.UpdateUser(updateUserRequest);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
        }
    }
}