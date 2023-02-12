using BlogAppWebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.Interfaces
{
    public interface ITagService
    {
        Task<ActionResult<TagViewModel>> CreateTag(TagViewModel input);

        Task<TagViewModel> GetTagById(Guid id);

        Task<List<TagViewModel>> GetAllTags();

        Task<TagViewModel> UpdateTag(Guid id, TagViewModel input);

        void DeleteTag(Guid id);
    }
}
