using BookAPI.Dto;
using BookAPI.Model;

namespace BookAPI.Service.Interface
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        // other methods in the interface...

        Task<IEnumerable<Author>> GetAuthorsAttachedToPublisher(int publisherId);
    }

}
