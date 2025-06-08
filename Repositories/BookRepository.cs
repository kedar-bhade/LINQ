using System;
using System.Collections.Generic;
using System.Linq;
using LINQ.Models;

namespace LINQ.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly List<Book> _books = new();
        private int _nextId = 1;
        private readonly IAuthorRepository _authors;

        public BookRepository(IAuthorRepository authors)
        {
            _authors = authors;
        }

        public IEnumerable<Book> GetAll() => _books;

        public Book Get(int id) => _books.FirstOrDefault(b => b.Id == id);

        public Book Create(Book book)
        {
            if (_authors.Get(book.AuthorId) == null)
            {
                throw new ArgumentException("Author does not exist", nameof(book.AuthorId));
            }

            book.Id = _nextId++;
            _books.Add(book);
            return book;
        }

        public void Update(Book book)
        {
            var existing = Get(book.Id);
            if (existing != null)
            {
                if (_authors.Get(book.AuthorId) == null)
                {
                    throw new ArgumentException("Author does not exist", nameof(book.AuthorId));
                }

                existing.Title = book.Title;
                existing.AuthorId = book.AuthorId;
            }
        }

        public void Delete(int id)
        {
            var book = Get(id);
            if (book != null)
            {
                _books.Remove(book);
            }
        }
    }
}
