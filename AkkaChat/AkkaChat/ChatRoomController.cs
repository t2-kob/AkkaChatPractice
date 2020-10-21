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

        public ChatRoomController(ActorSystem actorSystem)
        {
            _actorSystem = actorSystem;

        }


        [HttpGet]
        public async Task<string> Post(string userId, string message)
        {
            var chatRoomActorProps = Props.Create<ChatRoomActor>();
            var chatRoomActorRef = _actorSystem.ActorOf(chatRoomActorProps);

            var addMessage = new AddChatMessage(userId, message);

            var answer = await chatRoomActorRef.Ask<ReturnMessage>(addMessage);
            return answer.Message;

        }
    }
}
