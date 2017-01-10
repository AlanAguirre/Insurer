using AutoMapper;
using Insurer.Model;
using Insurer.Web.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Insurer.Web.Service.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            CreateMap<InsuranceTypeViewModel, InsuranceType>();
            CreateMap<CustomerViewModel, Customer>();
            CreateMap<CompanyViewModel, Company>();
        }
    }
}