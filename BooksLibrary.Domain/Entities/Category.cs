using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Domain.Entities
{
    public class Category
    {
        private readonly List<Book> _books = new();
        public Category(string name, string? description = null) {
            Name = name;
            Description = description;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public IReadOnlyCollection<Book> Books => _books.AsReadOnly();

        public void UpdateDetails(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

        public void AddBook(string title, string description, string author, int totalCopies, int availableCopies)
        { 
            var book = new Book(title, description, author, totalCopies, availableCopies);
            _books.Add(book);
        }
    }
}
