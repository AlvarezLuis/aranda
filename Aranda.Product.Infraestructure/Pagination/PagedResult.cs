using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aranda.Product.Infraestructure.Pagination
{
    public class PagedResult<T> : PagedBase where T : class
    {
        public IList<T> Results { get; set; }

        public PagedResult(List<T> items, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            PageSize = pageSize;
            RowCount = items.Count();

            var pageCount = (double)RowCount / PageSize;
            PageCount = (int)Math.Ceiling(pageCount);
            Results = items;
        }
    }
}
