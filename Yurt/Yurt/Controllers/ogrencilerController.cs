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
    public class ogrencilerController : Controller
    {
        private YurtOtomasyonEntities db = new YurtOtomasyonEntities();

        // GET: ogrenciler
        public ActionResult Index()
        {
            var ogrenciler = db.ogrenciler.Include(o => o.odalar);
            return View(ogrenciler.ToList());
        }

        // GET: ogrenciler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ogrenciler ogrenciler = db.ogrenciler.Find(id);
            if (ogrenciler == null)
            {
                return HttpNotFound();
            }
            return View(ogrenciler);
        }

        //Ekleme
        public ActionResult KayitEkle()
        {
            ViewBag.oda_id = new SelectList(db.odalar, "oda_id", "oda_no");
            return View();

        }

        [HttpPost]

        public ActionResult KayitEkle(FormCollection form)
        {

            ogrenciler veri = new ogrenciler();
            if (ModelState.IsValid)
            {
                veri.ogr_tcPas = form["ogr_tcPas"].Trim();
                veri.ogr_ad = form["ogr_ad"].Trim();
                veri.ogr_soyad = form["ogr_soyad"].Trim();
                veri.ogr_cinsiyet = form["ogr_cinsiyet"].Trim();
                veri.ogr_kan = form["ogr_kan"].Trim();
                veri.ogr_uyruk = form["ogr_uyruk"].Trim();
                veri.ogr_tel = form["ogr_tel"].Trim();
                veri.ogr_email = form["ogr_email"].Trim();
                veri.ogr_adres = form["ogr_adres"].Trim();
                veri.ogr_egitim = form["ogr_egitim"].Trim();
                db.ogrenciler.Add(veri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.oda_id = new SelectList(db.odalar, "oda_id", "oda_no", veri.oda_id);
            return RedirectToAction("Index", "Ogrenciler");
        }





        //Güncelleme
        public ActionResult KayitGuncelle(int? id)
        {
            ogrenciler ogrenciler = db.ogrenciler.Find(id);
            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KayitGuncelle(
            [Bind(Include = "ogr_id,ogr_tcPas,ogr_ad,ogr_soyad,ogr_cinsiyet,ogr_kan,ogr_uyruk,ogr_tel,ogr_email,ogr_adres,ogr_egitim,oda_id")]
        ogrenciler ogrenciler)
        {
            db.Entry(ogrenciler).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Ogrenciler");
        }

        


        //Silme
        public ActionResult KayitSil(int? id)
        {
            ogrenciler ogrenci = db.ogrenciler.Find(id);
            return View(ogrenci);
        }

        [HttpPost, ActionName("KayitSil")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ogrenciler ogrenci = db.ogrenciler.Find(id);
            db.ogrenciler.Remove(ogrenci);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Arama
        public ActionResult AdSoyadArama()
        {
            return View(db.ogrenciler.ToList());
        }

        [HttpPost]
        public ActionResult AdSoyadArama(FormCollection form)
        {
            string aramaalani = form["alanadi"].Trim();
            string aranacakdeger = form["aranandeger"].Trim();
            if (aramaalani == "a")
            {
                return View(db.ogrenciler.Where(x=>x.ogr_ad.Contains(aranacakdeger)).ToList());
            }
            else
                return View(db.ogrenciler.Where(x => x.ogr_soyad.Contains(aranacakdeger)).ToList());
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
