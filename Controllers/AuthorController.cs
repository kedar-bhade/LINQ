using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LINQ.Models;
using LINQ.Repositories;

namespace LINQ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _repository;

        public AuthorController(IAuthorRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Author>> GetAll() => Ok(_repository.GetAll());

        [HttpGet("{id}")]
        public ActionResult<Author> Get(int id)
        {
            var author = _repository.Get(id);
            return author == null ? NotFound() : Ok(author);
        }

        [HttpPost]
        public ActionResult<Author> Create([FromBody] Author author)
        {
            var created = _repository.Create(author);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Author author)
        {
            if (id != author.Id) return BadRequest();
            _repository.Update(author);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return NoContent();
        }
    }
}
