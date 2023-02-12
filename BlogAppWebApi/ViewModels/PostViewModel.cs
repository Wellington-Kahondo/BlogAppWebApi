using AutoMapper;
using BlogAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.ViewModels
{
    [AutoMap(typeof(Post))]
    public class PostViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedTimeStamp { get; set; }
        public Guid UserId { get; set; }
        public List<Guid> TagIds { get; set; }
        public string Author { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Comments { get; set; }
           
        
    }
}
