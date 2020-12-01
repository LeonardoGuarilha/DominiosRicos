using System;
using System.Collections.Generic;
using LeoStore.Domain.StoreContext.Entities;
using LeoStore.Domain.StoreContext.Queries;

namespace LeoStore.Domain.StoreContext.Repositories
{
    public interface ICustomerRepository
    {
        GetCustomerQueryResult CheckDocument(string document);
        GetCustomerQueryResult CheckEmail(string email);
        void Save(Customer customer);
        CustomerOrderCountResult GetCustomerOrderCount(string document);
        IEnumerable<ListCustomerQueryResult> Get();
        GetCustomerQueryResult GetById(string id);
        IEnumerable<ListCustomerOrderQueryResult> GetOrders(Guid id);
    }
}