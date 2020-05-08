using Hydrospot.Core.Interfaces.Repositories;
using Hydrospot.Core.Domain;
using Hydrospot.Core.Domain.Dtos;
using Hydrospot.Core.Utils;
using System;
using System.Linq;

namespace Hydrospot.Core.Services
{
    public class SoundService
    {
        private readonly ISoundRepository _soundRepository;

        public SoundService(ISoundRepository soundRepository)
        {
            _soundRepository = soundRepository;
        }

        public ResponseDto CreateSound(SoundDto sound)
        {
            if (sound == null)
                throw new ArgumentException(nameof(sound));

            ResponseDto response;

            if (_soundRepository.FindSoundByName(sound.Name) == null)
            {
                long id = _soundRepository.Save(sound);

                response = ResponseUtils.CreateResponseDto(StatusCodeEnum.Success, id);
            }
            else
                response = ResponseUtils.CreateResponseDto(StatusCodeEnum.ErrorSameNameSounds);

            return response;
        }
    }
}
