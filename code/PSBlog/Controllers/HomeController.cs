
using Ninject.Extensions.Logging;
using PSBlog.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _log;

        public HomeController(ILogger log)
        {
            _log = log;
        }

        //
        // GET: /Home/
        public ActionResult Index()
        {
            _log.Info("NLog Test");      
            return View();
        }
	}
}