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
    public class yataklarController : Controller
    {
        private YurtOtomasyonEntities db = new YurtOtomasyonEntities();

        // GET: yataklar
        public ActionResult Index()
        {
            var yataklar = db.yataklar.Include(y => y.odalar);
            return View(yataklar.ToList());
        }

        // GET: yataklar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yataklar yataklar = db.yataklar.Find(id);
            if (yataklar == null)
            {
                return HttpNotFound();
            }
            return View(yataklar);
        }




        public ActionResult YatakEkle()
        {
            
            return View();

        }

        [HttpPost]

        public ActionResult YatakEkle(FormCollection form)
        {
            yataklar veri = new yataklar();
            if (ModelState.IsValid)
            {
                
                veri.yat_no = form["yat_no"].Trim();
                veri.yat_durum = form["yat_durum"].Trim();
                db.yataklar.Add(veri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Index", "Yataklar");
        }










        //Güncelleme
        public ActionResult YatakGuncelle(int? id)
        {
            yataklar yatak= db.yataklar.Find(id);
            
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YatakGuncelle([Bind(Include = "yat_id,yat_no,oda_id")] yataklar yatak)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yatak).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Yataklar");
            }
            
            return View(yatak);
        }
        
        //Silme
        public ActionResult YatakSil(int? id)
        {
            yataklar yatak= db.yataklar.Find(id);
            return View();
        }

        [HttpPost, ActionName("YatakSil")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            yataklar yatak = db.yataklar.Find(id);
            db.yataklar.Remove(yatak);
            db.SaveChanges();
            return RedirectToAction("Index","Yataklar");
        }

        //Arama
        public ActionResult YatakArama(string aranan)
        {
            return View(db.yataklar.Where(x => x.yat_no.Contains(aranan) || aranan == null).ToList());
        }




















        //// GET: yataklar/Create
        //public ActionResult Create()
        //{
        //    ViewBag.oda_id = new SelectList(db.odalar, "oda_id", "oda_no");
        //    return View();
        //}

        //// POST: yataklar/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "yat_id,yat_no,oda_id")] yataklar yataklar)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.yataklar.Add(yataklar);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.oda_id = new SelectList(db.odalar, "oda_id", "oda_no", yataklar.oda_id);
        //    return View(yataklar);
        //}

        //// GET: yataklar/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    yataklar yataklar = db.yataklar.Find(id);
        //    if (yataklar == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.oda_id = new SelectList(db.odalar, "oda_id", "oda_no", yataklar.oda_id);
        //    return View(yataklar);
        //}

        //// POST: yataklar/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "yat_id,yat_no,oda_id")] yataklar yataklar)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(yataklar).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.oda_id = new SelectList(db.odalar, "oda_id", "oda_no", yataklar.oda_id);
        //    return View(yataklar);
        //}

        //// GET: yataklar/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    yataklar yataklar = db.yataklar.Find(id);
        //    if (yataklar == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(yataklar);
        //}

        //// POST: yataklar/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    yataklar yataklar = db.yataklar.Find(id);
        //    db.yataklar.Remove(yataklar);
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
