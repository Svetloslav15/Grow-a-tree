namespace GrowATree.WebUI.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using global::Application.Models.Auth;
    using GrowATree.Application.Auth.Commands;
    using GrowATree.Application.Auth.Commands.Register;
    using GrowATree.Application.Common.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///  Controller that handles
    ///  queries connected with users.
    /// </summary>
    public class AuthController : ApiController
    {
        /// <summary>
        /// Add user into db bt its credentials.
        /// </summary>
        /// <param name="registerCommand">Model with user's data</param>
        /// <returns>Result Models with error or success.</returns>
        [HttpPost("register")]
        public async Task<Result<TokenModel>> Register([FromBody] RegisterCommand registerCommand)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    var errorMessage = this.ModelState.Values
                        .Select(x => x.Errors)
                        .Select(x => x.FirstOrDefault()?.ErrorMessage)
                        .FirstOrDefault();

                    return Result<TokenModel>.Failure(errorMessage);
                }

                return await this.Mediator.Send(registerCommand);
            }
            catch (Exception ex)
            {
                // TODO: add exception logger
                Debug.WriteLine(ex.Message);
                return Result<TokenModel>.Failure(ErrorMessages.AccountFailureErrorMessage);
            }
        }

        [HttpPost("login")]
        public async Task<Result<TokenModel>> Login([FromBody] LoginCommand loginCommand)
        {
            try
            {
                return await this.Mediator.Send(loginCommand);
            }
            catch (Exception ex)
            {
                // TODO: add exception logger
                Debug.WriteLine(ex.Message);
                return Result<TokenModel>.Failure(ErrorMessages.AccountFailureErrorMessage);
            }
        }
    }
}
