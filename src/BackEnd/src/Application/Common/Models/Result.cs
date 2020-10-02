namespace GrowATree.Application.Common.Models
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A class that is used to return data to the front end.
    /// </summary>
    /// <typeparam name="T">Type of the entity that is returned.</typeparam>
    public class Result<T>
    {
        public Result(bool succeeded, T data, IEnumerable<string> errors)
        {
            this.Succeeded = succeeded;
            this.Errors = errors.ToArray();
            this.Data = data;
        }

        public T Data { get; set; }

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static Result<T> Success(T data)
        {
            return new Result<T>(true, data, new string[] { });
        }

        public static Result<T> Failure(IEnumerable<string> errors)
        {
            return new Result<T>(false, default(T), errors);
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>(false, default(T), new string[] { error });
        }
    }
}
