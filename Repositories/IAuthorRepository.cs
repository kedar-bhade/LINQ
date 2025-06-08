using System.Collections.Generic;
using LINQ.Models;

namespace LINQ.Repositories
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAll();
        Author Get(int id);
        Author Create(Author author);
        void Update(Author author);
        void Delete(int id);
    }
}
