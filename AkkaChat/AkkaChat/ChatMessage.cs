namespace AkkaChat
{
    public class ChatMessage
    {
        private string _userName;
        private string _message;

        public ChatMessage(string userName, string message)
        {
            this.UserName = userName;
            this.Message = message;
        }

        public string UserName { 
            get => _userName; 
            private set => _userName = value;
        }
        public string Message { 
            get => _message; 
            private set => _message = value; 
        }
    }
}