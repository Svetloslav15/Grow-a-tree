namespace Common.Interfaces
{
    using GrowATree.Domain.Entities;
    using System.Threading.Tasks;

    public interface IEmailSender
    {
        Task<bool> SendEmail(User receiver, string mailDescription, string subject);
    }
}