using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSBlog.ViewModels
{
    public class CreatePostCustomDataBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(CreatePostModel))
            {
                HttpRequestBase request = controllerContext.HttpContext.Request;

                int blogId = int.Parse(request.Form.Get("Blog.Id"));
                string title = request.Form.Get("Post.Title");
                
                string content = request.Form.Get("Post.Content");
                
                return new CreatePostModel
                {
                    Blog = new Models.Blog{Id = blogId},
                    Post = new Models.Post{Title = title, Content = content}
                };              
            }
            else
            {
                return base.BindModel(controllerContext, bindingContext);
            }
        }
    }
}