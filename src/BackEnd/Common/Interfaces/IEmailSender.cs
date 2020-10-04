namespace Common.Interfaces
{
    using GrowATree.Domain.Entities;

    public interface IEmailSender
    {
        void SendEmail(User receiver, string mailDescription, string subject);
    }
}