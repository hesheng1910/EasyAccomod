using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAccomod.Core.Model.PagedList
{
    public class PageList
    {
        public int Total { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int LastPage { get; set; }
        public bool HasPreviusPage { get; set; }
        public bool HasNextPage { get; set; }

        public PageList(int Count, int currentPage, int perPage)
        {
            Total = Count;
            LastPage = Total / perPage;
            if (Total % perPage > 0)
                LastPage++;
            CurrentPage = currentPage;
            PerPage = perPage;
            HasPreviusPage = (currentPage > 1);
            HasNextPage = (currentPage + 1 < LastPage); 
        }
    }
}
