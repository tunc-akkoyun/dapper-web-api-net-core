using Rise.Domain.User;
using Rise.Domain.User.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rise.Data.Repository.User
{
    public interface IUserRepository
    {
        //Queries
        Task<GetUser> GetByIdAsync(int id);
        Task<IEnumerable<ListUser>> GetAllAsync(ListUserRequest listUserRequest);
        Task<GetUser> LoginAsync(LoginUserRequest loginUserRequest);
        Task<GetUser> RegisterAsync(CreateUserRequest registerUserRequest);

        // Commands
        Task<int> UpdateAsync(UpdateUserRequest updateUserRequest);
        Task<int> DeleteAsync(int id);
    }
}
