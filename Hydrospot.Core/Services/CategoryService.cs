using Hydrospot.Core.Interfaces.Repositories;
using Hydrospot.Core.Domain;
using Hydrospot.Core.Domain.Dtos;
using Hydrospot.Core.Utils;
using System;
using System.Linq;

namespace Hydrospot.Core.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public ResponseDto CreateCategory(CategoryDto category)
        {
            if (category == null)
                throw new ArgumentException(nameof(category));

            ResponseDto response;

            if (!_categoryRepository.GetCategoryByName(category.Name).Any())
            {
                long id = _categoryRepository.Save(category);

                response = ResponseUtils.CreateResponseDto(StatusCodeEnum.Success, id);
            }
            else
                response = ResponseUtils.CreateResponseDto(StatusCodeEnum.ErrorSameNameCategories);

            return response;
        }
    }
}
