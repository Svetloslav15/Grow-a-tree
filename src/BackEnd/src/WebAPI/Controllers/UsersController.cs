namespace GrowATree.WebAPI.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.Users;
    using GrowATree.Application.Users.Queries.GetAll;
    using GrowATree.Application.Users.Queries.GetById;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

    public class UsersController : ApiController
    {

        /// <summary>
        /// Add user into db bt its credentials.
        /// </summary>
        /// <param name="id">Wanted user's id.</param>
        /// <returns>Result Models with error or success.</returns>
        [HttpGet("{id}")]
        public async Task<Result<UserModel>> GetById(string id)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    var errorMessage = this.ModelState.Values
                        .Select(x => x.Errors)
                        .Select(x => x.FirstOrDefault()?.ErrorMessage)
                        .FirstOrDefault();

                    return Result<UserModel>.Failure(errorMessage);
                }

                var query = new GetUserByIdQuery { Id = id };
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<UserModel>.Failure(ErrorMessages.AccountFailureErrorMessage);
            }
        }

        [HttpGet("all")]
        public async Task<Result<UsersListModel>> GetAll([FromQuery] GetAllUsersQuery query)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    var errorMessage = this.ModelState.Values
                        .Select(x => x.Errors)
                        .Select(x => x.FirstOrDefault()?.ErrorMessage)
                        .FirstOrDefault();

                    return Result<UsersListModel>.Failure(errorMessage);
                }

                var result = await this.Mediator.Send(query);
                return Result<UsersListModel>.Success(result);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<UsersListModel>.Failure(ErrorMessages.AccountFailureErrorMessage);
            }
        }
    }
}
