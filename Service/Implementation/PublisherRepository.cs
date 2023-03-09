using AutoMapper;
using BookAPI.Data;
using BookAPI.Dto;
using BookAPI.Model;
using BookAPI.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Service.Implementation
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public PublisherRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }

        public async Task<IEnumerable<Publisher>> GetAllAsync()
        {
            return await _dbContext.Publishers.ToListAsync();
            
        }

        public async Task<Publisher> GetByIdAsync(int id)
        {
            var publisher =  await _dbContext.Publishers.FindAsync(id);
            return publisher;
        
        }

        public async Task AddAsync(Publisher entity)
        {
            await _dbContext.Publishers.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthorsAttachedToPublisher(int id)
        {
            var publisher = _dbContext.Publishers
                .Include(p => p.Books)
                    .ThenInclude(b => b.Author)
                .SingleOrDefault(p => p.Id == id);

            if (publisher == null)
            {
                throw new ArgumentException("Publisher not found");
            }

            var authors = publisher.Books.Select(b => b.Author);

            return authors.ToList();

        }


    }

}
