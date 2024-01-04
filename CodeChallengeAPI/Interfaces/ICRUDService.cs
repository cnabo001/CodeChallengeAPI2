using CodeChallengeAPI.Models;

namespace CodeChallengeAPI.Interfaces
{
    public interface ICRUDService
    {
        public Task<List<Customers>> getCustomers();
        public Task<List<Customers>> AddCustomer(Customers customer);
        public Task<List<Customers>> UpdateCustomer(Customers customer);
        public Task<bool> DeleteCustomer(int customerId);
    }
}
