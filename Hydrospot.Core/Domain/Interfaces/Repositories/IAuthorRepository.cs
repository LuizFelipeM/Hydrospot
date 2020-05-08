using Hydrospot.Core.Domain.Dtos;
using Hydrospot.Core.Domain.Entity;
using System.Collections.Generic;

namespace Hydrospot.Core.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        long Save(AuthorDto author);
        IEnumerable<Author> GetAuthorByName(string name);
    }
}
