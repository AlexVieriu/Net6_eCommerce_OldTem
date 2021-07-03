using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;

namespace eShop.UseCases.ViewProductScreen
{
    public class AddProductToCartUseCase : IAddProductToCartUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCart _shoppingCart;
        private readonly IShoppingCartStateStore _shoppingCartStateStore;

        public AddProductToCartUseCase(IProductRepository productRepository,
                                       IShoppingCart shoppingCard,
                                       IShoppingCartStateStore shoppingCardStateStore)
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCard;
            _shoppingCartStateStore = shoppingCardStateStore;
        }

        public async void Execute(int productId)
        {
            var product = _productRepository.GetProduct(productId);
            await _shoppingCart.AddProductAsync(product);

            _shoppingCartStateStore.UpdateLineItemsCount();
        }
    }
}
