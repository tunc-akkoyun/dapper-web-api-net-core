using Rise.Data.Repository.User;
using Rise.Domain.User;
using Rise.Domain.User.Request;
using Rise.Utility.Enums;
using Rise.Utility.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rise.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUser> GetUserById(int id)
        {
            var result = await _userRepository.GetByIdAsync(id);
            return result ?? throw new RiseException("Kayıt bulunamadı", ApiStatusCodes.NotFound);
        }

        public async Task<List<ListUser>> GetAllUsers(ListUserRequest listUserRequest)
        {
            var result = await _userRepository.GetAllAsync(listUserRequest);
            return result?.ToList() ?? throw new RiseException("Kayıt bulunamadı", ApiStatusCodes.NotFound);
        }

        public async Task<GetUser> Login(LoginUserRequest loginUserRequest)
        {
            var result = await _userRepository.LoginAsync(loginUserRequest);
            return result ?? throw new RiseException("Kayıt bulunamadı", ApiStatusCodes.NotFound);
        }

        public async Task<GetUser> Register(CreateUserRequest registerUserRequest)
        {
            var result = await _userRepository.RegisterAsync(registerUserRequest);
            return result ?? throw new RiseException("Kayıt oluşturulamadı", ApiStatusCodes.BadRequest);
        }

        public async Task UpdateUser(UpdateUserRequest updateUserRequest)
        {
            var result = await _userRepository.UpdateAsync(updateUserRequest);
            if (result <= 0)
            {
                throw new RiseException("Kayıt güncellenemedi", ApiStatusCodes.BadRequest);
            }
        }

        public async Task DeleteUser(int id)
        {
            var result = await _userRepository.DeleteAsync(id);
            if (result <= 0)
            {
                throw new RiseException("Kayıt silinemedi", ApiStatusCodes.BadRequest);
            }
        }
    }
}
