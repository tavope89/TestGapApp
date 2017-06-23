using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using PagedList;
using TestGap.Api.Models;

namespace TestGap.Api.Controllers
{
    public class PacientesTestGapController : Controller
    {
        private GapTestEntitiesFramework db = new GapTestEntitiesFramework();

        /// <summary>
        /// Metodo que consulta toda la informacion de todos los clientes
        /// </summary>
        /// <param name="currentFilter"></param>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            IQueryable<Paciente> pacientes;
            if (!String.IsNullOrEmpty(searchString))
            {
                pacientes = db.Pacientes.Where(s => s.Identificacion.Contains(searchString)
                                               || s.Nombre.Contains(searchString)).OrderBy(o=>o.Id_Paciente);
            }
            else
            {
                pacientes = db.Pacientes.OrderBy(o=>o.Identificacion);
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(pacientes.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// Metodo que retorna los datos de un paciente
        /// </summary>
        /// <param name="id">identificador del paciente</param>
        /// <returns></returns>
        public async Task<ActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = await db.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        /// <summary>
        /// Metodo que retorna la vista para la creacion o el registro del un paciente
        /// </summary>
        /// <returns></returns>
        public ActionResult Crear()
        {
            return View();
        }

        /// <summary>
        /// Metodo que registra un paciente con toda su informacion
        /// </summary>
        /// <param name="paciente">Modelo que contiene toda la informacion del paciente a registrar</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear([Bind(Include = "Id_Paciente,Nombre,Identificacion,Edad,Correo,Telefono,Fecha_Ultima_Visita,Fecha_Proxima_Visita")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                db.Pacientes.Add(paciente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(paciente);
        }

        /// <summary>
        /// Metodo que muestra la pantalla para la edicion del paciente
        /// </summary>
        /// <param name="id">Identificador del paciente que se desea eliminar</param>
        /// <returns></returns>
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = await db.Pacientes.FindAsync(id);
            paciente.Tratamientos = db.Tratamientos.Where(x => x.Id_Paciente == paciente.Id_Paciente).ToList();
            if (paciente == null)
            {
                return HttpNotFound();
            }
            
            return View(paciente);
        }

        /// <summary>
        /// Metodo que Edita los datos del cliente
        /// </summary>
        /// <param name="paciente">Modelo que contiene toda la informacion del paciente a modificar</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar([Bind(Include = "Id_Paciente,Nombre,Identificacion,Edad,Correo,Telefono,Fecha_Ultima_Visita,Fecha_Proxima_Visita")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            paciente.Tratamientos = db.Tratamientos.Where(x => x.Id_Paciente == paciente.Id_Paciente).ToList();
            return View(paciente);
        }

        /// <summary>
        /// Metodo que muesta la pantalla de eliminacion de un paciente determinado
        /// </summary>
        /// <param name="id">Identificador del paciente que se desea eliminar</param>
        /// <returns></returns>
        public async Task<ActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = await db.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        /// <summary>
        /// Metodo que elimina permanentemente un registro de los pacientes
        /// </summary>
        /// <param name="id">Identificador del paciente que se desea eliminar</param>
        /// <returns></returns>
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmacionEliminacion(int id)
        {
            Paciente paciente = await db.Pacientes.FindAsync(id);
            db.Pacientes.Remove(paciente);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Metodo que realiza el dispose de los datos utilizados en las consultas realizadas
        /// </summary>
        /// <param name="disposing">Valor Boolean que verifica si se realiza el dispose o no de los datos</param>
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
