using eShop.CoreBusiness.Models;
using eShop.UseCases.CustomerPortal.PluginInterfaces.DataStore;
using eShop.UseCases.PluginInterfaces.DataStore;
using System.Collections.Generic;
using System.Linq;

namespace eShop.DataStore.SQL.Dapper
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDataAccess _dataAccess;

        public ProductRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Product GetProduct(int id)
        {
            return _dataAccess.QuerySingle<Product, dynamic>
                ("Select * from Product where ProductId = @ProductId", new { ProductId = id });
        }

        public IEnumerable<Product> GetProducts(string filter)
        {
            List<Product> list;

            if (string.IsNullOrWhiteSpace(filter))
                list = _dataAccess.Query<Product, dynamic>
                    ("Select * from Product", new { });

            else
                list = _dataAccess.Query<Product, dynamic>
                    ("Select * from Product where Name like '%' + @Filter + '%' ",
                     new { Filter = filter });

            return list.AsEnumerable();
        }
    }
}
