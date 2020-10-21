using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AkkaChat
{
    public class ChatRoomActor : ReceiveActor
    {
        public ChatRoomActor()
        {
            Receive<AddChatMessage>(x =>
            {
                Sender.Tell(new ReturnMessage($"[{x.Message}] を投稿しました。"));
            });
        }
    }
}
