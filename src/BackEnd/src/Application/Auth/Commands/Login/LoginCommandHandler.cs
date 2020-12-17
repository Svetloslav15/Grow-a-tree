namespace GrowATree.Application.Auth.Commands.Login
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Application.Models.Auth;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;

    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenModel>>
    {
        private readonly IIdentityService identityService;
        private readonly IApplicationDbContext context;

        public LoginCommandHandler(IIdentityService identityService, IApplicationDbContext context)
        {
            this.identityService = identityService;
            this.context = context;
        }

        public async Task<Result<TokenModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await this.identityService.LoginAsync(request.Email, request.Password);

            if (result.Succeeded)
            {
                var newLogin = new LoginHistory
                {
                    Date = DateTime.Now,
                    DeviceName = request.DeviceName,
                    Ip = request.Ip,
                    UserId = result.Data.Id,
                };

                await this.context.LoginHistory.AddAsync(newLogin);
                await this.context.SaveChangesAsync(CancellationToken.None);
            }

            return result;
        }
    }
}
