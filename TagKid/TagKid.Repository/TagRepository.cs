using System;
using System.Collections.Generic;
using System.Linq;
using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Repository;

namespace TagKid.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly IRepository _repository;

        public TagRepository(IRepository repository)
        {
            _repository = repository;
        }

        public Tag[] GetAll()
        {
            return _repository.Select<Tag>()
                .OrderBy(t => t.Name)
                .ToArray();
        }

        public IPage<Tag> Search(string name, int pageIndex, int pageSize)
        {
            return _repository.Select<Tag>()
                .Where(t => t.Name.Contains(name))
                .OrderByDescending(t => t.Count)
                .Page(pageIndex, pageSize);
        }

        public IDictionary<Tag, int> GetUserTagCounts(long userId)
        {
            throw new NotImplementedException();
        }

        public void Save(Tag tag)
        {
            _repository.Save(tag);
        }
    }
}