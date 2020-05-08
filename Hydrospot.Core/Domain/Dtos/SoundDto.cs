using System.Collections.Generic;

namespace Hydrospot.Core.Domain.Dtos
{
    public class SoundDto : BaseMusicDto
    {
        public long HeardCount { get; set; }
        public IEnumerable<string> AuthorsNames { get; set; }
    }
}
