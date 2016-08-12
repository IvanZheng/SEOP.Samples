using SEOP.Framework.Event;
using SEOP.Framework.Infrastructure;
using SEOP.Framework.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvents
{
    //[Topic("MyEvent2")]
    public class EventBase : IDomainEvent
    {
        public int Version { get; set; }
        public object AggregateRootID
        {
            get;
            private set;
        }

        public string AggregateRootName
        {
            get;
            set;
        }

        public string ID
        {
            get;
            set;
        }

        public EventBase()
        {
            ID = ObjectId.GenerateNewId().ToString();
        }

        public EventBase(object aggregateRootID)
            : this()
        {
            AggregateRootID = aggregateRootID;
            Key = aggregateRootID.ToString();
        }

        public virtual string Key
        {
            get; set;
        }
    }
}
