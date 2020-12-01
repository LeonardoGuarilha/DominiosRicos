using LeoStore.Domain.StoreContext.Entities;
using LeoStore.Domain.StoreContext.Queries;

namespace LeoStore.Domain.StoreContext.Repositories
{
    public interface IProductRepository
    {
        GetProductQueryResult CheckProduct(string title);
        void Save(Product product);
    }
}