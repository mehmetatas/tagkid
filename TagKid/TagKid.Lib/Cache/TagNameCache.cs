using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Cache
{
    public class TagNameCache
    {
        private static readonly CultureInfo EnGb = new CultureInfo("en-GB");
        public static TagNameCache Instance = new TagNameCache();

        private readonly TagSearchNode _cache;

        private TagNameCache()
        {
            IEnumerable<Tag> tags;
            using (Db.UnitOfWork())
            {
                tags = Db.TagRepository().GetAll().Where(t => t.Status == TagStatus.Active);
            }

            _cache = new TagSearchNode('\0');

            foreach (var tag in tags)
            {
                _cache.AddTag(tag);
            }
        }

        public IEnumerable<Tag> Search(string filter, int max = 20)
        {
            return _cache.Search(filter.ToLower(EnGb), max);
        }
    }

    public class TagSearchNode
    {
        public TagSearchNode(char initial)
        {
            Initial = initial;
            Nodes = new Hashtable();
            Tags = new HashSet<Tag>();
        }

        public char Initial { get; private set; }
        public Hashtable Nodes { get; private set; }
        public HashSet<Tag> Tags { get; private set; }

        public void AddTag(Tag tag)
        {
            var node = this;

            foreach (var c in tag.Name)
            {
                if (node.Nodes.ContainsKey(c))
                {
                    node = (TagSearchNode)node.Nodes[c];
                }
                else
                {
                    var tmp = new TagSearchNode(c);
                    node.Nodes.Add(c, tmp);
                    node = tmp;
                }
            }

            node.Tags.Add(tag);
        }

        public IEnumerable<Tag> Search(string filter, int max = 20)
        {
            var node = this;

            foreach (var c in filter)
            {
                if (node.Nodes.ContainsKey(c))
                {
                    node = (TagSearchNode)node.Nodes[c];
                }
                else
                {
                    return new List<Tag>();
                }
            }

            var tags = new List<Tag>();
            
            node.GetTags(tags, max);

            return tags.Take(max);
        }

        private void GetTags(List<Tag> tags, int max)
        {
            tags.AddRange(Tags);

            foreach (TagSearchNode node in Nodes.Values)
            {
                if (tags.Count >= max)
                    return;
                node.GetTags(tags, max);
            }
        }

        public override string ToString()
        {
            return String.Format("{0} : {1}", Initial, Tags.Count);
        }
    }
}
