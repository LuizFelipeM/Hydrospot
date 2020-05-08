using System;
using System.Collections.Generic;
using System.Text;

namespace Hydrospot.Core.Domain.Entity
{
    public class Sound
    {
        public long Id { get; set; }
        public long IdCategory { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public long HeardCount { get; set; }
    }
}
