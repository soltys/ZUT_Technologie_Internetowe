using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSBlog.Util.Test
{
    [TestFixture]
    class SlugTest
    {


        [Test]
        public void string_to_slug_replacing_spaces_with_underscores()
        {
            string input = "john doe";
            
            string actual = Slug.GenerateSlug(input);
            string expected = "john_doe";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void string_to_slug_remain_CapitalLetters()
        {
            string input = "JohnDoe";
            string actual = Slug.GenerateSlug(input);
            string expected = "JohnDoe";
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void string_to_slug_remove_polish_letters()
        {
            string input = "PawełSołtysiak";
            string actual = Slug.GenerateSlug(input);
            string expected = "PaweSotysiak";
            Assert.AreEqual(expected, actual);
        }
    }
}
