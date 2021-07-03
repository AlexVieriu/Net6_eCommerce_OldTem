using eShop.CoreBusiness.Models;

namespace eShop.UseCases.AdminPortal.OrderDetailScreen.Interfaces
{
    public interface IViewOrderDetailUseCase
    {
        Order Execute(int orderId);
    }
}