using System.ComponentModel.DataAnnotations;

namespace BooksLibrary.Application.DTOs
{
    public class CreateReviewDTO
    {
        [Required]
        public string ReviewerName { get; set; } = null!;

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }
    }
}
