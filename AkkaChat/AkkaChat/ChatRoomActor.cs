using Akka.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AkkaChat
{
    public class ChatRoomActor : ReceivePersistentActor
    {

        private readonly List<ChatMessage> _list = new List<ChatMessage>();

        public override string PersistenceId => "my-chat-room-actorId";

        public ChatRoomActor()
        {
            Recover<ChatMessage>(x =>
            {
                _list.Add(x);
                Debug.WriteLine("Actor Recovered.");
            });


            Command<AddChatMessage>(x =>
            {
                Persist(x, y =>
                {
                    var message = new ChatMessage(x.UserName, x.Message);
                    _list.Add(message);

                    Debug.WriteLine($"Saved ===> {x.UserName} posted: {x.Message}");
                });
            });


            Command<GetChatMessage>(x =>
            {
                Sender.Tell(FetchMessages(), Self);
            });
        }


        private IReadOnlyList<ChatMessage> FetchMessages() {
            return _list.Select(x => x)
                        .ToList()
                        .AsReadOnly();
        }
    }
}
