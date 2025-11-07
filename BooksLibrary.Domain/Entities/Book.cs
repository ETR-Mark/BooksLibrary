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
        private readonly List<Review> _reviews = new();

        public Book() { }

        public Book(string title, string description, string author, int totalCopies, int availableCopies)
        {
            Title = title;
            Description = description;
            Author = author;
            TotalCopies = totalCopies;
            AvailableCopies = availableCopies;
        }

        [Key]
        public int Id { get; private set; }
        [Required]
        public int CategoryId { get; private set; }
        public Category Category { get; private set; } = null!;
        [Required]
        public string Title { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        [Required]
        public string Author { get; private set; } = null!;
        public int TotalCopies { get; private set; }
        public int AvailableCopies { get; private set; }
        
        public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();

        public bool IsAvailable()
        {
            if (AvailableCopies > 0) return true;
            return false;
        }

        public void UpdateDetails(string title, string description, string author)
        {
            Title = title;
            Description = description;
            Author = author;
        }

        public void UpdateStock(int totalCopies, int availableCopies)
        {
            if (availableCopies > totalCopies)
            {
                throw new ArgumentException("Available copies cannot exceed total copies.");
            }
            TotalCopies = totalCopies;
            AvailableCopies = availableCopies;
        }

        public void AddReview(string reviewerName, int rating, string comment)
        {
            if (rating < 1 || rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }

            var review = new Review(Id, reviewerName, rating, comment);
            _reviews.Add(review);
        }
    }
}
