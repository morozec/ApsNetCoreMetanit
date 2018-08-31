using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HelloApp.Services
{
    public class MessageService
    {
        private IMessageSender _sender;
        public MessageService(IMessageSender sender)
        {
            _sender = sender;
        }

        public string SendMessage() => _sender.Send();
    }
}