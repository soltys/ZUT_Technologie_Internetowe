using PSBlog.Common;
using PSBlog.Models;

namespace PSBlog.Repository
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository 
    {
        public PostRepository(PSBlogContext context):base(context)
        {
                
        }
    }
}
