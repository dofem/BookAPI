using AutoMapper;
using BookAPI.Data;
using BookAPI.Dto;
using BookAPI.Model;
using BookAPI.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Service.Implementation
{
    public class BookRepository : IRepository<Book>
    {
        private readonly ApplicationDbContext _dbContext;
       

        public BookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            return book;

        }

        public async Task AddAsync(Book entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
    }

}
