using System;

namespace BooksLibrary.Application.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; } = null!;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
