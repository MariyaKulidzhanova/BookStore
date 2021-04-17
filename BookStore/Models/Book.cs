using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Book
    {
        public int ID { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string Title { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Author { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

    }
}
