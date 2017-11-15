
using WebApplication.DatabaseEntities;

namespace WebApplication.Repositories.Interfaces
{
    public interface IDeliveryMethodRepository
    {
        DeliveryMethod[] GetAll();
    }
}
