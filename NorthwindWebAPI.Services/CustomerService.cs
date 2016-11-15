using System.Collections.Generic;
using System.Linq;

using NorthwindWebAPI.Data;
using NorthwindWebAPI.Models;

namespace NorthwindWebAPI.Services
{
    public class CustomerService
    {
        private CustomerRepository _customerRepository;
            
        /// <summary>
        /// customerlist stores in-memory copy of repository (cached).
        /// </summary>
        private static List<Customer> _customerList;

        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
        }

        public IEnumerable<Customer> Get()
        {
            if(_customerList == null)
            {
                _customerList = _customerRepository.Get().ToList();
            }
            return _customerList;
        }

        public IEnumerable<Customer> Get(string customerId)
        {
            if (_customerList != null)
                return _customerList.Where(c => c.CustomerID == customerId);
            else return _customerRepository.Get(customerId);
        }

        public void Add(Customer customer)
        {
            _customerRepository.Add(customer);
            if (_customerList != null)
                _customerList.Add(customer);
        }

        public void Update(Customer customer)
        {
            _customerRepository.Update(customer);
            if (_customerList != null)
            {
                _customerList.Remove(_customerList.FirstOrDefault(c => c.CustomerID == customer.CustomerID));
                _customerList.Add(customer);
            }
        }

        public void Delete(string customerId)
        {
            _customerRepository.Delete(customerId);
            if (_customerList != null)
            {
                _customerList.Remove(_customerList.Where(c => c.CustomerID == customerId).FirstOrDefault());
            }
        }
    }
}
