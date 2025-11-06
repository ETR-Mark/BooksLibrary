using System;
using System.ComponentModel.DataAnnotations;

namespace BooksLibrary.Domain.Entities
{
    public class Review
    {
        internal Review(int bookId, string reviewerName, int rating, string comment)
        {
            BookId = bookId;
            ReviewerName = reviewerName;
            Rating = rating;
            Comment = comment;
            ReviewDate = DateTime.UtcNow;
        }

        private Review() { }

        [Key]
        public int Id { get; private set; }

        [Required]
        public int BookId { get; private set; }

        [Required]
        public string ReviewerName { get; private set; } = null!;

        [Required]
        public int Rating { get; private set; }

        public string? Comment { get; private set; }

        public DateTime ReviewDate { get; private set; }
    }
}
