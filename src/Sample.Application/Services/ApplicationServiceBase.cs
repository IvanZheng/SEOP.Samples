
using Sample.Domain.Persistence;
using SEOP.Framework.IoC;
using SEOP.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Application.Services
{
    public class ApplicationServiceBase
    {
        protected IUnitOfWork _unitOfWork;
        protected ISampleRepository _repository;
        protected IContainer _container;
        public ApplicationServiceBase(IUnitOfWork unitOfWork, ISampleRepository repository, IContainer container)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _container = container;
        }
    }
}
