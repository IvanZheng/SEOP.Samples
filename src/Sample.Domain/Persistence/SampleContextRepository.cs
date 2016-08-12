
using SEOP.EntityFramework.Repositories;
using SEOP.Framework.IoC;
using SEOP.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Persistence
{
    public class SampleRepository : DomainRepository, ISampleRepository
    {
        public SampleRepository(SampleContext context, IUnitOfWork unitOfWork, IContainer container)
            : base(context, unitOfWork, container)
        {

        }
    }
}
