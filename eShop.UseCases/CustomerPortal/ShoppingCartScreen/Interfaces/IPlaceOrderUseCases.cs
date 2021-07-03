using eShop.CoreBusiness.Models;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCartScreen.Interfaces
{
    public interface IPlaceOrderUseCases
    {
        Task<string> Execute(Order order);
    }
}