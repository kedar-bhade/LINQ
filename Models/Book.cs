using System.ComponentModel.DataAnnotations;

namespace LINQ.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public int AuthorId { get; set; }
    }
}
