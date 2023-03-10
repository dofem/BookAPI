using System.ComponentModel.DataAnnotations.Schema;

namespace BookAPI.Dto
{
    public class CreateBook
    {
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
    }
}
