using MyEvents;
using SEOP.Framework.Domain;
using SEOP.Framework.Event;
using SEOP.Framework.SysExceptions;
using System;

namespace Sample.Domain.Model
{
    public class Product : TimestampedAggregateRoot,
                           IEventSubscriber<ProductReduced> // AggregateRoot
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public DateTime CreateTime { get; set; }
        public Product() {
        }
        public Product(Guid id, string name, int count)
        {
            Id = id;
            Name = name;
            Count = count;
            CreateTime = DateTime.Now;
        }

        public void SetCount(int count)
        {
            Count = count;
        }

        public void ReduceCount(int reduceCount)
        {
            if (Count - reduceCount < 0)
            {
                throw new SysException(Error.CountNotEnougth);
            }
            OnEvent(new ProductReduced(Id, reduceCount, Count - reduceCount));
        }

        void SEOP.Framework.Message.IMessageHandler<ProductReduced>.Handle(ProductReduced @event)
        {
            Count = @event.Count;
        }
    }
}
