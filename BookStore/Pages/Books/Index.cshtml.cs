using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;

namespace BookStore.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly BookStore.Data.BookStoreContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(BookStoreContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        public string IdSort { get; set; }
        public string DateSort { get; set; }
        public string AuthorSort { get; set; }
        public string TitleSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Book> Book { get; set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            {

                CurrentSort = sortOrder;
                IdSort = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
                TitleSort = sortOrder == "Title" ? "title_desc" : "Title";
                DateSort = sortOrder == "Date" ? "date_desc" : "Date";
                AuthorSort = sortOrder == "Author" ? "author_desc" : "Author";
                
                if (searchString != null)
                {
                    pageIndex = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                CurrentFilter = searchString;

                IQueryable<Book> books = from m in _context.Book
                                         select m;

                if (!string.IsNullOrEmpty(searchString))
                {
                    books = books.Where(s => s.Title.Contains(searchString) ||
                                                s.Author.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "id_desc":
                        books = books.OrderByDescending(s => s.ID);
                        break;
                    case "Title":
                        books = books.OrderBy(s => s.Title);
                        break;
                    case "title_desc":
                        books = books.OrderByDescending(s => s.Title);
                        break;
                    case "Date":
                        books = books.OrderBy(s => s.Date);
                        break;
                    case "date_desc":
                        books = books.OrderByDescending(s => s.Date);
                        break;
                    case "Author":
                        books = books.OrderBy(s => s.Author);
                        break;
                    case "author_desc":
                        books = books.OrderByDescending(s => s.Author);
                        break;
                    default:
                        books = books.OrderBy(s => s.ID);
                        break;
                }

                var pageSize = Configuration.GetValue("PageSize", 4);
                Book = await PaginatedList<Book>.CreateAsync(
                    books.AsNoTracking(), pageIndex ?? 1, pageSize);
            }
        }
    }
}
