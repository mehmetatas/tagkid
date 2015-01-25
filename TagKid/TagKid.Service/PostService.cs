using Taga.Core.DynamicProxy;
using TagKid.Core.Domain;
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
            var posts = _postDomain.GetTimeline(request.MaxPostId);
            return Response.Success.WithData(posts);
        }

        public virtual Response SaveAsDraft(SaveAsDraftRequest request)
        {
            _postDomain.SaveAsDraft(request.Post);
            return Response.Success.WithData(request.Post);
        }

        public virtual Response Publish(PublishRequest request)
        {
            _postDomain.Publish(request.Post);
            return Response.Success.WithData(request.Post);
        }

        public virtual Response CreateCategory(CreateCategoryRequest request)
        {
            var category = request.Category;
            _postDomain.CreateCategory(category);
            return Response.Success.WithData(category.Id);
        }

        public virtual Response GetCategories(GetCategoriesRequest request)
        {
            var categories = _postDomain.GetCategoriesOfUser(request.UserId);
            return Response.Success.WithData(categories);
        }

        public virtual Response GetComments(GetCommentsRequest request)
        {
            var comments = _postDomain.GetComments(request.PostId, request.MaxCommentId);
            return Response.Success.WithData(comments);
        }

        public virtual Response LikeUnlike(LikeUnlikeRequest request)
        {
            var likeResult = _postDomain.LikeUnlike(request.PostId);
            return Response.Success.WithData(likeResult);
        }

        public Response GetPosts(GetPostsRequest request)
        {
            var res = _postDomain.GetPostsOfUser(request.UserId, request.CategoryId, request.MaxPostId);
            return Response.Success.WithData(res);
        }
    }
}