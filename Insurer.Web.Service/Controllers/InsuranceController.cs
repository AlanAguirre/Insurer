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
    [AllowAnonymous]
    [RoutePrefix("api/insurance")]
    public class InsuranceController : ApiController
    {
        private IInsuranceTypeService insuranceTypeService;
        private IInsuranceService insuranceService;
        private ICompanyService companyService;
        private ICompanyLogService companyLogService;

        public InsuranceController(IInsuranceTypeService insuranceTypeService, 
            IInsuranceService insuranceService, ICompanyService companyService,
            ICompanyLogService companyLogService)
        {
            this.insuranceTypeService = insuranceTypeService;
            this.insuranceService = insuranceService;
            this.companyService = companyService;
            this.companyLogService = companyLogService;
        }

        /// <summary>
        /// Gets a list with all insurance types 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("type")]
        public IEnumerable<InsuranceTypeViewModel> Get()
        {
            var types = insuranceTypeService.GetInsuranceTypes();
            return Mapper.Map<IEnumerable<InsuranceType>, IEnumerable<InsuranceTypeViewModel>>(types);
        }

        /// <summary>
        /// Gets a value based on the insurance type and information provided
        /// </summary>        /// 
        /// <param name="type"></param>
        /// <param name="price"></param>
        /// <param name="modelYear"></param>
        /// <param name="squareMeters"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("price")]
        public HttpResponseMessage Get(string type, decimal price, int? modelYear = null, float? squareMeters = null)
        {
            Decimal insuranceValue = 0;
            //Validate token
            var re = Request;
            var headers = re.Headers;
            Company company;
            if (headers.Contains("ServiceToken"))
            {
                string token = headers.GetValues("ServiceToken").First();

                company = companyService.GetCompanyByToken(token);

                if (company == null)
                {
                    return this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { Message = "Invalid ServiceToken." });
                }
            }
            else
            {
                return this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { Message = "Missing ServiceToken. Please register your company to use our web services." });
            }

            //Validate insurance type            
            var insuranceType = insuranceTypeService.GetInsuranceTypes(type).FirstOrDefault();
            if (insuranceType == null)
            {
                return this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { Message = "Invalid insurance type." });
            }


            switch (insuranceType.Name)
            {
                case "Car":
                    if (!modelYear.HasValue)
                    {
                        return this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { Message = "Missing model year." });
                    }

                    insuranceValue = insuranceService.CalcInsuranceCar(price, modelYear.Value);

                    break;
                case "Motorcycle":
                    if (!modelYear.HasValue)
                    {
                        return this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { Message = "Missing model year." });
                    }

                    insuranceValue = insuranceService.CalcInsuranceMotorcycle(price, modelYear.Value);
                    break;
                case "House":

                    insuranceValue = insuranceService.CalcInsuranceHouse(price);
                    break;
                case "Farm":
                    if (!squareMeters.HasValue)
                    {
                        return this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { Message = "Missing square meters." });
                    }

                    insuranceValue = insuranceService.CalcInsuranceFarm(price, squareMeters.Value);
                    break;
                default:

                    break;
            }

            companyLogService.CreateCompanyLog(new CompanyLog()
            {
                Company = company.Name,
                InsureType = insuranceType.Name
            });

            return this.Request.CreateResponse(
                        HttpStatusCode.OK,
                        new
                        {
                            Company = company.Name,
                            Type = insuranceType.Name,
                            Value = insuranceValue
                        });

        }
    }
}
