using Dapper;
using eShop.UseCases.CustomerPortal.PluginInterfaces.DataStore;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace eShop.DataStore.SQL.Dapper.Helpers
{
    public class DataAccess : IDataAccess
    {
        private readonly string _connectionString;

        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T, U>(string sql, U parameters)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<T>(sql, parameters).ToList();
            }
        }

        public T QuerySingle<T, U>(string sql, U parameters)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                return con.QuerySingle<T>(sql, parameters);
            }
        }

        public T QueryFirst<T, U>(string sql, U parameters)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                return con.QueryFirst<T>(sql, parameters);
            }
        }

        public void ExecuteCommand<T>(string sql, T parameters)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(sql, parameters);
            }
        }
    }
}
