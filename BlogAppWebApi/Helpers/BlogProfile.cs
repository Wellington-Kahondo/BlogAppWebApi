using AutoMapper;
using BlogAppWebApi.Models;
using BlogAppWebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.Helpers
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Post, PostViewModel>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => MapUserFirstNameFromPost(src)))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.PostTags.Select(pt => pt.Tag.Name)))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Select(ct => ct.Content)));
            CreateMap<PostViewModel, Post>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.PostTags, opt => opt.Ignore());

            CreateMap<Comment, CommentViewModel>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User != null  ? src.User.FirstName : ""));
                //.ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.User != null ? src.User.FirstName : ""));
            CreateMap<CommentViewModel, Comment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Tag, TagViewModel>();
            CreateMap<TagViewModel, Tag>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<PostTag, PostTagViewModel>();
            CreateMap<PostTagViewModel, PostTag>();
        }

        private static string MapUserFirstNameFromPost(Post src)
        {
            return src.User?.FirstName ?? null;
        }

        private static string MapUserFirstNameFromComment(Comment src)
        {
            return src.User?.FirstName ?? "Helloe wolrf";
        }
    }
}




