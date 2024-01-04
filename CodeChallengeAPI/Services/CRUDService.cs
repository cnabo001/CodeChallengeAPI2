using CodeChallengeAPI.Interfaces;
using CodeChallengeAPI.Models;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeChallengeAPI.Services
{
    public class CRUDService : ICRUDService
    {

        public CRUDService() { }

        private const string jsonData = "data/Customers.json";

        public async Task<List<Customers>> getCustomers()
        {
            var customers = await ReadData();
            return customers;
        }

        public async Task<List<Customers>> AddCustomer(Customers customer)
        {
            customer.Id = CreateId();
            customer.CreatedOn = DateTime.Now;
            customer.UpdatedOn = DateTime.Now;

            List<Customers> customers = await ReadData();
            customers.Add(customer);

            await WriteData(customers);

            return customers;

            
        }

        public async Task<List<Customers>> UpdateCustomer(Customers customer)
        {
            var customers = await ReadData();
            foreach(var c in customers)
            {
                if(c.Id == customer.Id)
                {
                    c.FirstName = customer.FirstName;
                    c.LastName = customer.LastName;
                    c.Email = customer.Email;
                    c.UpdatedOn = DateTime.Now;
                    await WriteData(customers);
                }
            }

            return customers;
        }

        public async  Task<bool> DeleteCustomer(int customerId)
        {
                var customers = await ReadData();

                foreach(var customer in customers)
                {
                    if(customer.Id == customerId)
                    {
                        customers.Remove(customer);
                        await WriteData(customers);
                        return true;
                    } else
                    {
                        return false;
                    }
                }
            return false;
        }


        private async Task  WriteData(List<Customers> customers)
        {
            using (StreamReader reader = File.OpenText(jsonData)) {
                var serialize = new JsonSerializerOptions {
                    WriteIndented = true
                };
                await JsonSerializer.SerializeAsync(reader.BaseStream, customers, serialize);
            }
        }

        private async Task<List<Customers>> ReadData()
        {
            using(StreamReader reader = File.OpenText(jsonData))
            {

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var customers = await JsonSerializer.DeserializeAsync<List<Customers>>(reader.BaseStream, options);
                return customers ?? new List<Customers>();
            }
        }

        private int CreateId()
        {
            return (int)DateTime.Now.Ticks;
        }
    }
}
