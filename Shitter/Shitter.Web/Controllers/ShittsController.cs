namespace Shitter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Shitter.Data.UnitOfWork;
    using Shitter.Models;
    using Shitter.Web.Models.Shitts;

    public class ShittsController : BaseController
    {
        public ShittsController(IShiterData data)
            :base(data)
        {
        }

        // POST: /Shitts/PostShitt
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostShitt(PostShitViewModel model)
        {
            if (ModelState.IsValid)
            {
                var shitt = new Shitt
                {
                    Content = model.Content,
                    OwnerId = this.UserProfile.Id,
                    CreatedOn = DateTime.Now,
                };

                // upload image if existing
                if (model.ImageDataUrl != null && model.ImageDataUrl.ContentLength > 0)
                {
                    var file = Request.Files[0];
                    string pathToSave = Server.MapPath("~/Content/Images/Shitts/");
                    string filename = Path.GetFileName(file.FileName);

                    //ad current datetime to filename for uniqueness and save to file system
                    string curentDateTimeString = DateTime.Now.ToString("hh-mm-ss-yyyy-mm-d");
                    filename = curentDateTimeString + filename;
                    file.SaveAs(Path.Combine(pathToSave, filename));

                    // ad photo path in database
                    shitt.ImageDataUrl = "/Content/Images/Shitts/" + filename;
                }

                try
                {
                    this.UserProfile.PostedShitts.Add(shitt);
                    this.Data.Shitts.Add(shitt);
                    this.Data.SaveChanges();
                }
                catch (Exception ex)
                {
                    return this.RedirectToAction("Error", "Home");
                }
            }

            return this.RedirectToAction("Dashboard", "Home");
        }
    }
}