namespace HelloApp.Services
{
    public class EmailMessageSender : IMessageSender
    {
        public string Send()
        {
            return "Message is sent by email.";
        }
    }
}