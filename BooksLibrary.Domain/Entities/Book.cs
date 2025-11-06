using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Domain.Entities
{
    public class Book
    {
        public Book(string title, string description, string author, int totalCopies, int availableCopies)
        {
            Title = title;
            Description = description;
            Author = author;
            TotalCopies = totalCopies;
            AvailableCopies = availableCopies;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Required]
        public string Author { get; set; } = null!;
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        
        public bool IsAvailable()
        {
            if (AvailableCopies > 0) return true;
            return false;
        }
    }
}
