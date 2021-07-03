using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;

namespace eShop.UseCases.SearchProductScreen
{
    public class ViewProductUseCase: IViewProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public ViewProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product Execute(int id)
        {
            return _productRepository.GetProduct(id);
        }
    }
}
