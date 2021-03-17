namespace GrowATree.WebAPI.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.LoginHistory;
    using GrowATree.Application.Models.Users;
    using GrowATree.Application.Users.Commands.ChangeProfilePicture;
    using GrowATree.Application.Users.Commands.Edit;
    using GrowATree.Application.Users.Commands.ToggleLock;
    using GrowATree.Application.Users.Queries.GetAll;
    using GrowATree.Application.Users.Queries.GetAllShortInfo;
    using GrowATree.Application.Users.Queries.GetById;
    using GrowATree.Application.Users.Queries.GetLoginHistory;
    using GrowATree.Application.Users.Queries.GetReferrers;
    using GrowATree.Application.Users.Queries.GetShortInfoById;
    using GrowATree.Application.Users.Queries.GetTrees;
    using GrowATree.Application.Users.Queries.UserNearTree;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Serilog;

    public class UsersController : ApiController
    {

        /// <summary>
        /// Add user into db bt its credentials.
        /// </summary>
        /// <param name="id">Wanted user's id.</param>
        /// <returns>Result Models with error or success.</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<UserModel>>> GetById(string id)
        {
            try
            {
                var query = new GetUserByIdQuery { Id = id };
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<UserModel>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [HttpGet("short-info/{id}")]
        public async Task<ActionResult<Result<UserShortInfoModel>>> GetShortInfoById(string id)
        {
            try
            {
                var query = new GetUserShortInfoQuery { Id = id };
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<UserShortInfoModel>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserListModel>> GetList([FromQuery] GetUserListQuery query)
        {
            try
            {
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return UserListModel.Failure<UserListModel>(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [HttpGet("list-short-info")]
        public async Task<ActionResult<UserListShortInfoModel>> GetListShortInfo([FromQuery] GetUserListShortInfoQuery query)
        {
            try
            {
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return UserListShortInfoModel.Failure<UserListShortInfoModel>(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [HttpGet("referrals")]
        public async Task<ActionResult<UserListShortInfoModel>> GetReferrels([FromQuery] GetUserReferralListQuery query)
        {
            try
            {
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return UserListShortInfoModel.Failure<UserListShortInfoModel>(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [Authorize]
        [HttpGet("login-history")]
        public async Task<ActionResult<LoginHistoryModel>> GetLoginHistory([FromQuery] GetUserLoginHistoryQuery query)
        {
            try
            {
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return LoginHistoryModel.Failure<LoginHistoryModel>(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [HttpGet("is-user-near-tree")]
        public async Task<ActionResult<Result<bool>>> IsUserShortToTree([FromQuery] IsUserNearTreeQuery query)
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

                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<bool>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [Authorize]
        [HttpPost("edit")]
        public async Task<ActionResult<Result<UserModel>>> Edit([FromBody] EditUserCommand command)
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

                    return Result<UserModel>.Failure(errorMessage);
                }

                return await this.Mediator.Send(command);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<UserModel>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [Authorize]
        [HttpPost("change-profile-picture")]
        public async Task<ActionResult<Result<string>>> ChangeProfilePicture([FromForm] ChangeProfilePictureCommand command)
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

                    return Result<string>.Failure(errorMessage);
                }

                return await this.Mediator.Send(command);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<string>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        [HttpPost("toggle-lockout")]
        public async Task<ActionResult<Result<bool>>> ToggleLockout([FromBody] ToggleLockUserAccountCommand command)
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
    }
}
