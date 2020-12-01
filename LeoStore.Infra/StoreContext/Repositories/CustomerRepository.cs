using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using LeoStore.Domain.StoreContext.Entities;
using LeoStore.Domain.StoreContext.Queries;
using LeoStore.Domain.StoreContext.Repositories;
using LeoStore.Infra.StoreContext.DataContext;

namespace LeoStore.Infra.StoreContext.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly LeoDataContext _context;
        public CustomerRepository(LeoDataContext context)
        {
            _context = context;
        }

        public GetCustomerQueryResult CheckDocument(string document)
        {
            return _context
                .Connection
                .Query<GetCustomerQueryResult>(
                    "select id, firstname, document, email from customer where document =@Document",
                    new { Document = document })
            .SingleOrDefault();
        }

        public GetCustomerQueryResult CheckEmail(string email)
        {
            return _context
                .Connection
                .Query<GetCustomerQueryResult>(
                    "select id, firstname, document, email from customer where email =@Email",
                new { Email = email })
            .FirstOrDefault();

        }

        public IEnumerable<ListCustomerQueryResult> Get()
        {
            return _context
            .Connection
            .Query<ListCustomerQueryResult>(
                "select id, firstname, document, email from customer",
            new { });

        }

        public GetCustomerQueryResult GetById(string id)
        {
            return _context
                .Connection
                .Query<GetCustomerQueryResult>(
                    "select id, firstname, document, email from customer where id =@Id ",
                    new { Id = id })
            .FirstOrDefault();
        }

        public CustomerOrderCountResult GetCustomerOrderCount(string document)
        {
            return _context
                .Connection
                .Query<CustomerOrderCountResult>(
                    @"select c.id, c.firstname, c.document from customer c
                    inner join orders o on o.customerid = c.id ",
            new { Documet = document })

            .FirstOrDefault();
        }

        public IEnumerable<ListCustomerOrderQueryResult> GetOrders(Guid id)
        {
            return _context
                .Connection
                .Query<ListCustomerOrderQueryResult>(
                "Fazer a query",
                new { Id = id });

        }

        public void Save(Customer customer)
        {
            _context
                .Connection
                .Execute(@"INSERT INTO customer(id, firstname, lastname, document, email, phone) values (@id, @firstname, @lastname, @document, @email, @phone)",
            new
            {
                id = customer.Id,
                firstname = customer.Name.FirstName,
                lastname = customer.Name.LastName,
                document = customer.Document.Number,
                email = customer.Email.Address,
                phone = customer.Phone
            });


            foreach (var address in customer.Addresses)
            {
                _context
                    .Connection
                    .Execute(@"INSERT INTO address(id, customerid, number, complement, district, city, state, country, zipcode, type) values (@id, @customerid, @number, @complement, @district, @city, @state, @country, @zipcode, @type)",
                new
                {
                    id = address.Id,
                    customerid = customer.Id,
                    number = address.Number,
                    complemet = address.Complement,
                    district = address.District,
                    city = address.City,
                    country = address.Country,
                    zipcode = address.ZipCode,
                    type = address.Type
                });
            }
        }
    }
}