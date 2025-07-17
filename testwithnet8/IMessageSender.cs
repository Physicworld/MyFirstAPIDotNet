namespace testwithnet8;

public interface IMessageSender
{
    public string Send();
}

public class WhatsappMessageSender : IMessageSender
{
    public string Send()
    {
        return "Sending message from whatsapp!";
    }
}


public class EmailMessageSender : IMessageSender
{
    public string Send()
    {
        return "Sending message from email!";
    }
}