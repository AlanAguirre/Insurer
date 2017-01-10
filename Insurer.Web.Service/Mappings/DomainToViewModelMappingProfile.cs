using AutoMapper;
using Insurer.Model;
using Insurer.Web.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Insurer.Web.Service.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<InsuranceType, InsuranceTypeViewModel>();
            CreateMap<Company, CompanyViewModel>();
        }
    }
}