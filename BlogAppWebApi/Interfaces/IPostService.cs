using BlogAppWebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.Interfaces
{
    public interface IPostService
    {
        Task<PostViewModel> CreatePost(PostViewModel input);

        Task<PostViewModel> GetPostById(Guid id);

        Task<List<PostViewModel>> GetAllPosts();

        Task<PostViewModel> UpdatePost(Guid id, PostViewModel input);

        void DeletePost(Guid id);
    }
}
