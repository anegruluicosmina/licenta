using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.ViewModel
{
    public class PaginatedList<T> : List<T> {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            //index of the page
            PageIndex = pageIndex;
            //total number og pages
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }
        /*model has previous page as long as page index>1*/
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }
        /* the page has a next page as long as index is lower then the total number of pages*/
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            //take item after the items already showed, in the number of the items in the page
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            //create the paginted list object with the items in the list
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }      
}
