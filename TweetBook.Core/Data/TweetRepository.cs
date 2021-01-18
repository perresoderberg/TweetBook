using Dapper;
//using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Core.DomainModels;

namespace TweetBook.Core.Data

{
    public class TweetRepository : ITweetRepository
    {
        private readonly IConfiguration _configuration;
        private  IDbConnection _connection;
        private IDbConnection GetOpenConnection() 
        {
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            _connection.Open();

            return _connection;
        }
        public TweetRepository(IConfiguration configuration, IDbConnection connection)
        {
            _configuration = configuration;
            _connection = connection;
        }

        public async Task<int> DeleteByIdAsync(int tweetId)
        {
            var sql = @"DELETE Tweet WHERE TweetId = @TweetId";

            using (var connection = GetOpenConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(sql, new { @TweetId = tweetId });
                return rowsAffected;
            }
        }

        public async Task<IEnumerable<Tweet>> GetAllAsync()
        {
            var sql = "SELECT * from Tweet";

            using (var connection = GetOpenConnection())
            {
                var result = await connection.QueryAsync<Tweet>(sql);
                return result?.ToList();
            }
        }

        public async Task<Tweet> GetByIdAsync(int TweetId)
        {
            var sql = @"SELECT * from Tweet WHERE TweetId = @TweetId";

            using (var connection = GetOpenConnection())
            {
                var result = await connection.QueryAsync<Tweet>(sql, new { @TweetId = TweetId });
                return result.FirstOrDefault();
            }
        }

        public async Task<int> UpdateAsync(Tweet item)
        {
            var sql = "UPDATE Tweet set name=@name where TweetId=@TweetId";

            using (var connection = GetOpenConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var result = await connection.ExecuteAsync(sql, new { @name=item.Name, @TweetId = item.TweetId}, transaction);
                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        async Task<int> IGenericRepository<Tweet>.AddAsync(Tweet item)
        {

            /// => using Contrib
            //using (var connection = GetOpenConnection())
            //{
            //    var ret = connection.Insert(new Tweet() { DateCreated=DateTime.Now, GuidId=Guid.NewGuid(), Name=item.Name });
            //    return (int)ret;
            //}


            var sql = "INSERT INTO Tweet (guidId, Name, dateCreated) " +
                "values (@guidId, @name, GetDate()); SELECT CAST(SCOPE_IDENTITY() as int)";
            using (var connection = GetOpenConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    //var rowsAffected = await connection.ExecuteAsync
                    //    (sql, new { @guidId = Guid.NewGuid(), @name = item.Name }, transaction);

                    var resp = await connection.QueryAsync<int>
                        (sql, new { @guidId = Guid.NewGuid(), @name = item.Name }, transaction);
                    int IdOfInsertedItem = resp.Single();
                    transaction.Commit();
                    
                    return IdOfInsertedItem;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
