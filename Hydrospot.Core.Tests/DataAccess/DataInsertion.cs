using Hydrospot.Core.Domain;
using Hydrospot.Core.Domain.Dtos;
using Hydrospot.Core.Domain.Entity;
using Hydrospot.Core.Interfaces.Repositories;
using Hydrospot.Core.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Hydrospot.Core.DataAccess
{
    public class DataInsertion
    {
        private readonly SoundDto _sound;
        private readonly AuthorDto _author;
        private readonly CategoryDto _category;
        private readonly SoundService _soundService;
        private readonly AuthorService _authorService;
        private readonly CategoryService _categoryService;
        private readonly List<Sound> _soundsAvailable;
        private readonly List<Author> _authorsAvailable;
        private readonly List<Category> _categoriesAvailable;
        private readonly Mock<ISoundRepository> _soundRepositoryMock;
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;

        public DataInsertion()
        {
            #region Author definition

            _authorsAvailable = new List<Author> { new Author
            {
                Id = 1,
                Name = "Pearl Jam",
                ImgUrl="https://i.ytimg.com/vi/cs-XZ_dN4Hc/0.jpg"
            } };

            _author = new AuthorDto
            {
                Name = "Pearl Jam",
                ImgUrl = "https://i.ytimg.com/vi/cs-XZ_dN4Hc/0.jpg"
            };

            #endregion

            #region Category definition

            _categoriesAvailable = new List<Category> { new Category
            {
                Id = 1,
                Name = "Rock",
                ImgUrl = "https://i.ytimg.com/vi/cs-XZ_dN4Hc/0.jpg"
            } };

            _category = new CategoryDto
            {
                Name = "Rock",
                ImgUrl = "https://images.vexels.com/media/users/3/145816/isolated/preview/7616b64374d1ecc318e9d638807c4d61-logotipo-de-sinal-de-m-sica-rock-by-vexels.png"
            };

            #endregion

            #region Sounds Definition

            _soundsAvailable = new List<Sound> { new Sound
            {
                Id = 1,
                Name = "Black",
                IdCategory = 1,
                ImgUrl = "https://i.ytimg.com/vi/cs-XZ_dN4Hc/0.jpg",
                HeardCount = 1000
            } };

            _sound = new SoundDto
            {
                Name = "Black",
                HeardCount = 0,
                ImgUrl = "https://i.ytimg.com/vi/4q9UafsiQ6k/hqdefault.jpg"
            };

            #endregion

            #region Author Repository and Service

            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _authorRepositoryMock.Setup(x => x.GetAuthorByName(_author.Name))
                .Returns(_authorsAvailable);

            _authorService = new AuthorService(
                _authorRepositoryMock.Object);

            #endregion

            #region Category Repository and Service

            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categoryRepositoryMock.Setup(x => x.GetCategoryByName(_category.Name))
                .Returns(_categoriesAvailable);

            _categoryService = new CategoryService(
                _categoryRepositoryMock.Object);

            #endregion

            #region Sound Repository and Service

            _soundRepositoryMock = new Mock<ISoundRepository>();
            _soundRepositoryMock.Setup(x => x.GetSoundByName(_sound.Name))
                .Returns(_soundsAvailable);

            _soundService = new SoundService(
                _soundRepositoryMock.Object);

            #endregion
        }

        #region Author Tests

        [Fact]
        public void ShouldNotSaveAuthorIfExistsAnAuthorWithSameName()
        {
            _authorService.CreateAuthor(_author);

            _authorRepositoryMock.Verify(x => x.Save(It.IsAny<AuthorDto>()), Times.Never);
        }

        [Fact]
        public void ShouldSaveAuthor()
        {
            _authorsAvailable.Clear();

            AuthorDto savedAuthor = null;

            _authorRepositoryMock.Setup(x => x.Save(It.IsAny<AuthorDto>()))
                .Callback<AuthorDto>(authorDto =>
                {
                    savedAuthor = authorDto;
                });

            _authorService.CreateAuthor(_author);

            _authorRepositoryMock.Verify(x => x.Save(It.IsAny<AuthorDto>()), Times.Once);

            Assert.NotNull(savedAuthor);
            Assert.Equal(_author, savedAuthor);
        }

        [Theory]
        [InlineData(StatusCodeEnum.Success, false)]
        [InlineData(StatusCodeEnum.ErrorSameNameAuthors, true)]
        public void ShouldReturnExpectedStatusCodeIfExistsAnAuthorWithSameName(StatusCodeEnum expectedStatusCode, bool existsAuthorWithSameName)
        {
            if (!existsAuthorWithSameName)
                _authorsAvailable.Clear();

            var response = _authorService.CreateAuthor(_author);

            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Theory]
        [InlineData(3, false)]
        [InlineData(null, true)]
        public void ShouldReturnExpectedAuthorId(long? expectedId, bool existsAuthorWithSameName)
        {
            if (!existsAuthorWithSameName)
            {
                _authorsAvailable.Clear();
                _authorRepositoryMock.Setup(x => x.Save(It.IsAny<AuthorDto>()))
                    .Returns(expectedId.GetValueOrDefault());
            }

            var response = _authorService.CreateAuthor(_author);

            Assert.Equal(expectedId, response.Id);
        }

        #endregion

        #region Category Tests

        [Fact]
        public void ShouldNotSaveCategoryIfExistsAnCategoryWithSameName()
        {
            _categoryService.CreateCategory(_category);

            _categoryRepositoryMock.Verify(x => x.Save(It.IsAny<CategoryDto>()), Times.Never);
        }

        [Fact]
        public void ShouldSaveCategory()
        {
            _categoriesAvailable.Clear();

            CategoryDto savedCtegory = null;

            _categoryRepositoryMock.Setup(x => x.Save(It.IsAny<CategoryDto>()))
                .Callback<CategoryDto>(categoryDto =>
                {
                    savedCtegory = categoryDto;
                });

            _categoryService.CreateCategory(_category);

            _categoryRepositoryMock.Verify(x => x.Save(It.IsAny<CategoryDto>()), Times.Once);

            Assert.NotNull(savedCtegory);
            Assert.Equal(_category, savedCtegory);
        }

        [Theory]
        [InlineData(StatusCodeEnum.Success, false)]
        [InlineData(StatusCodeEnum.ErrorSameNameCategories, true)]
        public void ShouldReturnExpectedStatusCodeIfExistsAnCategoryWithSameName(StatusCodeEnum expectedStatusCode, bool existsCategoryWithSameName)
        {
            if (!existsCategoryWithSameName)
                _categoriesAvailable.Clear();

            var response = _categoryService.CreateCategory(_category);

            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Theory]
        [InlineData(2, false)]
        [InlineData(null, true)]
        public void ShouldReturnExpectedCategoryId(long? expectedId, bool existsCategoryWithSameName)
        {
            if (!existsCategoryWithSameName)
            {
                _categoriesAvailable.Clear();
                _categoryRepositoryMock.Setup(x => x.Save(It.IsAny<CategoryDto>()))
                    .Returns(expectedId.GetValueOrDefault());
            }

            var response = _categoryService.CreateCategory(_category);

            Assert.Equal(expectedId, response.Id);
        }

        #endregion

        #region Sounds Tests

        [Fact]
        public void ShouldNotSaveSoundIfExistsAnSoundWithSameName()
        {
            _soundService.CreateSound(_sound);

            _categoryRepositoryMock.Verify(x => x.Save(It.IsAny<CategoryDto>()), Times.Never);
        }

        [Fact]
        public void ShouldSaveSound()
        {
            _soundsAvailable.Clear();

            SoundDto savedSound = null;

            _soundRepositoryMock.Setup(x => x.Save(It.IsAny<SoundDto>()))
                .Callback<SoundDto>(soundDto =>
                {
                    savedSound = soundDto;
                });

            _soundService.CreateSound(_sound);

            Assert.NotNull(savedSound);
            Assert.Equal(_sound, savedSound);
        }


        [Theory]
        [InlineData(StatusCodeEnum.Success, false)]
        [InlineData(StatusCodeEnum.ErrorSameNameSounds, true)]
        public void ShouldReturnExpectedStatusCodeIfExistsAnSoundWithSameName(StatusCodeEnum expectedStatusCode, bool existsSoundWithSameName)
        {
            if (!existsSoundWithSameName)
                _soundsAvailable.Clear();

            var response = _soundService.CreateSound(_sound);

            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Theory]
        [InlineData(5, false)]
        [InlineData(null, true)]
        public void ShouldReturnExpectedSoundId(long? expectedId, bool existsSoundWithSameName)
        {
            if (!existsSoundWithSameName)
            {
                _soundsAvailable.Clear();
                _soundRepositoryMock.Setup(x => x.Save(It.IsAny<SoundDto>()))
                    .Returns(expectedId.GetValueOrDefault());
                // expectedId ?? default -> iguais <- expectedId.GetValueOrDefault()
            }

            var response = _soundService.CreateSound(_sound);

            Assert.Equal(expectedId, response.Id);
        }

        #endregion
    }
}