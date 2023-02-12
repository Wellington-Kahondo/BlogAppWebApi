using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAppUnitTest
{
    public class PostViewModel
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedTimeStamp { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<string>? Tags { get; set; }
        public IEnumerable<Guid>? TagIds { get; set; }
        public string? Author { get; set; }
    }
}
