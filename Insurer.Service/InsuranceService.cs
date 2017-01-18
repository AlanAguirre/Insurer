using Insurer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurer.Service
{
    // operations you want to expose
    public interface IInsuranceService
    {
        decimal CalcInsuranceCar(decimal price, int modelYear);
        decimal CalcInsuranceMotorcycle(decimal price, int modelYear);
        decimal CalcInsuranceHouse(decimal price);
        decimal CalcInsuranceFarm(decimal price, float squareMeters);
    }

    public class InsuranceService : IInsuranceService
    {
        private int currentYear;

        public InsuranceService()
        {
            this.currentYear = DateTime.Now.Year;
        }

        public void setCurrentYear(int year)
        {
            this.currentYear = year;
        }

        public decimal CalcInsuranceCar(decimal price, int modelYear)
        {            
            var baseValue = (price * 6) / 100;            
            decimal yearOfUsageValue;
            var yearOfUsage = this.currentYear - modelYear;
            if(yearOfUsage > 20)
            {
                baseValue = 0;
                yearOfUsageValue = (price * 15) / 100;
            }else
            {
                yearOfUsageValue = (baseValue * (yearOfUsage * 5)) / 100;
            }

            return Math.Round(baseValue + yearOfUsageValue, 2);
        }

        public decimal CalcInsuranceFarm(decimal price, float squareMeters)
        {
            var baseValue = (price * (decimal)0.2) / 100;
            var sqmValue = (squareMeters * (float)0.001) / 100;


            return Math.Round(baseValue + (decimal)sqmValue, 2);
        }

        public decimal CalcInsuranceHouse(decimal price)
        {
            return Math.Round((price * 2) / 100, 2);
        }

        public decimal CalcInsuranceMotorcycle(decimal price, int modelYear)
        {
            var baseValue = (price * 6) / 100;
            decimal yearOfUsageValue;
            var yearOfUsage = this.currentYear - modelYear;
            if (yearOfUsage > 20)
            {
                baseValue = 0;
                yearOfUsageValue = (price * 25) / 100;
            }
            else
            {
                yearOfUsageValue = (baseValue * (yearOfUsage * 5)) / 100;
            }

            return Math.Round(baseValue + yearOfUsageValue, 2);
        }
    }
}
