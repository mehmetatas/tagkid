﻿using TagKid.Core.Models.DTO.Messages;
using TagKid.Core.Models.DTO.Messages.Post;

namespace TagKid.Core.Service
{
    public interface IPostService
    {
        Response GetTimeline(GetTimelineRequest request);

        Response SaveAsDraft(SaveAsDraftRequest request);

        Response Publish(PublishRequest request);

        Response CreateCategory(CreateCategoryRequest request);

        Response GetCategories();

        Response GetComments(GetCommentsRequest request);

        Response LikeUnlike(LikeUnlikeRequest request);
    }
}