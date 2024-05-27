namespace TripApp.Entities;

public class PagedList<T> : List<T>
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int AllPages { get; set; }
}