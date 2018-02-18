using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using vinodsharma.Models;
using vinodsharma.Utils;

namespace vinodsharma.Controllers
{
    public class HomeController : Controller
    {
        private Service service;
        public HomeController(Service service)
        {
            this.service = service;
        }
        [Route("InitializeApplication")]
        public async Task<ActionResult> InitialCreate()
        {
            await service.Initialize();
            return RedirectToAction("index");
        }
        public ActionResult Index()
        {
            if (Request.IsAuthenticated && User.IsInRole(Roles.Admin))
            {
                return RedirectToAction("Index", "Admin");
            }
            if (Request.IsAuthenticated && User.IsInRole(Roles.Customer))
            {
                return RedirectToAction("ViewProfile");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

        

        [Authorize(Roles = Roles.Customer)]
        public ActionResult ViewProfile()
        {
            var id = User.Identity.GetUserId();
            var model = service.GetProfileModel(id);
            ViewBag.Points = service.GetPoints(id);
            return View(model);
        }
        [Authorize(Roles = Roles.Customer)]
        public ActionResult PaymentHistory()
        {
            var id = User.Identity.GetUserId();
            var model = service.PaymentHistory(id);
            return View( model);
        }
    }
}