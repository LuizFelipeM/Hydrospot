using System;
using System.Collections.Generic;
using System.Text;

namespace Hydrospot.Core.Domain.Entity
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
    }
}
