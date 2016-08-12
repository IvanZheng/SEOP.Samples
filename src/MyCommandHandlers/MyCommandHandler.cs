using MyCommand;
using MyEvents;
using Sample.Domain.Model;
using Sample.Domain.Persistence;
using SEOP.Framework.Command;
using SEOP.Framework.Event;
using SEOP.Framework.Infrastructure;
using SEOP.Framework.Message;
using SEOP.Framework.Repositories;
using SEOP.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCommandHandlers
{
    public class MyCommandHandler : ICommandHandler<SubmitMessage>,
        ICommandAsyncHandler<ReduceProduct>
    {
        public static int HandledMessageCount;
        protected ISampleRepository _repository;
        protected IEventBus _eventBus;
        protected IUnitOfWork _unitOfWork;
        protected IMessageContext _commandContext;
        public MyCommandHandler(IEventBus eventBus, ISampleRepository repositroy, IUnitOfWork uow, IMessageContext commandContext)
        {
            _commandContext = commandContext;
            _repository = repositroy;
            _eventBus = eventBus;
            _unitOfWork = uow;
        }

        public async Task Handle(ReduceProduct command)
        {
            var product = await _repository.GetByKeyAsync<Product>(command.ProductId);
            product.ReduceCount(command.ReduceCount);
            await _unitOfWork.CommitAsync()
                             .ConfigureAwait(false);
            _commandContext.Reply = product.Count;
        }

        public void Handle(SubmitMessage command)
        {
            Console.WriteLine("handle command {0}", command.ToJson());
            _eventBus.Publish(new MessageSubmitted(command.MessageBody));
            Interlocked.Add(ref HandledMessageCount, 1);
        }
    }
}
