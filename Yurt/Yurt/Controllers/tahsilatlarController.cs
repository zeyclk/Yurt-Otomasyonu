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
    public class tahsilatlarController : Controller
    {
        private YurtOtomasyonEntities db = new YurtOtomasyonEntities();

        // GET: tahsilatlar
        public ActionResult Index()
        {
            var tahsilatlar = db.tahsilatlar.Include(t => t.ogrenciler);
            return View(tahsilatlar.ToList());
        }

        // GET: tahsilatlar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tahsilatlar tahsilatlar = db.tahsilatlar.Find(id);
            if (tahsilatlar == null)
            {
                return HttpNotFound();
            }
            return View(tahsilatlar);
        }



        public ActionResult TahsilatEkle()
        {
            ViewBag.tah_ogrID = new SelectList(db.ogrenciler, "ogr_id", "ogr_tcPas");
            return View();

        }

        [HttpPost]

        public ActionResult TahsilatEkle(FormCollection form)
        {
            tahsilatlar veri = new tahsilatlar();
            if (ModelState.IsValid)
            {
                veri.tah_tutar = form["tah_tutar"].Trim();
                veri.tah_sontarih = Convert.ToDateTime(form["tah_sontarih"].Trim());
                veri.tah_durum = form["tah_durum"].Trim();
                db.tahsilatlar.Add(veri);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.tah_ogrID = new SelectList(db.ogrenciler, "ogr_id", "ogr_tcPas", veri.tah_ogrID);
            return RedirectToAction("Index", "Tahsilatlar");
        }

        //Güncelleme
        public ActionResult TahsilatGuncelle(int? id)
        {
            tahsilatlar tahsilat= db.tahsilatlar.Find(id);

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TahsilatGuncelle([Bind(Include = "yat_id,yat_no,oda_id")] tahsilatlar tahsilat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tahsilat).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Tahsilatlar");
            }

            return View(tahsilat);
        }


        //Silme
        public ActionResult TahsilatSil(int? id)
        {
            tahsilatlar tahsilat = db.tahsilatlar.Find(id);
            return View();
        }

        [HttpPost, ActionName("TahsilatSil")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tahsilatlar tahsilat = db.tahsilatlar.Find(id);
            db.tahsilatlar.Remove(tahsilat);
            db.SaveChanges();
            return RedirectToAction("Index", "Tahsilatlar");
        }

        //Arama
        public ActionResult TahsilatArama(string aranan)
        {
            return View(db.tahsilatlar.Where(x => (x.tah_id).ToString().Contains(aranan) || aranan == null).ToList());
        }


        public ActionResult HepsiniSil()
        {
            db.Database.ExecuteSqlCommand("SP_hepsiniSil");
            return RedirectToAction("Index", "Tahsilatlar");
        }











        //// GET: tahsilatlar/Create
        //public ActionResult Create()
        //{
        //    ViewBag.tah_ogrID = new SelectList(db.ogrenciler, "ogr_id", "ogr_tcPas");
        //    return View();
        //}

        //// POST: tahsilatlar/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "tah_id,tah_ogrID,tah_tutar,tah_sontarih,tah_durum")] tahsilatlar tahsilatlar)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.tahsilatlar.Add(tahsilatlar);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.tah_ogrID = new SelectList(db.ogrenciler, "ogr_id", "ogr_tcPas", tahsilatlar.tah_ogrID);
        //    return View(tahsilatlar);
        //}

        //// GET: tahsilatlar/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tahsilatlar tahsilatlar = db.tahsilatlar.Find(id);
        //    if (tahsilatlar == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.tah_ogrID = new SelectList(db.ogrenciler, "ogr_id", "ogr_tcPas", tahsilatlar.tah_ogrID);
        //    return View(tahsilatlar);
        //}

        //// POST: tahsilatlar/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "tah_id,tah_ogrID,tah_tutar,tah_sontarih,tah_durum")] tahsilatlar tahsilatlar)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tahsilatlar).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.tah_ogrID = new SelectList(db.ogrenciler, "ogr_id", "ogr_tcPas", tahsilatlar.tah_ogrID);
        //    return View(tahsilatlar);
        //}

        //// GET: tahsilatlar/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tahsilatlar tahsilatlar = db.tahsilatlar.Find(id);
        //    if (tahsilatlar == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tahsilatlar);
        //}

        //// POST: tahsilatlar/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tahsilatlar tahsilatlar = db.tahsilatlar.Find(id);
        //    db.tahsilatlar.Remove(tahsilatlar);
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
