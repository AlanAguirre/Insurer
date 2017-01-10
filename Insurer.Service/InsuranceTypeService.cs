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
    public interface IInsuranceTypeService
    {
        IEnumerable<InsuranceType> GetInsuranceTypes(string name = null);
        InsuranceType GetInsuranceType(int id);       
        void SaveCustomer();
    }

    public class InsuranceTypeService : IInsuranceTypeService
    {
        private readonly IInsuranceTypeRepository insuranceTypesRepository;
        private readonly IUnitOfWork unitOfWork;

        public InsuranceTypeService(IInsuranceTypeRepository insuranceTypesRepository, IUnitOfWork unitOfWork)
        {
            this.insuranceTypesRepository = insuranceTypesRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IInsuranceTypeService Members

        public IEnumerable<InsuranceType> GetInsuranceTypes(string name)
        {
            if (string.IsNullOrEmpty(name))
                return insuranceTypesRepository.GetAll();
            else
            {
                var input = name.First().ToString().ToUpper() + name.Substring(1).ToLower();
                return insuranceTypesRepository.GetAll().Where(c => c.Name == input);
            }
                
        }

        public InsuranceType GetInsuranceType(int id)
        {
            return insuranceTypesRepository.GetById(id);
        }

        public void SaveCustomer()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}
