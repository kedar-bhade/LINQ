using System.Collections.Generic;
using System.Linq;
using LINQ.Models;

namespace LINQ.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly List<Author> _authors = new();
        private int _nextId = 1;

        public IEnumerable<Author> GetAll() => _authors;

        public Author Get(int id) => _authors.FirstOrDefault(a => a.Id == id);

        public Author Create(Author author)
        {
            author.Id = _nextId++;
            _authors.Add(author);
            return author;
        }

        public void Update(Author author)
        {
            var existing = Get(author.Id);
            if (existing != null)
            {
                existing.Name = author.Name;
            }
        }

        public void Delete(int id)
        {
            var author = Get(id);
            if (author != null)
            {
                _authors.Remove(author);
            }
        }
    }
}
