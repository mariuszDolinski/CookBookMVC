using Microsoft.AspNetCore.Mvc.Rendering;

namespace CookBook.MVC.Models
{
    public class Pagination
    {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }

        public int TotalPages { get; private set; } 
        public int StartPage { get; private set; }//pierwszy przycisk z numerem strony
        public int EndPage { get; private set; }//ostatni przycisk z numerem strony
        public int From { get; private set; }
        public int To { get; private set; }

        public string Action { get; set; } = "Index";
        public string? SearchPhrase { get; set; }
        public string? SortOrder { get; set; }

        public Pagination(int totalItems, int currentPage, int pageSize, string actionName = "Index")
        {
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            Action = actionName;

            TotalPages = (int)Math.Ceiling(totalItems / (decimal)pageSize);

            //ustawiamy odpowiednio przysiski z numerami stron (+/- 2 od obecnej)
            int startPage = CurrentPage - 2;
            int endPage = CurrentPage + 2;
            if(startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }
            if(endPage > TotalPages)
            {
                endPage = TotalPages;
                if(endPage > 5)
                {
                    startPage = endPage - 4;
                }
            }

            From = (CurrentPage - 1) * PageSize + 1;
            To = From - 1 + PageSize;
            if(To > TotalItems)
            {
                To = TotalItems;
            }

            if(TotalItems == 0)
            {
                StartPage = 0;
                From = 0;
                To = 0;
                CurrentPage = 0;
            }
            else if(TotalPages <= 5)
            {
                StartPage = 1;
                EndPage = TotalPages;
            }
            else
            {
                StartPage = startPage;
                EndPage = endPage;
            }
        }

        public List<SelectListItem> GetPageSizes()
        {
            var pageSizes = new List<SelectListItem>();

            for(int i = 5; i <= 20; i += 5)
            {
                if(i != 15)
                {
                    if (i == PageSize)
                    {
                        pageSizes.Add(new SelectListItem(i.ToString(), i.ToString(), true));
                    }
                    else
                    {
                        pageSizes.Add(new SelectListItem(i.ToString(), i.ToString()));
                    }
                }
            }

            return pageSizes;
        }
    }
}
