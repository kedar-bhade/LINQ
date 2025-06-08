using System.Collections.Generic;
using LINQ.Models;

namespace LINQ.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book Get(int id);
        Book Create(Book book);
        void Update(Book book);
        void Delete(int id);
    }
}
