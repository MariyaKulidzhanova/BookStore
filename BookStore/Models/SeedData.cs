using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookStore.Data;
using System;
using System.Linq;

namespace BookStore.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BookStoreContext>>()))
            {
                // Look for any movies.
                if (context.Book.Any())
                {
                    return;   // DB has been seeded
                }

                context.Book.AddRange(
                    new Book
                    {
                        Title = "Anna Karenina",
                        Date = DateTime.Parse("1877-2-12"),
                        Author = "Leo Tolstoy",
                    },

                    new Book
                    {
                        Title = "To Kill a Mockingbird",
                        Date = DateTime.Parse("1960-6-11"),
                        Author = "Harper Lee"
                    },

                    new Book
                    {
                        Title = "The Great Gatsby",
                        Date = DateTime.Parse("1925-4-10"),
                        Author = "F. Scott Fitzgerald"
                    },

                    new Book
                    {
                        Title = "One Hundred Years of Solitude",
                        Date = DateTime.Parse("1967-5-15"),
                        Author = "Gabriel García Márquez"
                    },

                    new Book
                    {
                        Title = "Jane Eyre",
                        Date = DateTime.Parse("1847-10-16"),
                        Author = "Charlotte Brontë"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}