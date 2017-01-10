﻿using AutoMapper;
using Insurer.Model;
using Insurer.Service;
using Insurer.Web.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Insurer.Web.Service.Controllers
{
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

        [Route("type")]
        public IEnumerable<InsuranceTypeViewModel> Get()
        {
            var types = insuranceTypeService.GetInsuranceTypes();
            return Mapper.Map<IEnumerable<InsuranceType>, IEnumerable<InsuranceTypeViewModel>>(types);
        }

        [Route("price")]
        public HttpResponseMessage Get(string type, decimal price)
        {
            return PriceRequest(type, price, null, null);
        }

        [Route("price")]
        public HttpResponseMessage Get(string type, decimal price, int? modelYear)
        {
            return PriceRequest(type, price, modelYear, null);
        }

        [Route("price")]       
        public HttpResponseMessage Get(string type, decimal price, float? squareMeters)
        {
            return PriceRequest(type, price, null, squareMeters);
        }
        
        private HttpResponseMessage PriceRequest(string type, decimal price, int? modelYear, float? squareMeters)
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