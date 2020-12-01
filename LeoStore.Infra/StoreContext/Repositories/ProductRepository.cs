using System.Linq;
using Dapper;
using LeoStore.Domain.StoreContext.Entities;
using LeoStore.Domain.StoreContext.Queries;
using LeoStore.Domain.StoreContext.Repositories;
using LeoStore.Infra.StoreContext.DataContext;

namespace LeoStore.Infra.StoreContext.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly LeoDataContext _context;
        public ProductRepository(LeoDataContext context)
        {
            _context = context;
        }
        public GetProductQueryResult CheckProduct(string title)
        {
            return _context
                .Connection
                .Query<GetProductQueryResult>(
                    "select id, title, description, image, price, quantityonhand from product where title =@Title",
                    new { Title = title })
            .SingleOrDefault();
        }

        public void Save(Product product)
        {
            _context
                .Connection
                .Execute(@"INSERT INTO product(id, title, description, image, price, quantityonhand) values (@id, @title, @description, @image, @price, @quantityonhand)",
            new
            {
                id = product.Id,
                title = product.Title,
                description = product.Description,
                image = product.Image,
                price = product.Price,
                quantityonhand = product.QuantityOnHand
            });
        }
    }
}