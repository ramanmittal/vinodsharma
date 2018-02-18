using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vinodsharma.Models;
using vinodsharma.Utils;

namespace vinodsharma.Controllers
{
    [Authorize(Roles = Utils.Roles.Admin)]
    public class AdminController : Controller
    {
        private Service service;
        public AdminController(Service service)
        {
            this.service = service;
        }
        // GET: Admin
        public ActionResult Index()
        {
            var model = service.GetUserList();
            return View(model);
        }

        public ActionResult CreateMember() {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMember(CreateMemberViewModel model) {
            if (ModelState.IsValid)
            {
                try
                {
                    service.VerifyInitializer(model.InlinerID);
                    var member=service.CreateMember(model);
                    service.CollectMember(member);
                    
                    return RedirectToAction("Index");
                }
                catch (CustomException ex)
                {
                    ModelState.AddModelError("InlinerID", ex.Message);
                }
                
            }
            return View();
        }

        public ActionResult EditMember(int memberID) {
            var model = service.GetEditUser(memberID);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditMember(EditUserModel model) {

            if (ModelState.IsValid)
            {
                service.EditUser(model);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult GiveMoney(int id) {
            AddMoneyViewModel model = service.GetGiveMoneyModel(id);
            //model.Date = DateTime.UtcNow.Date;
            return View(model);
        }
        [HttpPost]
        public ActionResult GiveMoney(AddMoneyModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    service.VerifyAmount(model);
                    service.AddMoney(model);
                    return RedirectToAction("EditMember", new { memberID = model.MemberID });
                }
                catch (CustomException ex)
                {
                    ViewData.Model= service.GetGiveMoneyModel(model.MemberID);
                    ModelState.AddModelError("Amount", ex.Message);
                }
                
            }
            return View();
        }
        public ActionResult Members(int memberID)
        {
            List<UserlistviewModel> list = service.GetChildren(memberID);
            return View(list);
        }

        public ActionResult PaymnetHistory(int memberID) {
            var data = service.PaymentHistory(memberID);
            return View(data);
        }

        public ActionResult UpdateProfile(int memberID)
        {            
            var model = service.GetProfileModel(memberID);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateProfile(UrerProfileModel model) {
            if (ModelState.IsValid)
            {
                service.UpdateProfile(model);
                return RedirectToAction("EditMember", new { memberID = model.MemberID });
            }
            return View();
        }
    }
}