using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using System.Collections.Generic;

namespace eShop.UseCases.AdminPortal.OutStandingOrdersScreen
{
    public class ViewOutstandingOrdersUseCase : IViewOutstandingOrdersUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public ViewOutstandingOrdersUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IEnumerable<Order> Execute()
        {
            return _orderRepository.GetOutstandingOrders();
        }

    }
}
