
using Sample.Domain.Model;
using Sample.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEOP.Framework.UnitOfWork;
using SEOP.EntityFramework.Repositories;
using SEOP.Framework.IoC;
using SEOP.Framework.Event;
using SEOP.Framework.Message;
using MyCommand;

namespace Sample.Application.Services
{
    public class ProductService : ApplicationServiceBase
    {
        protected IEventBus _eventBus;
        protected IMessagePublisher _messagePublisher;
        public ProductService(IUnitOfWork unitOfWork, ISampleRepository repository, 
                              IContainer container, IEventBus eventBus, IMessagePublisher messagePublisher)
            : base(unitOfWork, repository, container)
        {
            _eventBus = eventBus;
            _messagePublisher = messagePublisher;
        }

        public async Task<IEnumerable<Product>> Handle(GetTopProducts getTopProducts)
        {
            var db = _container.Resolve<SampleContext>();
            var products = await _repository.FindAll<Product>()
                                            .OrderByDescending(p => p.CreateTime)
                                            .Take(getTopProducts.Count)
                                            .ToListAsync()
                                            .ConfigureAwait(false);
            
            return products;
        }

        public IEnumerable<Product> Handle(GetProducts command)
        {
            var products = _repository.FindAll<Product>(p => command.ProductIds.Contains(p.Id))
                                            .ToList();
            return products;
        }

        public async Task<int> Handle(ReduceProduct command)
        {
            var product = await _repository.GetByKeyAsync<Product>(command.ProductId);
            product.ReduceCount(command.ReduceCount);
        
            await _unitOfWork.CommitAsync()
                             .ConfigureAwait(false);
            await _messagePublisher.SendAsync(_eventBus.GetEvents().ToArray())
                                   .ConfigureAwait(false);
            return product.Count;
        }

        public async Task<Guid> Handle(CreateProduct command)
        {
            var product = new Product(Guid.NewGuid(), command.Name, command.Count);
            _repository.Add(product);
            await _unitOfWork.CommitAsync()
                             .ConfigureAwait(false);
            return product.Id;
        }
    }
}
