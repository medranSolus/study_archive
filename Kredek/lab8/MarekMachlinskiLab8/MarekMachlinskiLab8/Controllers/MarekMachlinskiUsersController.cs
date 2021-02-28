using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarekMachlinskiLab8.Models;

namespace MarekMachlinskiLab8.Controllers
{
    public class MarekMachlinskiUsersController : Controller
    {
        private DB_9B1FC5_cpc20181Entities db = new DB_9B1FC5_cpc20181Entities();

        // GET: MarekMachlinskiUsers
        public ActionResult Index()
        {
            return View(db.MarekMachlinskiUsers.ToList());
        }

        // GET: MarekMachlinskiUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarekMachlinskiUser marekMachlinskiUser = db.MarekMachlinskiUsers.Find(id);
            if (marekMachlinskiUser == null)
            {
                return HttpNotFound();
            }
            return View(marekMachlinskiUser);
        }

        // GET: MarekMachlinskiUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MarekMachlinskiUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Login,Password")] MarekMachlinskiUser marekMachlinskiUser)
        {
            if (ModelState.IsValid)
            {
                db.MarekMachlinskiUsers.Add(marekMachlinskiUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marekMachlinskiUser);
        }

        // GET: MarekMachlinskiUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarekMachlinskiUser marekMachlinskiUser = db.MarekMachlinskiUsers.Find(id);
            if (marekMachlinskiUser == null)
            {
                return HttpNotFound();
            }
            return View(marekMachlinskiUser);
        }

        // POST: MarekMachlinskiUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Login,Password")] MarekMachlinskiUser marekMachlinskiUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marekMachlinskiUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marekMachlinskiUser);
        }

        // GET: MarekMachlinskiUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarekMachlinskiUser marekMachlinskiUser = db.MarekMachlinskiUsers.Find(id);
            if (marekMachlinskiUser == null)
            {
                return HttpNotFound();
            }
            return View(marekMachlinskiUser);
        }

        // POST: MarekMachlinskiUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MarekMachlinskiUser marekMachlinskiUser = db.MarekMachlinskiUsers.Find(id);
            db.MarekMachlinskiUsers.Remove(marekMachlinskiUser);
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
