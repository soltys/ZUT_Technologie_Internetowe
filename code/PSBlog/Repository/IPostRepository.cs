using PSBlog.Models;
using PSBlog.ViewModels;
using System.Collections.Generic;

namespace PSBlog.Repository
{
    public interface IPostRepository : PSBlog.Repository.IRepository<Post>
    {
        List<Post> GetAllPosts(int blogId);
        PostDetailsModel GetPostDetails(string blogSlug, string postSlug);
        void AddComment(string commentContent, string commentUserName, string blogSlug, string postSlug);
    }
}
