using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;
using eShop.UseCases.ShoppingCartScreen.Interfaces;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCartScreen
{
    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IShoppingCart _shoppingCart;
        private readonly IShoppingCartStateStore _shoppingCartStateStore;

        public DeleteProductUseCase(IShoppingCart shoppingCart,
                                    IShoppingCartStateStore shoppingCartStateStore)
        {
            _shoppingCart = shoppingCart;
            _shoppingCartStateStore = shoppingCartStateStore;
        }

        public async Task<Order> Execute(int productId)
        {
            var order = await _shoppingCart.DeleteProductAsync(productId);
            _shoppingCartStateStore.UpdateLineItemsCount();

            return order;
        }
    }
}
