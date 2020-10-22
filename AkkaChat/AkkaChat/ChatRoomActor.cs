using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AkkaChat
{
    public class ChatRoomActor : ReceiveActor
    {

        private List<ChatMessage> _list;

        public ChatRoomActor(List<ChatMessage> chatMessages)
        {
            _list = chatMessages;

            Receive<AddChatMessage>(x =>
            {
                var message = new ChatMessage(x.UserName, x.Message);
                _list.Add(message);


                Sender.Tell(new ReturnMessage($"[{x.Message}] を投稿しました。"));
            });


            Receive<GetChatMessage>(x => Sender.Tell(FetchMessages()));
        }

        public ChatRoomActor() : this(new List<ChatMessage>())
        {
        }


        private IReadOnlyList<ChatMessage> FetchMessages() {
            return _list.AsReadOnly();
        }
    }
}
