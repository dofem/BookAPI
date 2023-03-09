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
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IMapper _mapper;

        public PublisherController(IPublisherRepository publisherRepository, IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreatePublisher(CreatePublisher createPublisher)
        {
            var errors = new List<string>();
            try
            {
                var publisher = _mapper.Map<Publisher>(createPublisher);
                await _publisherRepository.AddAsync(publisher);
                var createdPublisher = _mapper.Map<CreatePublisher>(publisher);

                return new ApiResponse { StatusCode = HttpStatusCode.Created, IsSuccess = true, Result = createdPublisher };
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return new ApiResponse { StatusCode = HttpStatusCode.InternalServerError, IsSuccess = false, ErrorMessages = errors };
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetPublisher(int id)
        {
            var errors = new List<string>();
            try
            {
                var publisher = await _publisherRepository.GetByIdAsync(id);
                if (publisher == null)
                {
                    errors.Add($"Publisher with id {id} not found");
                    return new ApiResponse { StatusCode = HttpStatusCode.NotFound, IsSuccess = false, ErrorMessages = errors };
                }
                var thePublisher = _mapper.Map<CreatePublisher>(publisher);

                return new ApiResponse { StatusCode = HttpStatusCode.OK, IsSuccess = true, Result = thePublisher };
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return new ApiResponse { StatusCode = HttpStatusCode.InternalServerError, IsSuccess = false, ErrorMessages = errors };
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllPublishers()
        {
            var errors = new List<string>();
            try
            {
                var publishers = await _publisherRepository.GetAllAsync();
                var thePublishers = _mapper.Map<IEnumerable<CreatePublisher>>(publishers);
                return new ApiResponse { StatusCode = HttpStatusCode.OK, IsSuccess = true, Result = thePublishers };
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return new ApiResponse { StatusCode = HttpStatusCode.InternalServerError, IsSuccess = false, ErrorMessages = errors };
            }
        }

        [HttpGet("{id}/authors")]
        public async Task<ActionResult<ApiResponse>> GetAuthorsAttachedToAPublisher(int id)
        {
            var errors = new List<string>();
            try
            {
                var publisher = await _publisherRepository.GetByIdAsync(id);
                if (publisher == null)
                {
                    errors.Add($"Publisher with id {id} not found");
                    return new ApiResponse { StatusCode = HttpStatusCode.NotFound, IsSuccess = false, ErrorMessages = errors };
                }
                var authors = await _publisherRepository.GetAuthorsAttachedToPublisher(id);
                var theAuthors = _mapper.Map<IEnumerable<CreateAuthor>>(authors);

                return new ApiResponse { StatusCode = HttpStatusCode.OK, IsSuccess = true, Result = theAuthors };
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return new ApiResponse { StatusCode = HttpStatusCode.InternalServerError, IsSuccess = false, ErrorMessages = errors };
            }
        }

    }

 }
