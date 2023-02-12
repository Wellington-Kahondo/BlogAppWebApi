using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedTimeStamp { get; set; }

        //Navigation Properties
        public Guid UserId { get; set; }
        public User User { get; set; }
        public virtual List<PostTag> PostTags { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
