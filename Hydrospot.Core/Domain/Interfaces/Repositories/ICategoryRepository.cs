using Hydrospot.Core.Domain.Dtos;
using Hydrospot.Core.Domain.Entity;
using System.Collections.Generic;

namespace Hydrospot.Core.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        long Save(CategoryDto category);
        IEnumerable<Category> GetCategoryByName(string name);
    }
}
