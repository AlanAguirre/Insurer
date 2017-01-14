using AutoMapper;
using Insurer.Model;
using Insurer.Service;
using Insurer.Web.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Insurer.Web.Service.Controllers
{
    [EnableCors(origins: "https://localhost:3000", headers: "*", methods: "*")]
    [Authorize]
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        private ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        // GET: api/customer
        public IEnumerable<CustomerViewModel> Get()
        {
            var customers = customerService.GetCustomers();
            return Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(customers);
        }

        // GET: api/Customer/5
        public CustomerViewModel Get(int id)
        {
            var customer = customerService.GetCustomer(id);
            return Mapper.Map<Customer,CustomerViewModel>(customer);
        }

        // POST: api/Customer
        [Authorize(Roles = "Administrator")]
        public void Post(CustomerViewModel value)
        {
            customerService.CreateCustomer(Mapper.Map<CustomerViewModel, Customer>(value));                        
        }

        // PUT: api/Customer/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: api/Customer/5
        //public void Delete(int id)
        //{
        //}
    }
}
