using Insurer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Insurer.Data
{
    public class InsurerSeedData : DropCreateDatabaseIfModelChanges<InsurerEntities>
    {
        protected override void Seed(InsurerEntities context)
        {
            AddUserAndRole(context);
            GetInsuranceTypes().ForEach(i => context.InsuranceTypes.Add(i));            

            context.Commit();
        }

        bool AddUserAndRole(InsurerEntities context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("Administrator"));
            ir = rm.Create(new IdentityRole("Contributor"));

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var adminUser = new ApplicationUser()
            {
                UserName = "admin@ensurance.com",
                Email = "admin@ensurance.com",
            };

            IdentityResult user = manager.Create(adminUser, "P_assw0rd1");
            
            if (user.Succeeded == false)
                return user.Succeeded;
            user = manager.AddToRole(adminUser.Id, "Administrator");
            
            var contributorUser1 = new ApplicationUser()
            {
                UserName = "contributo1@ensurance.com",
                Email = "contributo1@ensurance.com",
            };

            user = manager.Create(contributorUser1, "contributo1");
            if (user.Succeeded == false)
                return user.Succeeded;
            user = manager.AddToRole(contributorUser1.Id, "Contributor");
            

            var contributorUser2 = new ApplicationUser()
            {
                UserName = "contributo2@ensurance.com",
                Email = "contributo2@ensurance.com",
            };

            user = manager.Create(contributorUser2, "contributo2");
            if (user.Succeeded == false)
                return user.Succeeded;
            user = manager.AddToRole(contributorUser2.Id, "Contributor");
            
            return user.Succeeded;
        }

        private static List<InsuranceType> GetInsuranceTypes()
        {
            return new List<InsuranceType>
            {
                new InsuranceType {
                    Name = "Car"
                },
                new InsuranceType {
                    Name = "Motorcycle"
                },
                new InsuranceType {
                    Name = "House"
                },
                new InsuranceType {
                    Name = "Farm"
                }
            };
        }        
    }
}
