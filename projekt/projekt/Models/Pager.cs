namespace projekt.Models
{
    public class Pager
    {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }//początek paska
        public int EndPage { get; private set; }//koniec paska
        public Pager() { }
        public Pager(int totalItems, int currentPage, int pageSize = 25)
        {
            TotalItems = totalItems;
            PageSize = pageSize;
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            CurrentPage = currentPage;
            int startPage = currentPage - 5;
            int endPage = currentPage + 4;
            if (startPage < 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }
            TotalItems = totalItems;
            TotalPages = totalPages;
            CurrentPage = currentPage;
            StartPage = startPage;
            EndPage = endPage;
            PageSize = pageSize;
        }
    }
}
