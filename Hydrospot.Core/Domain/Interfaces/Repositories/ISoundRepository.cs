using Hydrospot.Core.Domain.Dtos;
using Hydrospot.Core.Domain.Entity;
using System.Collections.Generic;

namespace Hydrospot.Core.Interfaces.Repositories
{
    public interface ISoundRepository
    {
        long Save(SoundDto sound);
        IEnumerable<SoundDto> GetSoundsByName(string name);
        SoundDto? FindSoundByName(string name);
    }
}
