namespace AkkaChat
{
    internal class CreateChatRoomMessage
    {
        private string chatRoomId;

        public CreateChatRoomMessage(string chatRoomId)
        {
            this.chatRoomId = chatRoomId;
        }
    }
}