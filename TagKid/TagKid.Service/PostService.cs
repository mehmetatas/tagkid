using Taga.Core.DynamicProxy;
using TagKid.Core.Domain;
using TagKid.Core.Models;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Database.View;
using TagKid.Core.Models.Domain;
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

        public virtual Response GetAnonymousTimeline()
        {
            var posts = _postDomain.GetAnonymousTimeline();
            return Response.Success.WithData(posts);
        }

        public virtual Response Save(SaveRequest request)
        {
            var postDO = _postDomain.Save(request.Post);
            return Response.Success.WithData(postDO);
        }

        public virtual Response GetComments(GetCommentsRequest request)
        {
            var comments = _postDomain.GetComments(request.PostId, request.MaxCommentId);
            return Response.Success.WithData(comments);
        }

        public virtual Response Comment(CommentRequest request)
        {
            var comment = _postDomain.Comment(request.PostId, request.Comment);
            return Response.Success.WithData(comment);
        }

        public virtual Response LikeUnlike(LikeUnlikeRequest request)
        {
            var likeResult = _postDomain.LikeUnlike(request.PostId);
            return Response.Success.WithData(likeResult);
        }

        public Response GetPosts(GetPostsRequest request)
        {
            var res = _postDomain.GetPostsOfUser(request.UserId, request.MaxPostId);
            return Response.Success.WithData(res);
        }

        public Response SearchTags(SearchTagsRequest request)
        {
            var res = _postDomain.SearchTags(request.TagName);
            return Response.Success.WithData(res);
        }
    }
}