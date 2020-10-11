namespace GrowATree.Application.Models.Common.Models
{
    public class Pagination
    {
        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public int PerPage { get; set; }
    }

    public class PaginationMeta
    {
        public Pagination Pagination { get; set; }
    }
}
