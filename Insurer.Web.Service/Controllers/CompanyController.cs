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
    [RoutePrefix("api/businesspartner")]
    public class CompanyController : ApiController
    {
        private ICompanyService companyService;
        private ICompanyLogService companyLogService;

        public CompanyController(ICompanyService companyService, ICompanyLogService companyLogService)
        {
            this.companyService = companyService;
            this.companyLogService = companyLogService;
        }

        /// <summary>
        /// Gets a list with all companies registered
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("companies")]
        public IEnumerable<CompanyViewModel> Get()
        {
            var companies = companyService.GetCompanies();
            return Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(companies);
        }

        /// <summary>
        /// Gets a list of companies that are consuming the service and the quantity of what type of insurance they want to know
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("companies/log")]
        [HttpGet]
        public IEnumerable<CompanyLogView> GetCompaniesLog()
        {
            
            return companyLogService.GetCompaniesLog();
        }

        /// <summary>
        /// Registers a company to get the service token to use the API
        /// </summary>
        /// <param name="value">Company information</param>
        /// <returns>ServiceToken</returns>
        [AllowAnonymous]
        [Route("register")]
        public HttpResponseMessage Post(CompanyViewModel value)
        {
            if (ModelState.IsValid)
            {
                var token = companyService.RegisterCompany(Mapper.Map<CompanyViewModel, Company>(value));
                if (string.IsNullOrEmpty(token))
                    return this.Request.CreateResponse(
                            HttpStatusCode.BadRequest);
                else
                    return this.Request.CreateResponse(
                            HttpStatusCode.OK,
                            new { ServiceToken = token });
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }                        
        }
        
    }
}
