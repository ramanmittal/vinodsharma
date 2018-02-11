﻿using System;
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

    }
}