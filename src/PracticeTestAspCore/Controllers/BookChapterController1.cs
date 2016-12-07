using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticeTestAspCore.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PracticeTestAspCore.Controllers
{
        [Produces("application/json", "application/xml")]

        [Route("api/[controller]")]
        public class BookChaptersController : Controller
        {
            private readonly IBookChaptersRepository _repository;

            public BookChaptersController(IBookChaptersRepository bookChapterRepository)
            {
                _repository = bookChapterRepository;
            }

            // GET: api/bookchapters
            [Route("/api/Bookchapters")]
            [HttpGet]
            public IEnumerable<BookChapter> GetBookChapters() => _repository.GetAll();


            // GET api/bookchapters/guid
            [HttpGet("{id}", Name = nameof(GetBookChapterById))]
            public IActionResult GetBookChapterById(Guid id)
            {
                BookChapter chapter = _repository.Find(id);

                if (chapter == null)
                    return NotFound();
                else
                    return new ObjectResult(chapter);
            }

            //Add a new book chapter
            // POST api/bookchapters
            [HttpPost]
            public IActionResult PostBookChapter([FromBody]BookChapter chapter)
            {
                if (chapter == null)
                    return BadRequest();
                _repository.Add(chapter);
                return CreatedAtRoute(nameof(GetBookChapterById), new { id = chapter.Id }, chapter);

            }

            // Update items is based on the HTTP PUT request
            // PUT api/bookchapters/guid
            [HttpPut("{id}")]
            public IActionResult PutBookChapter(Guid id, [FromBody]BookChapter chapter)
            {
                if ((chapter == null) || (id != chapter.Id))
                    return BadRequest();

                if (_repository.Find(id) == null)
                    return NotFound();

                _repository.Update(chapter);
                return new NoContentResult();
            }

            //with the HTTP DELETE requst, book chapters are simply remomved from the dictionary
            // DELETE api/bookchapters/5

            [HttpDelete("{id}")]
            public void Delete(Guid id) => _repository.Remove(id);

        }
    }
