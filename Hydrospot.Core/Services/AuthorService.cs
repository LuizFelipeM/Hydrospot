using Hydrospot.Core.Interfaces.Repositories;
using Hydrospot.Core.Domain;
using Hydrospot.Core.Domain.Dtos;
using Hydrospot.Core.Utils;
using System;
using System.Linq;

namespace Hydrospot.Core.Services
{
    public class AuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public ResponseDto CreateAuthor(AuthorDto author)
        {
            if (author == null)
                throw new ArgumentException(nameof(author));

            ResponseDto response;

            if (!_authorRepository.GetAuthorByName(author.Name).Any())
            {
                long id = _authorRepository.Save(author);

                response = ResponseUtils.CreateResponseDto(StatusCodeEnum.Success, id);
            }
            else
                response = ResponseUtils.CreateResponseDto(StatusCodeEnum.ErrorSameNameAuthors);

            return response;
        }
    }
}
