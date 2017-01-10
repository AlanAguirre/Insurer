using Insurer.Data.Infrastructure;
using Insurer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurer.Data.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public override void Add(Company entity)
        {
            entity.CreationDate = DateTime.Now;
            entity.LastAccessDate = DateTime.Now;
            base.Add(entity);
        }

        public Company getCompanyByToken(string token)
        {
            var company = DbContext.Companies.Where(c => c.Token == token).FirstOrDefault();

            return company;
        }

        public override void Update(Company entity)
        {
            entity.LastAccessDate = DateTime.Now;
            base.Update(entity);
        }
    }

    public interface ICompanyRepository : IRepository<Company>
    {
        Company getCompanyByToken(string token);
    }
}
