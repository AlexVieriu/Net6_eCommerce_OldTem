using System.Collections.Generic;

namespace eShop.UseCases.CustomerPortal.PluginInterfaces.DataStore
{
    public interface IDataAccess
    {
        void ExecuteCommand<T>(string sql, T parameters);
        List<T> Query<T, U>(string sql, U parameters);
        T QueryFirst<T, U>(string sql, U parameters);
        T QuerySingle<T, U>(string sql, U parameters);
    }
}
