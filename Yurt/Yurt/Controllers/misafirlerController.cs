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
    public class misafirlerController : Controller
    {
        private YurtOtomasyonEntities db = new YurtOtomasyonEntities();

        // GET: misafirler
        public ActionResult Index()
        {
            return View(db.misafirler.ToList());
        }

        // GET: misafirler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            misafirler misafirler = db.misafirler.Find(id);
            if (misafirler == null)
            {
                return HttpNotFound();
            }
            return View(misafirler);
        }



        public ActionResult KayitEkle()
        { return View(); }

        [HttpPost]
        public ActionResult KayitEkle(FormCollection form)
        {

            misafirler veri = new misafirler();
            if (ModelState.IsValid)
            {
                veri.mis_tcPas = form["mis_tcPas"].Trim();
                veri.mis_ad = form["mis_ad"].Trim();
                veri.mis_soyad = form["mis_soyad"].Trim();
                veri.mis_cinsiyet = form["mis_cinsiyet"].Trim();
                veri.mis_uyruk = form["mis_uyruk"].Trim();
                veri.mis_tel = form["mis_tel"].Trim();
                veri.mis_giris =Convert.ToDateTime(form["mis_giris"].Trim());
                veri.mis_cikis = Convert.ToDateTime(form["mis_cikis"].Trim());
                veri.mis_kgun =Convert.ToInt32(form["mis_kgun"].Trim()) ;
                veri.mis_yatakucret = Convert.ToInt32(form["mis_yatakucret"].Trim());
                veri.mis_borc = veri.mis_yatakucret * veri.mis_kgun;
                db.misafirler.Add(veri);
                db.SaveChanges();
               
            } return RedirectToAction("Index", "Misafirler");
        }

        //Güncelleme
        public ActionResult KayitGuncelle(int? id)
        {
            misafirler misafirler = db.misafirler.Find(id);
            return View();


        }


        [HttpPost]
        public ActionResult KayitGuncelle([Bind(Include = "mis_id,mis_tcPas,mis_ad,mis_soyad,mis_cinsiyet,mis_uyruk,mis_tel,mis_giris,mis_cikis,mis_kgun,mis_yatakucret,mis_borc")] misafirler misafir)
        {

            db.Entry(misafir).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Misafirler");
        }

        //Silme
        public ActionResult KayitSil(int? id)
        {
            misafirler misafir = db.misafirler.Find(id);
            return View();
        }

        [HttpPost, ActionName("KayitSil")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            misafirler misafirler = db.misafirler.Find(id);
            db.misafirler.Remove(misafirler);
            db.SaveChanges();
            return RedirectToAction("Index","Misafirler");
        }


        //Arama
        public ActionResult ASArama()
        {
            return View(db.misafirler.ToList());
        }

        [HttpPost]
        public ActionResult ASArama(FormCollection form)
        {
            string aramaalani = form["alanadi"].Trim();
            string aranacakdeger = form["aranandeger"].Trim();
            if (aramaalani == "A")
            {
                return View(db.misafirler.Where(x => x.mis_ad.Contains(aranacakdeger)).ToList());
            }
            else
                return View(db.misafirler.Where(x => x.mis_soyad.Contains(aranacakdeger)).ToList());
        }













        //// GET: misafirler/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    misafirler misafirler = db.misafirler.Find(id);
        //    if (misafirler == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(misafirler);
        //}

        //// POST: misafirler/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "mis_id,mis_tcPas,mis_ad,mis_soyad,mis_cinsiyet,mis_uyruk,mis_tel,mis_giris,mis_cikis,mis_kgun,mis_yatakucret,mis_borc")] misafirler misafirler)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(misafirler).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(misafirler);
        //}

        //// GET: misafirler/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    misafirler misafirler = db.misafirler.Find(id);
        //    if (misafirler == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(misafirler);
        //}

        //// POST: misafirler/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    misafirler misafirler = db.misafirler.Find(id);
        //    db.misafirler.Remove(misafirler);
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
