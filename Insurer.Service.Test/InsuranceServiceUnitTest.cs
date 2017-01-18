using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Insurer.Service.Test
{
    [TestClass]
    public class InsuranceServiceUnitTest
    {
        [TestMethod]
        public void TestCalcInsuranceCar()
        {
            var insuranceService = new InsuranceService();

            insuranceService.setCurrentYear(2000);

            var result = insuranceService.CalcInsuranceCar(100, 1998);

            Assert.AreEqual((decimal)6.6, result);
        }

        [TestMethod]
        public void TestCalcInsuranceCarOlderThanTwentyYear()
        {
            var insuranceService = new InsuranceService();

            insuranceService.setCurrentYear(2000);

            var result = insuranceService.CalcInsuranceCar(100, 1975);

            Assert.AreEqual((decimal)15, result);
        }

        [TestMethod]
        public void TestCalcInsuranceFarm()
        {
            var insuranceService = new InsuranceService();
            

            var result = insuranceService.CalcInsuranceFarm(100, 100);

            Assert.AreEqual((decimal)0.20, result);
        }

        [TestMethod]
        public void TestCalcInsuranceHouse()
        {
            var insuranceService = new InsuranceService();
            var result = insuranceService.CalcInsuranceHouse(100);

            Assert.AreEqual((decimal)2, result);
        }

        [TestMethod]
        public void TestCalcInsuranceMotorcycle()
        {
            var insuranceService = new InsuranceService();

            insuranceService.setCurrentYear(2000);

            var result = insuranceService.CalcInsuranceMotorcycle(100, 1998);

            Assert.AreEqual((decimal)6.6, result);
        }

        [TestMethod]
        public void TestCalcInsuranceMotorcycleOlderThanTwentyYear()
        {
            var insuranceService = new InsuranceService();

            insuranceService.setCurrentYear(2000);

            var result = insuranceService.CalcInsuranceMotorcycle(100, 1975);

            Assert.AreEqual((decimal)25, result);
        }
    }
}
