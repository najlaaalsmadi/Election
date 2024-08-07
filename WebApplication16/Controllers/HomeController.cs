using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication16.Models;

namespace WebApplication16.Controllers
{
    public class HomeController : Controller
    {
        private ElectionsEntities1 db = new ElectionsEntities1();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAdmin(Admin model)
        {
            if (ModelState.IsValid)
            {
                var admin = db.Admins
                    .Where(a => a.email == model.email && a.password == model.password)
                    .SingleOrDefault();

                if (admin != null)
                {
                    // تسجيل الدخول ناجح، يمكنك التوجيه إلى صفحة أخرى أو إنشاء جلسة
                    Session["Admin"] = admin;
                    return RedirectToAction("IndexAdmin", "partyLists"); // أو أي صفحة تريد الانتقال إليها بعد تسجيل الدخول
                }
                else
                {
                    // البيانات غير صحيحة
                    ModelState.AddModelError("", "البريد الإلكتروني أو كلمة المرور غير صحيحة");
                    TempData["LoginError"] = "البريد الإلكتروني أو كلمة المرور غير صحيحة";
                }
            }

            return View(model);
        }
        public ActionResult Logout()
        {
            // قم بحذف بيانات الجلسة
            Session.Clear();
            Session.Abandon();

            // إعادة التوجيه إلى صفحة تسجيل الدخول أو الصفحة الرئيسية
            return RedirectToAction("LoginAdmin", "Home");
        }
        public ActionResult QA() { return View(); }
        public ActionResult ploice() { return  View(); }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}