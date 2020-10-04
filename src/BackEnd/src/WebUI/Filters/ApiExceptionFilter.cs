namespace GrowATree.WebUI.Filters
{
    using System;
    using System.Collections.Generic;
    using GrowATree.Application.Common.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Middleware that handle Exceptions.
    /// </summary>
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> exceptionHandlers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiExceptionFilter"/> class.
        /// </summary>
        public ApiExceptionFilter()
        {
            // Register known exception types and handlers.
            this.exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), this.HandleValidationException },
                { typeof(NotFoundException), this.HandleNotFoundException },
            };
        }

        /// <summary>
        /// Method that handles when there is an exception.
        /// </summary>
        /// <param name="context">ExceptionContext.</param>
        public override void OnException(ExceptionContext context)
        {
            this.HandleException(context);

            base.OnException(context);
        }

        /// <summary>
        /// Method that handles when there is an exception.
        /// </summary>
        /// <param name="context">ExceptionContext.</param>
        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (this.exceptionHandlers.ContainsKey(type))
            {
                this.exceptionHandlers[type].Invoke(context);
                return;
            }

            this.HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };

            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;

            var details = new ValidationProblemDetails(exception.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception.Message,
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}