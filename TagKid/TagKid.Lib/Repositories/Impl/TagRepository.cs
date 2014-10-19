using System;
using System.Collections.Generic;
using System.Linq;
using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Repositories.Impl
{
    public class TagRepository : ITagRepository
    {
        private readonly IRepository _repository;

        public TagRepository(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Tag> GetAll()
        {
            return _repository.Query<Tag>()
                .OrderBy(t => t.Name)
                .ToList();
        }

        public IPage<Tag> Search(string name, int pageIndex, int pageSize)
        {
            return _repository.Query<Tag>()
                .Where(t => t.Name.Contains(name))
                .OrderByDescending(t => t.Count)
                .Page(pageSize, pageIndex);
        }

        public IEnumerable<Tag> GetPostTags(long postId)
        {
            throw new NotImplementedException();
        }

        public IDictionary<Tag, int> GetUserTagCounts(long userId)
        {
            throw new NotImplementedException();
        }

        public IDictionary<Tag, int> GetCategoryTagCounts(long userId)
        {
            throw new NotImplementedException();
        }

        public void Save(Tag tag)
        {
            _repository.Save(tag);
        }
    }
}