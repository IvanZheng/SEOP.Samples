using SEOP.Framework.Command;
using SEOP.Framework.Infrastructure;
using SEOP.Framework.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCommand
{
    [Topic("Classic.MyCommand")]
    public class CommandBase : ICommand
    {
        public string ID
        {
            get;set;
        }

        public string Key
        {
            get;set;
        }

        public bool NeedRetry
        {
            get;set;
        }

        public CommandBase()
        {
            NeedRetry = false;
            ID = ObjectId.GenerateNewId().ToString();
        }
    }

    public class ConcurrentCommand : LinearCommandBase
    {
        public ConcurrentCommand():
            base()
        {
            NeedRetry = true;
        }
    }

    public abstract class LinearCommandBase : CommandBase, ILinearCommand
    {
        public LinearCommandBase() : base() {
        }
    }
}
