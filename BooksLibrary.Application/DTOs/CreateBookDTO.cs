using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Application.DTOs
{
    public class CreateBookDTO
    {
        [Required]
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
    }
}
