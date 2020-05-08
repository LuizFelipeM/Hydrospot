using Hydrospot.Core.Domain.Dtos;
using Hydrospot.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace Hydrospot.Core.Services
{
    public class SoundSearchService
    {
        private readonly ISoundRepository _soundRepository;

        public SoundSearchService(ISoundRepository soundRepository)
        {
            _soundRepository = soundRepository;
        }

        public IEnumerable<SoundDto> FindSoundsByName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var response = _soundRepository.GetSoundsByName(name);

            return response;
        }
    }
}