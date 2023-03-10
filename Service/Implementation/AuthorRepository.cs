using AutoMapper;
using BookAPI.Data;
using BookAPI.Dto;
using BookAPI.Model;
using BookAPI.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Service.Implementation
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _dbContext;
       
        public AuthorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
           
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _dbContext.Authors.ToListAsync();

        }

        public async Task<Author> GetByIdAsync(int id)
        {
            var author = await _dbContext.Authors.FindAsync(id);
            return author;
        }

        public async Task AddAsync(Author entity)
        {
           
            await _dbContext.Authors.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<IEnumerable<Book>> GetBooksAttachedToAuthor(int authorId)
        {
            var books = await _dbContext.Books.Where(b => b.AuthorId == authorId).ToListAsync();
            
            return books;
        }

        //Task<IEnumerable<Author>> IRepository<Author>.GetAllAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //Task<Author> IRepository<Author>.GetByIdAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<Author> IRepository<Author>.AddAsync(Author entity)
        //{
        //    throw new NotImplementedException();
        //}
    }

}
