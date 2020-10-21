using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AkkaChat
{
    public class AddChatMessage
    {
        public AddChatMessage(string userName, string message)
        {
            UserName = userName;
            Message = message;
        }

        public string UserName { get; }
        public string Message { get; }
    }
}
