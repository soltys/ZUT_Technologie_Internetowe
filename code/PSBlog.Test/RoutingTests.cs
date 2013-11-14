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

       
    }
}
