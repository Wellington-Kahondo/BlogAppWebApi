using AutoMapper;
using BlogAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.ViewModels
{
    [AutoMap(typeof(PostTag))]
    public class PostTagViewModel
    {
        public Guid PostId { get; set; }
        public Guid TagId { get; set; }
    }
}
