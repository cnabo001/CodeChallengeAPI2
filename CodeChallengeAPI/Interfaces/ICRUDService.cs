using CodeChallengeAPI.Models;

namespace CodeChallengeAPI.Interfaces
{
    public interface ICRUDService
    {
        public Task<List<Customers>> getCustomers();
        public Task<List<Customers>> AddCustomer(Customers customer);
    }
}
