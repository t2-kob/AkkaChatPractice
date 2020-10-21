using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AkkaChat
{
    public class ReturnMessage
    {
        public ReturnMessage(string message)
        {
            Message = message;
        }

         public string Message { get; }
    }
}
