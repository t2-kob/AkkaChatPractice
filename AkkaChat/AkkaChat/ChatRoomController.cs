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
        private readonly ActorSystem _actorSystem;



        public ChatRoomController(ActorSystem actorSystem)
        {
            _actorSystem = actorSystem;
        }



        [HttpGet("CreateChatRoom")]
        public async Task<string> CreateChatRoom(string chatRoomName)
        {
            var chatRoomActorName = MakeActorName(chatRoomName);
            var chatRoomActor = _actorSystem.ActorOf(ChatRoomActor.Props(chatRoomActorName), chatRoomActorName);

            var createChatRoomMessage = new CreateChatRoomMessage(chatRoomName);

            var answer = await chatRoomActor.Ask<ReturnMessage>(createChatRoomMessage);
            return answer.Message;
        }


        [HttpGet("AddMessage")]
        public async Task<string> AddMessage(string userId, string message, string chatRoomName)
        {
            var chatRoomActor = _actorSystem.ActorSelection("/user/" + MakeActorName(chatRoomName));

            var addMessage = new AddChatMessage(chatRoomName, userId, message);

            var answer = await chatRoomActor.Ask<ReturnMessage>(addMessage);
            return answer.Message;

        }

        [HttpGet("GetMessages")]
        public async Task<IReadOnlyList<ChatMessage>> GetMessages(string chatRoomName)
        {
            var chatRoomActor = _actorSystem.ActorSelection("/user/" + MakeActorName(chatRoomName));

            var getChatMessage = new GetChatMessage();

            var answer = await chatRoomActor.Ask<IReadOnlyList<ChatMessage>>(getChatMessage, TimeSpan.FromSeconds(5));
            return answer;

        }



        private string MakeActorName(string chatRoomName) { 
            var chatRoomActorName = $"chat-room-actor-{chatRoomName}";
            return chatRoomActorName;
        }

    }
}
