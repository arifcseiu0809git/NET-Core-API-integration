using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Common.DBContext
{
    public class ESSDBContext : DbContext//IdentityDbContext<ApplicationUser>
    {
        public ESSDBContext()
            : base("POSConnectionString")//, throwIfV1Schema: false)
        {
        }

        public static ESSDBContext Create()
        {
            return new ESSDBContext();
        }
    }
}
