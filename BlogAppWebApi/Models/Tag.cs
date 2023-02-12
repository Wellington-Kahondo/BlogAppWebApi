using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual List<PostTag> PostTags { get; set; }
    }
}
