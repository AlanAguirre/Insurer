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

namespace Insurer.Web.Service.Controllers
{
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

        // GET: api/businesspartner/companies
        [Authorize]
        [Route("companies")]
        public IEnumerable<CompanyViewModel> Get()
        {
            var companies = companyService.GetCompanies();
            return Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(companies);
        }

        // GET: api/businesspartner/companies
        [Authorize]
        [Route("companies/log")]
        [HttpGet]
        public IEnumerable<CompanyLogView> GetCompaniesLog()
        {
            
            return companyLogService.GetCompaniesLog();
        }

        // POST: api/businesspartner/register
        [Route("register")]
        public HttpResponseMessage Post(CompanyViewModel value)
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
        
    }
}
