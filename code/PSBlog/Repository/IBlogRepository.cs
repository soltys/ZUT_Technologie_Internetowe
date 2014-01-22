using PSBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSBlog.Repository
{
    public interface IBlogRepository:IRepository<Blog>
    {
        Blog GetBlogBySlugUrl(string slugUrl);
        void AddPost(int blogId, Post post);
        void AddToUser(Blog blog, User user);
    }
}
