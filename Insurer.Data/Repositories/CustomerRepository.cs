using Insurer.Data.Infrastructure;
using Insurer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurer.Data.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public override void Add(Customer entity)
        {
            var selectedInsuranceTypes = entity.InsuranceTypes.Select(i => i.InsuranceTypeId);
            var insuranceTypes = DbContext.InsuranceTypes.Where(i => selectedInsuranceTypes.Contains(i.InsuranceTypeId)).ToList();

            entity.InsuranceTypes = insuranceTypes;
            entity.CreationDate = DateTime.Now;
            base.Add(entity);
        }
    }

    public interface ICustomerRepository : IRepository<Customer>
    {
        
    }
}
