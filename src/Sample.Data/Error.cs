
using SEOP.Framework.SysExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data
{
    public enum Error
    {
        NoError,
        CountNotEnougth,
        CommandInvalid = 0x7ffffffe,
        UnknownError = 0x7fffffff
    }


    public class ErrorCodeDictionaryInitializer
    {
        public static void Init()
        {
            ErrorCodeDictionary.InitErrorCodeDictionary(
                new Dictionary<object, string>()
            {
                { Error.CountNotEnougth, "product count is not enough" }
            });
        }
    }
}
