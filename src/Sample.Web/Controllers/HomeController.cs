using Sample.Domain.Persistence;
using SEOP.Framework.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class HomeController : Controller
    {
        SampleContext _sampleContext;

        public HomeController(SampleContext dbctx)
        {
            var db1 = DependencyResolver.Current.GetService<SampleContext>();
            _sampleContext = dbctx;
        }

        // GET: Home
        public ActionResult Index()
        {

            return View();
        }
    }
}