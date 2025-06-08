using System.ComponentModel.DataAnnotations;

namespace LINQ.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
