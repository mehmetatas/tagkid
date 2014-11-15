using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TagKid.Core.Database;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Filter;
using TagKid.Core.Repositories;
using TagKid.Core.Utils;

namespace TagKid.Tests.RepositoryTests
{
    [TestClass]
    public class PostRepositoryTests : BaseRepositoryTests
    {
        private readonly string[] _titleWords =
            @"Metro has a whole lot of really great data about bike and pedestrian counts in Portland and lots of info about how people use the trails. All of it is in spreadsheets right now and they want to find a way to visualize the data and make better use of the data. The team brainstormed ways to use this data and came up with some great ideas that can really help people and show useful information. For example: we can show trails that are ADA accessible and where they are, trails that have parking nearby, protected crossings, safe bike commuter routes, or are particularly scenic. This is a map application that asks questions to determine what trail you are looking for. You start with broader categories like biking or walking and narrow your focus from there based on what matters most to you. Right now, we are working on importing PLATS compatible geometric trail data and assigning it the values from the spreadsheets and displaying it on the map in a heat-map style. So the trails that best fit your criteria will really stand out on the map for you. Eventually, you will also be able to tap on a trail and pull up more information about the trail."
                .Split(' ');

        private readonly Random _rnd = new Random();

        private string GetRandomTitle()
        {
            var titleLen = _rnd.Next(1, 10);

            var title = "";

            for (var j = 0; j < titleLen; j++)
            {
                title += _titleWords[_rnd.Next(_titleWords.Length)] + " ";
            }

            title = title.Trim();

            if (title.Length > 100)
            {
                title = title.Substring(0, 100);
            }

            return title;
        }

        [TestMethod, TestCategory("RepositoryTests")]
        public void Should_Search_Posts()
        {
            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IPostRepository>();
                var catRepo = db.GetRepository<ICategoryRepository>();
                var userRepo = db.GetRepository<IUserRepository>();

                for (var i = 0; i < 4; i++)
                {
                    catRepo.Save(new Category
                    {
                        AccessLevel = (AccessLevel)(i % 3),
                        UserId = i + 1,
                        Status = CategoryStatus.Active,
                        Name = "CatName" + i,
                        Description = "CatDesc" + i
                    });

                    userRepo.Save(new User
                    {
                        Username = "Username",
                        JoinDate = DateTime.Now.TrimMillis(),
                        Status = i % 5 == 0 ? UserStatus.Passive : UserStatus.Active
                    });
                }

                for (var i = 0; i < 20; i++)
                {
                    repo.Save(new Post
                    {
                        AccessLevel = (AccessLevel) (i % 3),
                        CategoryId = (i % 4) + 1,
                        UserId =  (i % 4) + 1,
                        Content = "Content",
                        ContentCode = "Content Code",
                        CreateDate = DateTime.Now.AddDays(-2).TrimMillis(),
                        LinkDescription = "Link Description",
                        LinkImageUrl = "Link Image Url",
                        LinkTitle = "Link Title",
                        LinkUrl = "Link Url",
                        MediaEmbedUrl = "Media Embed Url",
                        PublishDate = DateTime.Now.AddDays(-1).TrimMillis(),
                        QuoteAuthor = "Quote Author",
                        QuoteText = "Quote Text",
                        Status =
                            i % 5 != 1 ? PostStatus.Published : (i % 2 == 0 ? PostStatus.Draft : PostStatus.Unpublished),
                        Tags =
                            new List<Tag>
                            {
                                new Tag { Id = i + 1 },
                                new Tag { Id = i + 2 },
                                new Tag { Id = i + 3 },
                                new Tag { Id = i + 4 },
                                new Tag { Id = i + 5 }
                            },
                        Title = GetRandomTitle(),
                        UpdateDate = DateTime.Now.TrimMillis(),
                        Type = (PostType)(i % 7)
                    });
                }

                db.Save();
            }

            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<IPostRepository>();

                repo.Search(new PostFilter
                {
                    TagIds = new long[] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
                    Title = "a",
                    PageIndex = 1,
                    PageSize = 10
                });
            }
        }
    }
}