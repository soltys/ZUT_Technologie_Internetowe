using PSBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSBlog
{
    static class ModelBindersConfig
    {

        internal static void RegisterModelBinders(ModelBinderDictionary modelBinderDictionary)
        {
            modelBinderDictionary.Add(typeof(CreateOrEditPostModel), new CreateOrEditPostCustomDataBinder());
        }
    }
}