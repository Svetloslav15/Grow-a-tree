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
        }

        public MetaResult(bool succeeded, TData data, TMeta meta, IEnumerable<string> errors)
        {
            this.Succeeded = succeeded;
            this.Errors = errors.ToArray();
            this.Data = data;
            this.Meta = meta;
        }

        public TData Data { get; set; }

        public TMeta Meta { get; set; }

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static MetaResult<TData, TMeta> Success(TData data, TMeta meta)
        {
            return new MetaResult<TData, TMeta>(true, data, meta, new string[] { });
        }

        public static MetaResult<TData, TMeta> Failure(IEnumerable<string> errors)
        {
            return new MetaResult<TData, TMeta>(false, default(TData), default(TMeta), errors);
        }

        public static MetaResult<TData, TMeta> Failure(string error)
        {
            return new MetaResult<TData, TMeta>(false, default(TData), default(TMeta), new string[] { error });
        }
    }
}
