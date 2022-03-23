using System.Collections.Generic;

namespace WebApp.Composite.Models
{
    public class Category
    {
        //Id Name      UserId ReferenceId //Üst kategoriyi tutuyor 0 ise üst kategorisi yok.
        //1 kitaplar    1        0 
        //2 kitaplar1   1        1    
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }

        public ICollection<Book> Books { get; set; }
        public int ReferenceId { get; set; }

    }
}
