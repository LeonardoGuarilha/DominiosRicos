using System;
using System.Linq;
using Dapper;
using LeoStore.Domain.StoreContext.Entities;
using LeoStore.Domain.StoreContext.Enums;
using LeoStore.Domain.StoreContext.Queries;
using LeoStore.Domain.StoreContext.Repositories;
using LeoStore.Infra.StoreContext.DataContext;

namespace LeoStore.Infra.StoreContext.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly LeoDataContext _context;
        public OrderRepository(LeoDataContext context)
        {
            _context = context;
        }
        public GetCustomerQueryResultOrder CustomerExists(string id)
        {
            return _context
                .Connection
                .Query<GetCustomerQueryResultOrder>(
                    "select id, firstname, lastname, email, document, phone from customer where id =@Id",
                    new { Id = id })
            .SingleOrDefault();
        }

        public Product ProductExists(string id)
        {
            return _context
            .Connection
            .Query<Product>(
                "select id, title, description, image, price, quantityonhand from product where id =@Id",
                new { Id = id })
            .SingleOrDefault();
        }

        public void Save(Order order)
        {
            _context
                .Connection
                .Execute(@"INSERT INTO orders(id, customerid, createdate, status) values (@id, @customerid, @createdate, @status)",
            new
            {
                id = order.Id,
                customerid = order.Customer,
                createdate = DateTime.Now,
                status = EOrderStatus.Created
            });

            foreach (var item in order.Items)
            {
                _context
                    .Connection
                    .Execute(@"INSERT INTO orderitem(id, orderid, productid, quantity, price) values (@id, @orderid, @productid, @quantity, @price)",
                     new
                     {
                         id = item.Id,
                         orderid = order.Id,
                         productid = item.Product,
                         quantity = item.Quantity,
                         price = item.Price
                     }
                    );
            }
        }
    }
}