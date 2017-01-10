using Insurer.Data.Infrastructure;
using Insurer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurer.Data.Repositories
{
    public class InsuranceTypeRepository : RepositoryBase<InsuranceType>, IInsuranceTypeRepository
    {
        public InsuranceTypeRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface IInsuranceTypeRepository : IRepository<InsuranceType>
    {

    }
}
