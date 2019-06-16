using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models.Pages
{
    public class PageList<T>:List<T>{

        public PageList(IQueryable<T> query, QueryOptions options=null) {     //query - list of products
            CurrentPage = options.CurrentPage;
            PageSize = options.PageSize;
            TotalPages = query.Count()/PageSize;
            AddRange(query.Skip((CurrentPage - 1) * PageSize).Take(PageSize)); //adding in class as well as in collection
        }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
