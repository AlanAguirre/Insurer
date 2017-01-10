using Insurer.Data.Infrastructure;
using Insurer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurer.Data.Repositories
{
    public class CompanyLogRepository : RepositoryBase<CompanyLog>, ICompanyLogRepository
    {
        public CompanyLogRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public override void Add(CompanyLog entity)
        {
            entity.RequestDate = DateTime.Now;
          
            base.Add(entity);
        }


    }

    public interface ICompanyLogRepository : IRepository<CompanyLog>
    {
        
    }
}
