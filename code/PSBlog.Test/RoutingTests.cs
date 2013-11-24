using Messum.UI.Test.Helpers;
using NUnit.Framework;

namespace PSBlog.Test
{
    [TestFixture]
    class RoutingTests
    {
        //Slash
        [Test]
        public void When_Url_Slash_Home_Index()
        {
            const string url = "~/";
            RouteHelpers.TestRoute(url, new
            {
                Controller = "Home",
                Action = "Index",
            });
        }
        //User
        [Test]
        public void slash_user_user_index()
        {
            const string url = "~/User";
            RouteHelpers.TestRoute(url, new
            {
                Controller = "User",
                Action = "Index",
            });
        }

        [Test]
        public void slash_user_register_user_register()
        {
            const string url = "~/User/Register";
            RouteHelpers.TestRoute(url, new
            {
                Controller = "User",
                Action = "Register",
            });
        }

        [Test]
        public void slash_user_login_user_login()
        {
            const string url = "~/User/Login";
            RouteHelpers.TestRoute(url, new
            {
                Controller = "User",
                Action = "Login",
            });
        }

        [Test]
        public void slash_b_empty_to_blog_details()
        {
            const string url = "~/b/";
            RouteHelpers.TestRoute(url, new
            {
                Controller = "Blog",
                Action = "Details",
                BlogSlug = "",
            });
        }

        [Test]
        public void slash_b_empty_without_ending_slash_to_blog_details()
        {
            const string url = "~/b";
            RouteHelpers.TestRoute(url, new
            {
                Controller = "Blog",
                Action = "Details",
                BlogSlug = "",
            });
        }

        [Test]
        public void slash_b_blogSlug_to_blog_details()
        {
            const string url = "~/b/Soltys_AweSomeBlog";
            RouteHelpers.TestRoute(url, new
            {
                Controller = "Blog",
                Action = "Details",
                BlogSlug = "Soltys_AweSomeBlog",
            });
        }

        [Test]
        public void slash_b_blogSlug_postSlug_to_post_details()
        {
            const string url = "~/b/Soltys_AweSomeBlog/Soltys_M_E_G_A_post";
            RouteHelpers.TestRoute(url, new
            {
                Controller = "Post",
                Action = "Details",
                BlogSlug = "Soltys_AweSomeBlog",
                PostSlug = "Soltys_M_E_G_A_post"
            });
        }
       
    }
}
