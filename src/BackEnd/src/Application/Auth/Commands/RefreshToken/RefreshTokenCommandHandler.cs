namespace GrowATree.Application.Auth.Commands.RefreshToken
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation.Results;
    using global::Application.Models.Auth;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using Microsoft.AspNet.Identity;
    using Microsoft.EntityFrameworkCore;

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<TokenModel>>
    {
        private readonly IIdentityService identityService;
        private readonly IApplicationDbContext context;

        public RefreshTokenCommandHandler(IIdentityService identityService, IApplicationDbContext context)
        {
            this.identityService = identityService;
            this.context = context;
        }

        public async Task<Result<TokenModel>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var accessToken = request.AccessToken;
            var refreshToken = request.RefreshToken;

            var principal = this.identityService.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name;

            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return Result<TokenModel>.Failure(ErrorMessages.AccessTokenInvalidErrorMessage);
            }

            var newTokenModel = await this.identityService.GenerateTokenModel(user);

            user.RefreshToken = newTokenModel.RefreshToken;

            await this.context.SaveChangesAsync(cancellationToken);

            return Result<TokenModel>.Success(newTokenModel);
        }
    }
}
