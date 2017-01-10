using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurer.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        InsurerEntities dbContext;

        public InsurerEntities Init()
        {
            return dbContext ?? (dbContext = new InsurerEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
