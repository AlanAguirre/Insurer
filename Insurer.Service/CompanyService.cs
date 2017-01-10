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
    public interface ICompanyService
    {
        IEnumerable<Company> GetCompanies(string name = null);
        Company GetCompany(int id);
        string RegisterCompany(Company company);
        void UpdateCompany(Company company);
        Company GetCompanyByToken(string token);
        void SaveCompany();
    }

    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository companiesRepository;
        private readonly IUnitOfWork unitOfWork;

        public CompanyService(ICompanyRepository companiesRepository, IUnitOfWork unitOfWork)
        {
            this.companiesRepository = companiesRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Company> GetCompanies(string name = null)
        {
            if (string.IsNullOrEmpty(name))
                return companiesRepository.GetAll();
            else
                return companiesRepository.GetAll().Where(c => c.Name == name);
        }

        public Company GetCompany(int id)
        {
            return companiesRepository.GetById(id);
        }

        public Company GetCompanyByToken(string token)
        {
            return companiesRepository.getCompanyByToken(token);
        }

        public string RegisterCompany(Company company)
        {
            company.Token = Guid.NewGuid().ToString();
            companiesRepository.Add(company);
            SaveCompany();
            return company.Token;
        }

        public void SaveCompany()
        {
            unitOfWork.Commit();
        }

        public void UpdateCompany(Company company)
        {
            companiesRepository.Update(company);
            SaveCompany();
        }
    }
}
