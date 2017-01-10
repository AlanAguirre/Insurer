using Insurer.Data.Infrastructure;
using Insurer.Data.Repositories;
using Insurer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurer.Service
{
    // operations you want to expose
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomers(string name = null);
        Customer GetCustomer(int id);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void SaveCustomer();
    }

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customersRepository;
        private readonly IUnitOfWork unitOfWork;

        public CustomerService(ICustomerRepository customersRepository, IUnitOfWork unitOfWork)
        {
            this.customersRepository = customersRepository;
            this.unitOfWork = unitOfWork;
        }

        #region ICustomerService Members

        public IEnumerable<Customer> GetCustomers(string name)
        {
            if (string.IsNullOrEmpty(name))
                return customersRepository.GetAll();
            else
                return customersRepository.GetAll().Where(c => c.Name == name);
        }

        public Customer GetCustomer(int id)
        {
            return customersRepository.GetById(id);
        }

        public void CreateCustomer(Customer customer)
        {
            customersRepository.Add(customer);
            SaveCustomer();
        }

        public void UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
                
        public void SaveCustomer()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}
