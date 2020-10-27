using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AkkaChat
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        private ActorSystem _actorSystem;

        protected static IActorRef ChatRoomActor;


        public ChatRoomController(ActorSystem actorSystem)
        {
            _actorSystem = actorSystem;

            if (ChatRoomActor == null)
            {
                var chatRoomActorProps = Props.Create<ChatRoomActor>();
                ChatRoomActor = _actorSystem.ActorOf(chatRoomActorProps, "my-chat-room-actor");
            }

        }


        [HttpGet("AddMessage")]
        public async Task<string> AddMessage(string userId, string message)
        {
            var addMessage = new AddChatMessage(userId, message);

            var answer = await ChatRoomActor.Ask<ReturnMessage>(addMessage);
            return answer.Message;

        }

        [HttpGet("GetMessages")]
        public async Task<IReadOnlyList<ChatMessage>> GetMessages()
        {
            var answer = await ChatRoomActor.Ask<IReadOnlyList<ChatMessage>>(new GetChatMessage());
            return answer;

        }

    }
}
