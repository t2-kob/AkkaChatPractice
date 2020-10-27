using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AkkaChat
{
    public class AddChatMessage
    {

        public AddChatMessage(string chatRoomName, string userName, string message)
        {
            ChatRoomName = chatRoomName;
            UserName = userName;
            Message = message;
        }



        public string ChatRoomName { get; }

        public string UserName { get; }
        public string Message { get; }
    }
}
