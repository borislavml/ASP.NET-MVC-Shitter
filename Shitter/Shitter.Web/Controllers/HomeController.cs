namespace Shitter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Expressions;

    using PagedList;

    using Shitter.Models;
    using Shitter.Data.UnitOfWork;

    public class HomeController : BaseController
    {
        public HomeController(IShiterData data)
            :base(data)
        {
        }

        [HttpGet]
        public ActionResult Index(int id = 1)
        {
            int pageSize = 5;
            int shittsFromDb = id - 1;

            List<User> shittsList = this.Data.Users.All()
                //.Skip(shittsFromDb * pageSize)
                //.Take(pageSize)
                .ToList();

            PagedList<User> model = new PagedList<User>(shittsList, id, pageSize);
            return this.View(model);

        }

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page."; 
           return this.View();
        }
        
        [HttpGet]
        public ActionResult NotFound()
        {
            ViewBag.Message = "Sory, this page doesn't exist!";
            return this.View();
        }
    }
}