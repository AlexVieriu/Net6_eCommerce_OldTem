using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;
using eShop.UseCases.ShoppingCartScreen.Interfaces;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCartScreen
{
    public class UpdateQuantityUseCase : IUpdateQuantityUseCase
    {
        private readonly IShoppingCart _shoppingCart;
        private readonly IShoppingCartStateStore _shoppingCartStateStore;

        public UpdateQuantityUseCase(IShoppingCart shoppingCart,
                                     IShoppingCartStateStore shoppingCartStateStore)
        {
            _shoppingCart = shoppingCart;
            _shoppingCartStateStore = shoppingCartStateStore;
        }
        public async Task<Order> Execute(int productId, int quantity)
        {
            var order = await _shoppingCart.UpdateQuantityAsync(productId, quantity);
            _shoppingCartStateStore.UpdateProductQuantity();        // manage the global state

            return order;
        }
    }
}
