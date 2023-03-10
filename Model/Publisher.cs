using System.ComponentModel.DataAnnotations.Schema;

namespace BookAPI.Model
{
    public class Publisher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Book> Books { get; set; }
    }

}
