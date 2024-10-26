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
    public class PagoController : Controller
    {
        private SistemaAguaEntities1 db = new SistemaAguaEntities1();

        // GET: Pago
        public ActionResult Index()
        {
            var pago = db.Pago.Include(p => p.Factura);
            return View(pago.ToList());
        }

        // GET: Pago/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = db.Pago.Find(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            return View(pago);
        }

        // GET: Pago/Create
        public ActionResult Create()
        {
            ViewBag.id_factura = new SelectList(db.Factura, "id_factura", "estado");
            return View();
        }

        // POST: Pago/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pago,id_factura,fecha_pago,monto_pagado,metodo_pago,estado")] Pago pago)
        {
            if (ModelState.IsValid)
            {
                db.Pago.Add(pago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_factura = new SelectList(db.Factura, "id_factura", "estado", pago.id_factura);
            return View(pago);
        }

        // GET: Pago/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = db.Pago.Find(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_factura = new SelectList(db.Factura, "id_factura", "estado", pago.id_factura);
            return View(pago);
        }

        // POST: Pago/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pago,id_factura,fecha_pago,monto_pagado,metodo_pago,estado")] Pago pago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_factura = new SelectList(db.Factura, "id_factura", "estado", pago.id_factura);
            return View(pago);
        }

        // GET: Pago/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = db.Pago.Find(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            return View(pago);
        }

        // POST: Pago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pago pago = db.Pago.Find(id);
            db.Pago.Remove(pago);
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
