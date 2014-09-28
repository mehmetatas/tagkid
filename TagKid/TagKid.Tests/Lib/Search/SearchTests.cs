using System.Linq;
using System.Runtime.Remoting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TagKid.Lib.Cache;
using TagKid.Lib.Models.Entities;

namespace TagKid.Tests.Lib.Search
{
    [TestClass]
    public class SearchTests
    {
        [TestMethod]
        public void TestSearch()
        {
            var tags = new[]
            {
                new Tag {Name = "java", Hint = "programming language"},
                new Tag {Name = "javascript", Hint = "scripting language"},
                new Tag {Name = "jquery", Hint = "javascript library"},
                new Tag {Name = "knockoutjs", Hint = "javascript mvvm library"},
                new Tag {Name = "angularjs", Hint = "javascript mvvm library"},
                new Tag {Name = "c-plus-plus", Hint = "programming language"},
                new Tag {Name = "c", Hint = "programming language"},
                new Tag {Name = "c-sharp", Hint = "programming language"},
                new Tag {Name = "c", Hint = "music note"},
                new Tag {Name = "c-sharp", Hint = "music note"},
                new Tag {Name = "java", Hint = "an island"},
                new Tag {Name = "java", Hint = "a coffee type"}
            };

            var node = new TagSearchNode('\0');

            foreach (var tag in tags)
            {
                node.AddTag(tag);
            }

            var res = node.Search("");
            Assert.AreEqual(tags.Length, res.Count());

            res = node.Search("c");
            Assert.AreEqual(5, res.Count());

            res = node.Search("c-");
            Assert.AreEqual(3, res.Count());

            res = node.Search("c-p");
            Assert.AreEqual(1, res.Count());

            res = node.Search("c-s");
            Assert.AreEqual(2, res.Count());

            res = node.Search("c-pu");
            Assert.AreEqual(0, res.Count());

            res = node.Search("c-plus-plus");
            Assert.AreEqual(1, res.Count());

            res = node.Search("c-sharp");
            Assert.AreEqual(2, res.Count());

            res = node.Search("c-sharps");
            Assert.AreEqual(0, res.Count());

            res = node.Search("c-plus-pluss");
            Assert.AreEqual(0, res.Count());

            res = node.Search("j");
            Assert.AreEqual(5, res.Count());

            res = node.Search("jav");
            Assert.AreEqual(4, res.Count());

            res = node.Search("java");
            Assert.AreEqual(4, res.Count());

            res = node.Search("jq");
            Assert.AreEqual(1, res.Count());
        }
    }
}
