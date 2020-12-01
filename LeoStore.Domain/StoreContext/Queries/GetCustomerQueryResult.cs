using System;

namespace LeoStore.Domain.StoreContext.Queries
{
    public class GetCustomerQueryResult
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
    }
}