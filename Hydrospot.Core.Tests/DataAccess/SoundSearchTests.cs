using Hydrospot.Core.Domain.Entity;
using Hydrospot.Core.Interfaces.Repositories;
using Hydrospot.Core.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Hydrospot.Core.DataAccess
{
    public class SoundSearchTests
    {
        private readonly List<Sound> _soundsAvailable;
        private readonly SoundSearchService _searcher;
        private readonly Mock<ISoundRepository> _soundRepositoryMock;

        public SoundSearchTests()
        {
            _soundsAvailable = new List<Sound> { new Sound
            {
                Id = 1,
                Name = "Black",
                IdCategory = 1,
                ImgUrl = "https://i.ytimg.com/vi/cs-XZ_dN4Hc/0.jpg",
                HeardCount = 1000
            } };

            _soundRepositoryMock = new Mock<ISoundRepository>();

            _searcher = new SoundSearchService(
                _soundRepositoryMock.Object);
        }

        [Fact]
        public void ShouldReturnListWithSoundsBySoundName()
        {
            var response = _searcher.FindSoundsByName("Black");

            // Assert
            Assert.NotNull(response);
            //Assert.All(searchResult, item => Assert.Contains("Pearl Jam", item.AuthorsNames));
        }

        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => _searcher.FindSoundsByName(null));

            // Assert
            Assert.Equal("request", exception.ParamName);
        }
    }
}
