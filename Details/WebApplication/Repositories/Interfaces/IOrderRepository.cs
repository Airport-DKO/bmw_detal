using WebApplication.Controllers.ControllersEntities;

namespace WebApplication.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Order Create(Order order);
        OrderState? CheckState(int orderId);
    }
}