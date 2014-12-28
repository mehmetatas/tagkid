using Taga.Core.DynamicProxy;
using TagKid.Core.Domain;
using TagKid.Core.Models;
using TagKid.Core.Models.DTO.Messages;
using TagKid.Core.Models.DTO.Messages.Post;
using TagKid.Core.Service;

namespace TagKid.Service
{
    [Intercept]
    public class PostService : IPostService
    {
        private readonly IPostDomainService _postDomain;

        public PostService(IPostDomainService postDomain)
        {
            _postDomain = postDomain;
        }

        public virtual Response GetTimeline(GetTimelineRequest request)
        {
            var posts = _postDomain.GetTimeline(10);
            return Response.Success.WithData(posts);
        }

        public virtual Response SavePost(SavePostRequest request)
        {
            _postDomain.SaveAsDraft(request.Post);
            return Response.Success.WithData(request.Post);
        }

        public virtual Response CreateCategory(CreateCategoryRequest request)
        {
            var category = request.Category;
            _postDomain.CreateCategory(category);
            return Response.Success.WithData(category.Id);
        }

        public virtual Response GetCategories()
        {
            var categories = _postDomain.GetCategoriesOfUser(RequestContext.User.Id);
            return Response.Success.WithData(categories);
        }
    }
}