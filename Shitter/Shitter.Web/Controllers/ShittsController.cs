namespace Shitter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class ShittsController : Controller
    {
        // GET: Shitts
        public ActionResult Index()
        {
            return View();
        }
    }
}