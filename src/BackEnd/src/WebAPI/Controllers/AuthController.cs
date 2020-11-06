namespace GrowATree.WebAPI.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using global::Application.Models.Auth;
    using GrowATree.Application.Auth.Commands;
    using GrowATree.Application.Auth.Commands.ConfirmEmail;
    using GrowATree.Application.Auth.Commands.FacebookLogin;
    using GrowATree.Application.Auth.Commands.ForgottenPassword;
    using GrowATree.Application.Auth.Commands.RefreshToken;
    using GrowATree.Application.Auth.Commands.Register;
    using GrowATree.Application.Auth.Commands.ResetPassword;
    using GrowATree.Application.Common.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Serilog;

    /// <summary>
    ///  Controller that handles
    ///  queries connected with users.
    /// </summary>
    public class AuthController : ApiController
    {
        /// <summary>
        /// Add user into db bt its credentials.
        /// </summary>
        /// <param name="registerCommand">Model with user's data.</param>
        /// <returns>Result Models with error or success.</returns>
        [HttpPost("register")]
        public async Task<ActionResult<Result<bool>>> Register([FromBody] RegisterCommand registerCommand)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    var errorMessage = this.ModelState.Values
                        .Where(x => x.ValidationState == ModelValidationState.Invalid)
                        .Select(x => x.Errors)
                        .Select(x => x.FirstOrDefault()?.ErrorMessage)
                        .FirstOrDefault();

                    return Result<bool>.Failure(errorMessage);
                }

                return await this.Mediator.Send(registerCommand);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<bool>.Failure(ErrorMessages.AccountFailureErrorMessage);
            }
        }

        /// <summary>
        /// Returns token when credentials are
        /// valid and error message when not.
        /// </summary>
        /// <param name="loginCommand">Model with credentials.</param>
        /// <returns>Result Models with error or success.</returns>
        [HttpPost("login")]
        public async Task<ActionResult<Result<TokenModel>>> Login([FromBody] LoginCommand loginCommand)
        {
            try
            {
                return await this.Mediator.Send(loginCommand);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<TokenModel>.Failure(ErrorMessages.AccountFailureErrorMessage);
            }
        }

        /// <summary>
        /// Returns token when credentials are
        /// valid and error message when not.
        /// </summary>
        /// <param name="loginCommand">Model with credentials.</param>
        /// <returns>Result Models with error or success.</returns>
        [HttpPost("external-login")]
        public async Task<ActionResult<Result<TokenModel>>> ExternalLogin([FromBody] ExternalLoginCommand externalLoginCommand)
        {
            try
            {
                return await this.Mediator.Send(externalLoginCommand);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<TokenModel>.Failure(ErrorMessages.AccountFailureErrorMessage);
            }
        }

        [HttpPost("confirm-email")]
        public async Task<ActionResult<Result<bool>>> ConfirmEmail([FromBody] ConfirmEmailCommand confirmEmailCommand)
        {
            try
            {
                return await this.Mediator.Send(confirmEmailCommand);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<bool>.Failure(ErrorMessages.ConfirmEmailError);
            }
        }

        [HttpPost("resend-link-confirm-email")]
        public async Task<ActionResult<Result<bool>>> ResendLinkConfirmEmail([FromBody] ResendConfirmationLinkCommand confirmEmailCommand)
        {
            try
            {
                return await this.Mediator.Send(confirmEmailCommand);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<bool>.Failure(ErrorMessages.ConfirmEmailError);
            }
        }

        [HttpPost("forgotten-password")]
        public async Task<ActionResult<Result<bool>>> ForgottenPassword([FromBody] ForgottenPasswordCommand command)
        {
            try
            {
                return await this.Mediator.Send(command);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<bool>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<Result<bool>>> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    var errorMessage = this.ModelState.Values
                        .Where(x => x.ValidationState == ModelValidationState.Invalid)
                        .Select(x => x.Errors)
                        .Select(x => x.FirstOrDefault()?.ErrorMessage)
                        .FirstOrDefault();

                    return Result<bool>.Failure(errorMessage);
                }

                return await this.Mediator.Send(command);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<bool>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<Result<TokenModel>>> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    var errorMessage = this.ModelState.Values
                        .Where(x => x.ValidationState == ModelValidationState.Invalid)
                        .Select(x => x.Errors)
                        .Select(x => x.FirstOrDefault()?.ErrorMessage)
                        .FirstOrDefault();

                    return Result<TokenModel>.Failure(errorMessage);
                }

                return await this.Mediator.Send(command);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<TokenModel>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }
    }
}