using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.UseCases.PluginInterfaces.UI;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.ShoppingCard.LocalStorage
{
    public class ShoppingCart : IShoppingCart
    {
        private const string cstrShoppingCart = "eShop.ShoppingCart";

        private readonly IJSRuntime _jSRuntime;
        private readonly IProductRepository _productRepository;

        public ShoppingCart(IJSRuntime jSRuntime, IProductRepository productRepository)
        {
            _jSRuntime = jSRuntime;
            _productRepository = productRepository;
        }

        public async Task<Order> AddProductAsync(Product product)
        {
            // get the Product from localStorage         
            var order = await GetOrder();

            // add the OrderLineItem with the Product already asign with the GetOrder() method
            order.AddProduct(product.ProductId, 1, product.Price);

            // set the Order to the localStorage
            await SetOrder(order);

            return order;
        }

        public async Task<Order> DeleteProductAsync(int productId)
        {
            var order = await GetOrder();
            order.RemoveProduct(productId);
            await SetOrder(order);

            return order;
        }

        public Task EmptyAsync()
        {            
            return SetOrder(null);
        }

        public async Task<Order> GetOrderAsync()
        {
            return await GetOrder();
        }

        public Task<Order> PlaceOrderAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            await SetOrder(order);
            return order;
        }

        public async Task<Order> UpdateQuantityAsync(int productId, int quantity)
        {
            var order = await GetOrder();
            if (quantity < 0)
                return order;
            else if (quantity == 0)
                return await DeleteProductAsync(productId);

            //pass by value, pass by reference
            var lineItem = order.LineItems.SingleOrDefault(x => x.ProductId == productId);
            if (lineItem != null)
                lineItem.Quantity = quantity;

            await SetOrder(order);

            return order;
        }

        private async Task<Order> GetOrder()
        {
            Order order;

            // InvokeAsync : An instance of TValue obtained by JSON-deserializing the return value.
            var strOrder = await _jSRuntime.InvokeAsync<string>("localStorage.getItem", cstrShoppingCart);
            if (!string.IsNullOrWhiteSpace(strOrder) && strOrder.ToLower() != "null")
                order = JsonConvert.DeserializeObject<Order>(strOrder);
            else
            {
                order = new Order();
                await SetOrder(order);
            }

            // we add the Product to the OrderLineItem
            foreach (var item in order.LineItems)
            {
                item.Product = _productRepository.GetProduct(item.ProductId);
            }

            return order;
        }

        private async Task SetOrder(Order order)
        {
            // InvokeVoidAsync(this IJSRuntime jsRuntime, string identifier, params object[] args);

            await _jSRuntime.InvokeVoidAsync("localStorage.setItem",
                                             cstrShoppingCart,
                                             JsonConvert.SerializeObject(order));
        }
    }
}
