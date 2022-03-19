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
    public class odalarController : Controller
    {
        private YurtOtomasyonEntities db = new YurtOtomasyonEntities();

        // GET: odalar
        public ActionResult Index()
        {
            return View(db.odalar.ToList());
        }

        // GET: odalar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            odalar odalar = db.odalar.Find(id);
            if (odalar == null)
            {
                return HttpNotFound();
            }
            return View(odalar);
        }

        public ActionResult OdaEkle()
        {
            return View();
        }


        [HttpPost]
        public ActionResult OdaEkle(FormCollection form)
        {
            odalar veri = new odalar();
            veri.oda_no = form["oda_no"].Trim();
            db.odalar.Add(veri);
            db.SaveChanges();
            return RedirectToAction("Index", "Odalar");
        }


        //Güncelleme
        public ActionResult OdaGuncelle(int? id)
        {
            odalar odalar = db.odalar.Find(id);
            return View();


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OdaGuncelle([Bind(Include = "oda_id,oda_no")] odalar odalar)
        {

            db.Entry(odalar).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Odalar");
        }


        //Silme
        public ActionResult OdaSil(int? id)
        {
            odalar oda = db.odalar.Find(id);
            return View();
        }

        [HttpPost, ActionName("OdaSil")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            odalar oda = db.odalar.Find(id);
            db.odalar.Remove(oda);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Arama
        public ActionResult OdaArama(string aranan)
        {
            return View(db.odalar.Where(x=>x.oda_no.Contains(aranan)||aranan==null).ToList());
        }

      

        //// GET: odalar/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

            //// POST: odalar/Create
            //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public ActionResult Create([Bind(Include = "oda_id,oda_no")] odalar odalar)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        db.odalar.Add(odalar);
            //        db.SaveChanges();
            //        return RedirectToAction("Index");
            //    }

            //    return View(odalar);
            //}

            //// GET: odalar/Edit/5
            //public ActionResult Edit(int? id)
            //{
            //    if (id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            //    odalar odalar = db.odalar.Find(id);
            //    if (odalar == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    return View(odalar);
            //}

            //// POST: odalar/Edit/5
            //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public ActionResult Edit([Bind(Include = "oda_id,oda_no")] odalar odalar)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        db.Entry(odalar).State = EntityState.Modified;
            //        db.SaveChanges();
            //        return RedirectToAction("Index");
            //    }
            //    return View(odalar);
            //}

            //// GET: odalar/Delete/5
            //public ActionResult Delete(int? id)
            //{
            //    if (id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            //    odalar odalar = db.odalar.Find(id);
            //    if (odalar == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    return View(odalar);
            //}

            //// POST: odalar/Delete/5
            //[HttpPost, ActionName("Delete")]
            //[ValidateAntiForgeryToken]
            //public ActionResult DeleteConfirmed(int id)
            //{
            //    odalar odalar = db.odalar.Find(id);
            //    db.odalar.Remove(odalar);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

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
