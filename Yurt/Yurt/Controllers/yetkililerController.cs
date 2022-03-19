using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Yurt.Models;

namespace Yurt.Controllers
{
    public class yetkililerController : Controller
    {
        private YurtOtomasyonEntities db = new YurtOtomasyonEntities();

        // GET: yetkililer
        public ActionResult Index()
        {
            return View(db.yetkililer.ToList());
        }

        // GET: yetkililer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yetkililer yetkililer = db.yetkililer.Find(id);
            if (yetkililer == null)
            {
                return HttpNotFound();
            }
            return View(yetkililer);
        }

        // GET: yetkililer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: yetkililer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "yet_id,yet_kullaniciAd,yet_sifre,yet_tel,yet_email")] yetkililer yetkililer)
        {
            if (ModelState.IsValid)
            {
                db.yetkililer.Add(yetkililer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yetkililer);
        }

        // GET: yetkililer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yetkililer yetkililer = db.yetkililer.Find(id);
            if (yetkililer == null)
            {
                return HttpNotFound();
            }
            return View(yetkililer);
        }

        // POST: yetkililer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "yet_id,yet_kullaniciAd,yet_sifre,yet_tel,yet_email")] yetkililer yetkililer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yetkililer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yetkililer);
        }

        // GET: yetkililer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yetkililer yetkililer = db.yetkililer.Find(id);
            if (yetkililer == null)
            {
                return HttpNotFound();
            }
            return View(yetkililer);
        }

        // POST: yetkililer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            yetkililer yetkililer = db.yetkililer.Find(id);
            db.yetkililer.Remove(yetkililer);
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
