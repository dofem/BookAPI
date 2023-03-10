using System.ComponentModel.DataAnnotations.Schema;

namespace BookAPI.Model
{
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<Book> Books { get; set; }
    }

}