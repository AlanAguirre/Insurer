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
    public interface ICompanyLogService
    {
        IEnumerable<CompanyLogView> GetCompaniesLog(string company = null);
        void CreateCompanyLog(CompanyLog companyLog);        
        void SaveCompany();
    }
    public class CompanyLogService : ICompanyLogService
    {
        private readonly ICompanyLogRepository companiesLogsRepository;
        private readonly IUnitOfWork unitOfWork;

        public CompanyLogService(ICompanyLogRepository companiesLogsRepository, IUnitOfWork unitOfWork)
        {
            this.companiesLogsRepository = companiesLogsRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreateCompanyLog(CompanyLog companyLog)
        {
            companiesLogsRepository.Add(companyLog);
            SaveCompany();
        }

        public IEnumerable<CompanyLogView> GetCompaniesLog(string company = null)
        {
            IEnumerable<CompanyLog> companiesLogs;
            if (string.IsNullOrEmpty(company))
                companiesLogs = companiesLogsRepository.GetAll();
            else
                companiesLogs = companiesLogsRepository.GetAll().Where(c => c.Company == company);

            var companiesLogsGroup = companiesLogs.GroupBy(c => new { c.Company, c.InsureType })
                                    .Select(group => new
                                    {
                                        Company = group.Key.Company,
                                        InsureType = group.Key.InsureType,
                                        Count = group.Count()
                                    });

            var companiesLogsResult = companiesLogsGroup.GroupBy(c => new { c.Company })
                .Select(c => new CompanyLogView
                {
                    Company = c.Key.Company,
                    Car = c.Where(i => i.InsureType == "Car").Select(x => x.Count).FirstOrDefault(),
                    Motorcycle = c.Where(i => i.InsureType == "Motorcycle").Select(x => x.Count).FirstOrDefault(),
                    House = c.Where(i => i.InsureType == "House").Select(x => x.Count).FirstOrDefault(),
                    Farm = c.Where(i => i.InsureType == "Farm").Select(x => x.Count).FirstOrDefault()
                });

            

            return companiesLogsResult;
        }

        public void SaveCompany()
        {
            unitOfWork.Commit();
        }
    }

    public class CompanyLogView
    {
        public string Company { get; set; }
        public int Car { get; set; }
        public int Motorcycle { get; set; }
        public int House { get; set; }
        public int Farm { get; set; }
    }
}
