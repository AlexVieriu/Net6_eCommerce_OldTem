using eShop.CoreBusiness.Models;
using eShop.UseCases.CustomerPortal.PluginInterfaces.DataStore;
using eShop.UseCases.PluginInterfaces.DataStore;
using System.Collections.Generic;
using System.Linq;

namespace eShop.DataStore.SQL.Dapper
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDataAccess _dataAccess;

        public OrderRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public int CreateOrder(Order order)
            // Create Order
        {
            var sql =
                    @"INSERT INTO [dbo].[Order]
                       ([DatePlaced]
                       ,[DateProcessing]
                       ,[DateProcessed]
                       ,[CustomerName]
                       ,[CustomerAddress]
                       ,[CustomerCity]
                       ,[CustomerStateProvince]
                       ,[CustomerCountry]
                       ,[AdminUser]
                       ,[UniqueId])
                 OUTPUT INSERTED.OrderId
                 VALUES
                       (@DatePlaced
                        ,@DateProcessing
                        ,@DateProcessed
                        ,@CustomerName
                        ,@CustomerAddress
                        ,@CustomerCity
                        ,@CustomerStateProvince
                        ,@CustomerCountry
                        ,@AdminUser
                        ,@UniqueId)";

            var orderId = _dataAccess.QuerySingle<int, Order>(sql, order);

            sql = @"INSERT INTO [dbo].[OrderLineItem]
                           ([ProductId]
                           ,[OrderId]
                           ,[Quantity]
                           ,[Price])
                     VALUES
                           (@ProductId
                           ,@OrderId
                           ,@Quantity
                           ,@Price)";


            order.LineItems.ForEach(x => x.OrderId = orderId);
            _dataAccess.ExecuteCommand(sql, order.LineItems);

            return orderId;
        }

        public IEnumerable<OrderLineItem> GetLineItemsByOrderId(int orderId)
        {
            var sql = "select * from OrderLineItem where OrderId = @OrderId";
            var lineItems = _dataAccess.Query<OrderLineItem, dynamic>(sql, new { OrderId = orderId });

            sql = "Select * from Product where ProductId = @ProductId";
            lineItems.ForEach(x => x.Product = _dataAccess.QuerySingle<Product, dynamic>
                            (sql, new { ProductId = x.ProductId }));

            return lineItems;
        }

        public Order GetOrder(int id)
        {
            var sql = "SELECT * FROM [ORDER] WHERE OrderId = @OrderId";
            var order = _dataAccess.QuerySingle<Order, dynamic>(sql, new { OrderId = id });
            order.LineItems = GetLineItemsByOrderId(order.OrderId.Value).ToList();

            return order;
        }

        public Order GetOrderByUniqueId(string uniqueId)
        {
            var sql = "Select * from [Order] where UniqueId = @UniqueId";
            var order = _dataAccess.QuerySingle<Order, dynamic>(sql, new { UniqueId = uniqueId });
            order.LineItems = GetLineItemsByOrderId(order.OrderId.Value).ToList();

            return order;
        }
     
        public IEnumerable<Order> GetOrders()
        {
            return _dataAccess.Query<Order, dynamic>("select * from [order]", new { });
        }

        public IEnumerable<Order> GetOutstandingOrders()
        {
            var sql = "select * from [order] where DateProcessed is null";
            return _dataAccess.Query<Order, dynamic>(sql, new { });
        }

        public IEnumerable<Order> GetProcessedOrders()
        {
            var sql = "select * from [order] where DateProcessed is null";
            return _dataAccess.Query<Order, dynamic>(sql, new { });
        }

        public void UpdateOrder(Order order)
        {
            // update order
            var sql = @"UPDATE [Order]
                          SET [DatePlaced] = @DatePlaced
                          ,[DateProcessing] = @DateProcessing
                          ,[DateProcessed] = @DateProcessed
                          ,[CustomerName] = @CustomerName
                          ,[CustomerAddress] = @CustomerAddress
                          ,[CustomerCity] = @CustomerCity
                          ,[CustomerStateProvince] = @CustomerStateProvince
                          ,[CustomerCountry] = @CustomerCountry
                          ,[AdminUser] = @AdminUser
                          ,[UniqueId] = @UniqueId
                      WHERE OrderId = @OrderId";

            _dataAccess.ExecuteCommand<Order>(sql, order);

            // update line items
            sql = @"UPDATE [OrderLineItem]
                       SET [ProductId] = @ProductId
                          ,[OrderId] = @OrderId
                          ,[Quantity] = @Quantity
                          ,[Price] = @Price
                     WHERE LineItemId = @LineItemId";

            _dataAccess.ExecuteCommand<List<OrderLineItem>>(sql, order.LineItems);
        }
    }
}
