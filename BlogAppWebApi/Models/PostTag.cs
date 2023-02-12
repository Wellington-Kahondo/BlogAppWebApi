using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.Models
{
    public class PostTag
    {
        //[Key]
        //[Column(Order = 1)]
        public Guid PostId { get; set; }
        [ForeignKey("BlogPostId")]
        public virtual Post Post { get; set; }

        //[Key]
        //[Column(Order = 2)]
        public Guid TagId { get; set; }
        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
    }
}
