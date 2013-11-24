using PSBlog.Models;
using System.Collections.Generic;

namespace PSBlog.Repository
{
    public interface IPostRepository : PSBlog.Repository.IRepository<Post>
    {
        List<Post> GetAllPosts(int blogId);
    }
}
