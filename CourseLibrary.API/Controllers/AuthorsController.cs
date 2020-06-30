using AutoMapper;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
using CourseLibrary.API.ResourceParameters;
using CourseLibrary.API.Services;
using EO.Internal;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository,
            IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        //public IActionResult GetAuthors()
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors(
            [FromQuery] AuthorResourceParameters authorResourceParameters) //string mainCategory, string searchQuery) // Improve
        {
            //throw new Exception("Test exception");
            //var authorsFromRepo = _courseLibraryRepository.GetAuthors(mainCategory, searchQuery);
            var authorsFromRepo = _courseLibraryRepository.GetAuthors(authorResourceParameters);
            //var authors = new List<AuthorDto>();

            //foreach (var author in authorsFromRepo)
            //{
            //    authors.Add(new AuthorDto()
            //    {
            //        Id = author.Id,
            //        Name = $"{author.FirstName} {author.LastName}",
            //        MainCatgory = author.MainCategory,
            //        Age = author.DateOfBirth.GetCurrentAge()
            //    });
            //}
            //return new JsonResult(authorsFromRepo);
            //return Ok(authors);
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
        }

        [HttpGet("{authorId}")]
        public IActionResult GetAuthor(Guid authorId)
        {
            var authorFromRepo = _courseLibraryRepository.GetAuthor(authorId);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            //return Ok(authorFromRepo);
            return Ok(_mapper.Map<AuthorDto>(authorFromRepo));
        }
    }
}
