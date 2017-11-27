namespace WebApplication4.Services
{
    public interface IMailService
    {
        void SendMessage(string to, string subject, string body);
    }
}