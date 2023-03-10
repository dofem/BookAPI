using BookAPI.Model;

namespace BookAPI.Service.Interface
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Book>> GetBooksAttachedToAuthor(int authorId);
    }
}
