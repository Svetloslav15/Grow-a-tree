namespace GrowATree.Application.Models.Common.Models
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A class that is used to return data to the front end.
    /// </summary>
    /// <typeparam name="TData">Type of the entity that is returned.</typeparam>
    /// <typeparam name="TMeta">Type of the meta that is returned.</typeparam>
    public class MetaResult<TData, TMeta>
    {
        public MetaResult()
        {
            this.Succeeded = true;
            this.Errors = new string[] { };
        }

        public TData Data { get; set; }

        public TMeta Meta { get; set; }

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static T Failure<T>()
            where T : MetaResult<TData, TMeta>, new()
        {
            return new T
            {
                Succeeded = false,
                Errors = new string[] { },
            };
        }

        public static T Failure<T>(string error)
            where T : MetaResult<TData, TMeta>, new()
        {
            return new T
            {
                Succeeded = false,
                Errors = new string[] { error },
            };
        }

        public static T Failure<T>(string[] error)
            where T : MetaResult<TData, TMeta>, new()
        {
            return new T
            {
                Succeeded = false,
                Errors = error,
            };
        }
    }
}
