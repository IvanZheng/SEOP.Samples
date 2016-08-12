using Sample.Domain.Model;
using SEOP.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Persistence
{
    public class SampleContext : MSDbContext
    {
        public DbSet<Product> Products { get; set; }

        public SampleContext() :
            base("SampleContext")
        {

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
