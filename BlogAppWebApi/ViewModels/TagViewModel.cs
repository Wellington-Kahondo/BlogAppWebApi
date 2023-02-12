using AutoMapper;
using BlogAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.ViewModels
{
    [AutoMap(typeof(Tag))]
    public class TagViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
