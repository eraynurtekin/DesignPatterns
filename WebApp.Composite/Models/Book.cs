using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Composite.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
