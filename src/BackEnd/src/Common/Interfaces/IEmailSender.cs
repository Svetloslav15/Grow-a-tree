namespace Common.Interfaces
{
    using System.Threading.Tasks;
    using GrowATree.Domain.Entities;

    public interface IEmailSender
    {
        Task<bool> SendEmail(User receiver, string mailDescription, string subject);
    }
}