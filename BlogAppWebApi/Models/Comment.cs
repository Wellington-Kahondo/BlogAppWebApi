using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid? PostId { get; set; }
        public Post Post { get; set; }

        public Guid? UserId { get; set; }
        //[ForeignKey("AuthorId")]
        public User User { get; set; }

    }

}
