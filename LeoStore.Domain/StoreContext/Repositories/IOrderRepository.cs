using LeoStore.Domain.StoreContext.Entities;
using LeoStore.Domain.StoreContext.Queries;

namespace LeoStore.Domain.StoreContext.Repositories
{
    public interface IOrderRepository
    {
        GetCustomerQueryResultOrder CustomerExists(string id);
        Product ProductExists(string id);

        void Save(Order order);
    }
}