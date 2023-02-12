using AutoMapper;
using BlogAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.ViewModels
{
    //[AutoMap(typeof(Comment))]
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public Guid PostId { get; set; }
        public string PostTitle { get; set; }
        public Guid UserId { get; set; }

    }
}
