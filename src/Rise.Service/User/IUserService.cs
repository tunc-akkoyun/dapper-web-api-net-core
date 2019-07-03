using Rise.Domain.User;
using Rise.Domain.User.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rise.Service.User
{
    public interface IUserService
    {
        Task<GetUser> GetUserById(int Id);
        Task<List<ListUser>> GetAllUsers(ListUserRequest listUserRequest);
        Task<GetUser> Login(LoginUserRequest loginUserRequest);
        Task<GetUser> Register(CreateUserRequest createUserRequest);

        Task UpdateUser(UpdateUserRequest updateUserRequest);
        Task DeleteUser(int id);
    }
}
