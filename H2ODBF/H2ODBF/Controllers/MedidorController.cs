using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using H2ODBF.Models;

namespace H2ODBF.Controllers
{
    public class MedidorController : Controller
    {
        private SistemaAguaEntities1 db = new SistemaAguaEntities1();

        // GET: Medidor
        public ActionResult Index()
        {
            var medidor = db.Medidor.Include(m => m.Usuario);
            return View(medidor.ToList());
        }

        // GET: Medidor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medidor medidor = db.Medidor.Find(id);
            if (medidor == null)
            {
                return HttpNotFound();
            }
            return View(medidor);
        }

        // GET: Medidor/Create
        public ActionResult Create()
        {
            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "nombre");
            return View();
        }

        // POST: Medidor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_medidor,numero_serie,fecha_instalacion,tipo_medidor,ubicacion,estado,fecha_ultima_revision,id_usuario")] Medidor medidor)
        {
            if (ModelState.IsValid)
            {
                db.Medidor.Add(medidor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "nombre", medidor.id_usuario);
            return View(medidor);
        }

        // GET: Medidor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medidor medidor = db.Medidor.Find(id);
            if (medidor == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "nombre", medidor.id_usuario);
            return View(medidor);
        }

        // POST: Medidor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_medidor,numero_serie,fecha_instalacion,tipo_medidor,ubicacion,estado,fecha_ultima_revision,id_usuario")] Medidor medidor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medidor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "nombre", medidor.id_usuario);
            return View(medidor);
        }

        // GET: Medidor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medidor medidor = db.Medidor.Find(id);
            if (medidor == null)
            {
                return HttpNotFound();
            }
            return View(medidor);
        }

        // POST: Medidor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medidor medidor = db.Medidor.Find(id);
            db.Medidor.Remove(medidor);
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
