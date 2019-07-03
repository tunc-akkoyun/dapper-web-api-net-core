using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Rise.Data.Repository
{
    /// <summary>
    /// Base abstract class for repository operations
    /// </summary>
    public abstract class RepositoryBase
    {
        /// <summary>
        /// Base connection for injection
        /// </summary>
        public readonly IDbConnection _dbConnection;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dbConnection"></param>
        public RepositoryBase(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// Select top 1 *
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="commandType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync<T>(string query, CommandType commandType, int id)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<T>(query, param: new { Id = id }, commandType: commandType);
        }

        /// <summary>
        /// Select *
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync<T>(string query, CommandType commandType, object parameters)
        {
            return await _dbConnection.QueryAsync<T>(query, param: parameters, commandType: commandType);
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="query"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual async Task<int> AddAsync(string query, CommandType commandType, object parameters)
        {
            return await _dbConnection.ExecuteAsync(query, param: parameters, commandType: commandType);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="query"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(string query, CommandType commandType, object parameters)
        {
            return await _dbConnection.ExecuteAsync(query, param: parameters, commandType: commandType);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="query"></param>
        /// <param name="commandType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(string query, CommandType commandType, int id)
        {
            return await _dbConnection.ExecuteAsync(query, param: new { Id = id }, commandType: commandType);
        }
    }
}
