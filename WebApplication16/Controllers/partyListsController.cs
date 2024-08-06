using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;


using System.Web.Mvc;
using WebApplication16.Models;

namespace WebApplication16.Controllers
{
    public class partyListsController : Controller
    {
        private ElectionsEntities1 db = new ElectionsEntities1();

        // GET: partyLists
        public ActionResult IndexAdmin()
        {
            // استرجاع بيانات الحزب من قاعدة البيانات
            var partyLists = db.partyLists.ToList();
            return View(partyLists);
        }
       
        


        // GET: partyLists/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            partyList partyList = db.partyLists.Find(id);
            if (partyList == null)
            {
                return HttpNotFound();
            }
            return View(partyList);
        }
        public ActionResult Createuser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createuser([Bind(Include = "id,partyName,counter")] partyList partyList)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Check if a party with the same name already exists
                    var existingParty = db.partyLists.FirstOrDefault(p => p.partyName == partyList.partyName);
                    //if (existingParty != null)
                    //{
                    //    ModelState.AddModelError("partyName", "A party with the same name already exists.");
                    //    return View(partyList);
                    //}

                    db.partyLists.Add(partyList);
                    db.SaveChanges();
                    Session["PartyId"] = partyList.id;
                    Session["PartyName"] = partyList.partyName;
                    Session["Counter"] = 0;

                    return RedirectToAction("index", "partyCandidates"); // تأكد من صحة اسم الـ Controller والإجراء
                }
                catch (DbEntityValidationException ex)
                {
                    // Print the details to the error log or console
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }

            return View(partyList);
        }


        // GET: partyLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: partyLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,partyName,counter")] partyList partyList)
        {
            if (ModelState.IsValid)
            {
                db.partyLists.Add(partyList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(partyList);
        }

        // GET: partyLists/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            partyList partyList = db.partyLists.Find(id);
            if (partyList == null)
            {
                return HttpNotFound();
            }
            return View(partyList);
        }

        // POST: partyLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,partyName,counter")] partyList partyList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partyList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partyList);
        }

        // GET: partyLists/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            partyList partyList = db.partyLists.Find(id);
            if (partyList == null)
            {
                return HttpNotFound();
            }
            return View(partyList);
        }

        // POST: partyLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            partyList partyList = db.partyLists.Find(id);
            db.partyLists.Remove(partyList);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
