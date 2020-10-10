namespace GrowATree.Application.Auth.Commands.Login
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Application.Models.Auth;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenModel>>
    {
        private readonly IIdentityService identityService;

        public LoginCommandHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public async Task<Result<TokenModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await this.identityService.LoginAsync(request.Email, request.Password);

            return result;
        }
    }
}
