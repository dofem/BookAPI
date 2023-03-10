using AutoMapper;
using BookAPI.Dto;
using BookAPI.Model;
using BookAPI.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IMapper _mapper;

            public BookController(IRepository<Book> bookRepository, IMapper mapper)
            {
                _bookRepository = bookRepository;
                _mapper = mapper;
            }

            [HttpPost("CreateBook")]
            public async Task<ActionResult<ApiResponse>> CreateBook(CreateBook createBook)
            {
                var errors = new List<string>();
                try
                {
                    var book = _mapper.Map<Book>(createBook);
                    await _bookRepository.AddAsync(book);
                    var createdBook = _mapper.Map<CreateBook>(book); // map Book to CreateBook
                    return new ApiResponse { StatusCode = HttpStatusCode.Created, IsSuccess = true, Result = createdBook };
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                    return new ApiResponse { StatusCode = HttpStatusCode.InternalServerError, IsSuccess = false, ErrorMessages = errors };
                }
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<ApiResponse>> GetBookById(int id)
            {
                var errors = new List<string>();
                try
                {
                    var book = await _bookRepository.GetByIdAsync(id);
                    if (book == null)
                    {
                        errors.Add($"Book with id {id} not found");
                        return new ApiResponse { StatusCode = HttpStatusCode.NotFound, IsSuccess = false, ErrorMessages = errors };
                    }
                    var thebook = _mapper.Map<CreateBook>(book);
                    return new ApiResponse { StatusCode = HttpStatusCode.OK, IsSuccess = true, Result = thebook };
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                    return new ApiResponse { StatusCode = HttpStatusCode.InternalServerError, IsSuccess = false, ErrorMessages = errors };
                }
            }

            [HttpGet]
            public async Task<ActionResult<ApiResponse>> GetAllBooks()
            {
                var errors = new List<string>();
                try
                {
                    var books = await _bookRepository.GetAllAsync();
                    var thebooks = _mapper.Map<IEnumerable<CreateBook>>(books);
                    return new ApiResponse { StatusCode = HttpStatusCode.OK, IsSuccess = true, Result = thebooks };
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                    return new ApiResponse { StatusCode = HttpStatusCode.InternalServerError, IsSuccess = false, ErrorMessages = errors };
                }
            }
        }

    }
