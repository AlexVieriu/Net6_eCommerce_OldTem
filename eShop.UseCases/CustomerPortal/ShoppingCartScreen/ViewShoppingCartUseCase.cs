using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.UI;
using eShop.UseCases.ShoppingCartScreen.Interfaces;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCartScreen
{
    public class ViewShoppingCartUseCase : IViewShoppingCartUseCase
    {
        private readonly IShoppingCart _shoppingCart;

        public ViewShoppingCartUseCase(IShoppingCart shoppingCard)
        {
            _shoppingCart = shoppingCard;
        }
        public async Task<Order> Execute()
        {
            return await _shoppingCart.GetOrderAsync();
        }
    }
}
