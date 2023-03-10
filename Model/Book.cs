using System.ComponentModel.DataAnnotations.Schema;

namespace BookAPI.Model
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        //[ForeignKey("Publisher")]
        //public int PublisherId { get; set; }
        public Author Author { get; set; }
        //public Publisher Publisher { get; set; }
    }

}
