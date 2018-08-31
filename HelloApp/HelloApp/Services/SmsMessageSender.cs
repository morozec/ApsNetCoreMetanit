namespace HelloApp.Services
{
    public class SmsMessageSender : IMessageSender
    {
        public string Send()
        {
            return "Message is sent by sms.";
        }
    }
}