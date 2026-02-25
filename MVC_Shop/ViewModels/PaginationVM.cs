namespace MVC_Shop.ViewModels
{
    public class PaginationVM
    {
        public int PageSize { get; set; } = 5;
        public int Page { get; set; } = 1;
        public int PageCount { get; set; } = 40;
    }
}
