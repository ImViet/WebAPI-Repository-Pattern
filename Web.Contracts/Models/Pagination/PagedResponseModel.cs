namespace Web.Contracts.Models
{
    public class PagedResponseModel<T>
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; }
    }
}