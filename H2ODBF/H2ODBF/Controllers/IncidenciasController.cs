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
    public class IncidenciasController : Controller
    {
        private SistemaAguaEntities1 db = new SistemaAguaEntities1();

        // GET: Incidencias
        public ActionResult Index()
        {
            var incidencia = db.Incidencia.Include(i => i.Medidor).Include(i => i.Usuario);
            return View(incidencia.ToList());
        }

        // GET: Incidencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incidencia incidencia = db.Incidencia.Find(id);
            if (incidencia == null)
            {
                return HttpNotFound();
            }
            return View(incidencia);
        }

        // GET: Incidencias/Create
        public ActionResult Create()
        {
            ViewBag.id_medidor = new SelectList(db.Medidor, "id_medidor", "numero_serie");
            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "nombre");
            return View();
        }

        // POST: Incidencias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_incidencia,id_usuario,id_medidor,tipo_incidencia,fecha_reporte,estado,detalle_resolucion")] Incidencia incidencia)
        {
            if (ModelState.IsValid)
            {
                db.Incidencia.Add(incidencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_medidor = new SelectList(db.Medidor, "id_medidor", "numero_serie", incidencia.id_medidor);
            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "nombre", incidencia.id_usuario);
            return View(incidencia);
        }

        // GET: Incidencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incidencia incidencia = db.Incidencia.Find(id);
            if (incidencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_medidor = new SelectList(db.Medidor, "id_medidor", "numero_serie", incidencia.id_medidor);
            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "nombre", incidencia.id_usuario);
            return View(incidencia);
        }

        // POST: Incidencias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_incidencia,id_usuario,id_medidor,tipo_incidencia,fecha_reporte,estado,detalle_resolucion")] Incidencia incidencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_medidor = new SelectList(db.Medidor, "id_medidor", "numero_serie", incidencia.id_medidor);
            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "nombre", incidencia.id_usuario);
            return View(incidencia);
        }

        // GET: Incidencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incidencia incidencia = db.Incidencia.Find(id);
            if (incidencia == null)
            {
                return HttpNotFound();
            }
            return View(incidencia);
        }

        // POST: Incidencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Incidencia incidencia = db.Incidencia.Find(id);
            db.Incidencia.Remove(incidencia);
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
