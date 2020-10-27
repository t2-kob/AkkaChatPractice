using Akka.Actor;
using Akka.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AkkaChat
{
    public class ChatRoomActor : ReceiveActor
    {

        private readonly List<ChatMessage> _list = new List<ChatMessage>();

//        private string ChatRoomName { get; }
//        public override string PersistenceId => $"PersistentId-{ChatRoomName}";



        public ChatRoomActor(string chatRoomName)
        {
//            ChatRoomName = chatRoomName;


            /*
            Recover<ChatMessage>(x =>
            {
                _list.Add(x);
                Debug.WriteLine("Actor Recovered.");
            });
            */
            
            Receive<CreateChatRoomMessage>(x =>
            {
                 _list.Add(new ChatMessage("SYSTEM", "チャットルームが作成されました。"));

                var result = $"Saved ===> チャットルーム作成";

                 Debug.WriteLine(result);
                Sender.Tell(new ReturnMessage(result), Self);
            });

            Receive<AddChatMessage>(x =>
            {
//                Persist(x, y =>
//                {
                    var message = new ChatMessage(x.UserName, x.Message);
                    _list.Add(message);

                    var result = $"Saved ===> {x.UserName} posted: {x.Message}";

                    Debug.WriteLine(result);
                    Sender.Tell(new ReturnMessage(result), Self);

                //                });
            });


            Receive<GetChatMessage>(x =>
            {
                Sender.Tell(FetchMessages(), Self);
            });

        }

        private IReadOnlyList<ChatMessage> FetchMessages() {
            return _list.Select(x => x)
                        .ToList()
                        .AsReadOnly();
        }





        internal static Props Props(string chatRoomName)
        {
            return Akka.Actor.Props.Create(() => new ChatRoomActor(chatRoomName));
        }

    }
}
