
using WebApplication.DatabaseEntities;

namespace WebApplication.Repositories.Interfaces
{
    public interface IDeliveryMethodRepository
    {
        /// <summary>
        /// Получение методов доставки
        /// </summary>
        DeliveryMethod[] GetAll();
    }
}
