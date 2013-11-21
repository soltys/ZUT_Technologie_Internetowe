using PSBlog.Common;
using PSBlog.Models;

namespace PSBlog.Repository
{
    internal class PostRepository : UrlSlugRepository<Post>, IPostRepository 
    {
        public PostRepository(PSBlogContext context):base(context)
        {
                
        }
    }
}
