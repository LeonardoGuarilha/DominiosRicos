using System;
using System.Collections.Generic;
using LeoStore.Domain.StoreContext.Entities;
using LeoStore.Domain.StoreContext.Queries;
using LeoStore.Domain.StoreContext.Repositories;

namespace LeoStore.Tests.Fakes
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public GetCustomerQueryResult CheckDocument(string document)
        {
            throw new System.NotImplementedException();
        }

        public GetCustomerQueryResult CheckEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ListCustomerQueryResult> Get()
        {
            throw new System.NotImplementedException();
        }

        public GetCustomerQueryResult GetById(string id)
        {
            throw new NotImplementedException();
        }

        public CustomerOrderCountResult GetCustomerOrderCount(string document)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ListCustomerOrderQueryResult> GetOrders(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save(Customer customer)
        {

        }
    }
}