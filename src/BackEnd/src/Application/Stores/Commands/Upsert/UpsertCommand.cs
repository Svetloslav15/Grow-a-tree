namespace GrowATree.Application.Stores.Commands.Upsert
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class UpsertCommand : IRequest<Result<bool>>
    {
        public int? Id { get; set; }

        public string ApplicationUserId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Latitude { get; set; }

        public string Longitute { get; set; }

        public string City { get; set; }

        public string WorkingHours { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }
    }
}
