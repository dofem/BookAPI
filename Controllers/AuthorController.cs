using AutoMapper;
using BookAPI.Dto;
using BookAPI.Model;
using BookAPI.Service.Implementation;
using BookAPI.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateAuthor(CreateAuthor createAuthor)
        {
            try
            {
                var author = _mapper.Map<Author>(createAuthor); // map CreateAuthor to Author
                await _authorRepository.AddAsync(author);
                var result = _mapper.Map<CreateAuthor>(author);
                return new ApiResponse { StatusCode = HttpStatusCode.Created, IsSuccess = true, Result = result };
            }
            catch (Exception ex)
            {
                var errors = new List<string> { ex.Message };
                return new ApiResponse { StatusCode = HttpStatusCode.InternalServerError, IsSuccess = false, ErrorMessages = errors };
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetAuthorById(int id)
        {
            try
            {
                var author = await _authorRepository.GetByIdAsync(id);
                if (author == null)
                {
                    return new ApiResponse { StatusCode = HttpStatusCode.NotFound, IsSuccess = false };
                }
                var result = _mapper.Map<CreateAuthor>(author);
                return new ApiResponse { StatusCode = HttpStatusCode.OK, IsSuccess = true, Result = result };
            }
            catch (Exception ex)
            {
                var errors = new List<string> { ex.Message };
                return new ApiResponse { StatusCode = HttpStatusCode.InternalServerError, IsSuccess = false, ErrorMessages = errors };
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllAuthors()
        {
            try
            {
                var authors = await _authorRepository.GetAllAsync();
                var result = _mapper.Map<IEnumerable<CreateAuthor>>(authors);
                return new ApiResponse { StatusCode = HttpStatusCode.OK, IsSuccess = true, Result = result };
            }
            catch (Exception ex)
            {
                var errors = new List<string> { ex.Message };
                return new ApiResponse { StatusCode = HttpStatusCode.InternalServerError, IsSuccess = false, ErrorMessages = errors };
            }
        }

        [HttpGet("{id}/books")]
        public async Task<ActionResult<ApiResponse>> GetBooksAttachedToAnAuthor(int id)
        {
            try
            {
                var books = await _authorRepository.GetBooksAttachedToAuthor(id);
                if (books == null)
                {
                    return new ApiResponse { StatusCode = HttpStatusCode.NotFound, IsSuccess = false };
                }
                var result = _mapper.Map<IEnumerable<CreateBook>>(books);
                return new ApiResponse { StatusCode = HttpStatusCode.OK, IsSuccess = true, Result = result };
            }
            catch (Exception ex)
            {
                var errors = new List<string> { ex.Message };
                return new ApiResponse { StatusCode = HttpStatusCode.InternalServerError, IsSuccess = false, ErrorMessages = errors };
            }
        }
    }

}
