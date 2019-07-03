using Dapper;
using Rise.Domain.User;
using Rise.Domain.User.Request;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Rise.Data.Repository.User
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private readonly static string GetUserByIdSP = "U_P_GetUserById";
        private readonly static string GetAllUsersSP = "U_P_GetAllUsers";
        private readonly static string InsertUserSP = "U_P_InsertUser";
        private readonly static string UpdateUserSP = "U_P_UpdateUser";
        private readonly static string DeleteUserSP = "U_P_DeleteUser";
        private readonly static string LoginQuery = "SELECT * FROM Y_User with(nolock) where Email = @Email and Password = @Password";

        public UserRepository(IDbConnection dbConnection) : base(dbConnection) { }

        // Base repo operations

        public async Task<GetUser> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync<GetUser>(GetUserByIdSP, CommandType.StoredProcedure, id);
        }

        public async Task<IEnumerable<ListUser>> GetAllAsync(ListUserRequest listUserRequest)
        {
            return await base.GetAllAsync<ListUser>(GetAllUsersSP, CommandType.StoredProcedure, new { listUserRequest.IsActive, listUserRequest.Skip, listUserRequest.Take });
        }

        public async Task<int> UpdateAsync(UpdateUserRequest updateUserRequest)
        {
            using (var transaction = _dbConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                var resultUpdate = await base.UpdateAsync(UpdateUserSP, CommandType.StoredProcedure, updateUserRequest);
                if (resultUpdate > 0)
                {
                    // other transactions can be done
                    var resultOther = true;

                    if (resultOther)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                return 1;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await base.DeleteAsync(DeleteUserSP, CommandType.StoredProcedure, id);
        }


        // Custom repo operations

        public async Task<GetUser> LoginAsync(LoginUserRequest loginUserRequest)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<GetUser>(LoginQuery, param: new { loginUserRequest.Email, loginUserRequest.Password });
        }

        public async Task<GetUser> RegisterAsync(CreateUserRequest createUserRequest)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<GetUser>(InsertUserSP, param: createUserRequest, commandType: CommandType.StoredProcedure);
        }
    }
}
