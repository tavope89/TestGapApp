using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestGap.Api.Class;
using TestGap.Api.Models;

namespace TestGap.Api.Controllers
{
    public class TratamientosTestGapController : BaseController
    {
        private GapTestEntitiesFramework db = new GapTestEntitiesFramework();

        /// <summary>
        /// Metodo que Consulta todos los tratamiento especificos de un cliente
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Consulta(int id)
        {
            ViewBag.id = id;
            return RespuestaParcial();
        }

        /// <summary>
        /// Metodo que despliega el detalle de un registro especifico de un tratamiento
        /// </summary>
        /// <param name="id">Identificador del tramtamiento que se desea consultar</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var respuesta = new Respuesta();
            Tratamiento tratamiento = await db.Tratamientos.FindAsync(id);
            if (tratamiento == null)
            {
                return Json(respuesta);
            }
            return RespuestaParcial(new Respuesta(), Parametros.Rutas.Tratamientos.VistaParcialDetalleTratamiento, tratamiento);
        }

        /// <summary>
        /// Metodo que retorna la vista o la pantalla para el registro de la informacion del tratamiento
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Crear()
        {
            return RespuestaParcial(new Respuesta(), Parametros.Rutas.Tratamientos.VistaParcialInsertarTratamiento);
        }

        /// <summary>
        /// Metodo que registra en la BD el tratamiento especifico
        /// </summary>
        /// <param name="tratamiento">Modelo que contiene toda la informacion del tratamiento</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ProcesarCreacion([Bind(Include = "Id_Tratamiento,Id_Paciente,Fecha_Inicio,Fecha_Fin,Costo,Detalle")] Tratamiento tratamiento)
        {
            var respuesta = new Respuesta();
            if (ModelState.IsValid)
            {
                db.Tratamientos.Add(tratamiento);
                await db.SaveChangesAsync();
                ViewBag.id = tratamiento.Id_Paciente;
                return RespuestaParcial(respuesta);
            }
            Validation();
            return RespuestaParcialConError(respuesta, Parametros.Rutas.Tratamientos.VistaParcialInsertarTratamiento, tratamiento);
        }

        /// <summary>
        /// Metodo que muestra la pantalla para la edicion del tratamiento
        /// </summary>
        /// <param name="id">Identificador del tratamiento que se desea visualizar para editar</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var respuesta = new Respuesta();
            Tratamiento tratamiento = await db.Tratamientos.FindAsync(id);
            if (tratamiento == null)
            {
                return Json(respuesta);
            }
            return RespuestaParcial(respuesta, Parametros.Rutas.Tratamientos.VistaParcialEditarTratamiento, tratamiento);
        }

        /// <summary>
        /// Metodo que edita el registro en la base de datos del tratamiento respectivo
        /// </summary>
        /// <param name="tratamiento">modelo que contiene toda la informacion del tratamiento</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ProcesarEdicion([Bind(Include = "Id_Tratamiento,Id_Paciente,Fecha_Inicio,Fecha_Fin,Costo,Detalle")] Tratamiento tratamiento)
        {
            var respuesta = new Respuesta();
            if (ModelState.IsValid)
            {
                db.Entry(tratamiento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                ViewBag.id = tratamiento.Id_Paciente;
                return RespuestaParcial(respuesta);
            }
            Validation();
            return RespuestaParcialConError(respuesta, Parametros.Rutas.Tratamientos.VistaParcialEditarTratamiento, tratamiento);
        }

        /// <summary>
        /// Metodo que muestra la pantalla o vista de la eliminacion del tratamiento
        /// </summary>
        /// <param name="id">Identificador del tratamineto a eliminar</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var respuesta = new Respuesta();
            Tratamiento tratamiento = await db.Tratamientos.FindAsync(id);
            if (tratamiento == null)
            {
                return Json(respuesta);
            }
            return RespuestaParcial(respuesta, Parametros.Rutas.Tratamientos.VistaParcialEliminarTratamiento, tratamiento);
        }

        /// <summary>
        /// Metodo que elimina por completo el registro del tratamiento
        /// </summary>
        /// <param name="id">identificador del tramtamiento</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ProcesarEliminacion(int id)
        {
            Tratamiento tratamiento = await db.Tratamientos.FindAsync(id);
            db.Tratamientos.Remove(tratamiento);
            await db.SaveChangesAsync();
            ViewBag.id = tratamiento.Id_Paciente;
            return RespuestaParcial();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Metodo que realiza las validaciones del Model Tratamiento
        /// </summary>
        private void Validation()
        {
            ViewBag.ModelCheckFechaI = ModelState["tratamiento.Fecha_Inicio"].Errors.Any() ? ModelState["tratamiento.Fecha_Inicio"].Errors.First().ErrorMessage : null;
            ViewBag.ModelCheckFechaF = ModelState["tratamiento.Fecha_Fin"].Errors.Any() ? ModelState["tratamiento.Fecha_Fin"].Errors.First().ErrorMessage : null;
            ViewBag.ModelCheckCosto = ModelState["tratamiento.Costo"].Errors.Any() ? ModelState["tratamiento.Costo"].Errors.First().ErrorMessage : null;
            ViewBag.ModelCheckDetalle = ModelState["tratamiento.Detalle"].Errors.Any() ? ModelState["tratamiento.Detalle"].Errors.First().ErrorMessage : null;
        }

        /// <summary>
        /// Metodo que realiza o serializa el objeto respuesta este sera reutilizados en los return de los action que se requiera llamar por medio de un ajax
        /// y que el mismo renderice la parcial correspondiente
        /// </summary>
        /// <param name="respuesta">Objeto Respuesta que contiene los datos de cualquier tipo de respuesta</param>
        /// <param name="ruta"> Ruta de la vista que se desea realizar la renderizacion</param>
        /// <param name="modelo">Modelo que contiene los datos necesario para rellenar la vista o parcial correspondiente</param>
        /// <returns></returns>
        private JsonResult RespuestaParcial(Respuesta respuesta = null, string ruta = null, object modelo = null)
        {
            respuesta = respuesta ?? new Respuesta();
            int id = (int?)ViewBag.id ?? 0;
            respuesta.MensajeExito(new Dictionary<string, object>()
                {
                    {
                        Parametros.Constantes.Html,
                        RenderPartialViewToString(ruta??Parametros.Rutas.Tratamientos.VistaParcialConsultarTratamiento,
                        ruta==null? (db.Tratamientos.Where(t => t.Id_Paciente == id).ToList()):modelo)
                    }

                });
            return Json(respuesta);
        }

        /// <summary>
        /// Metodo que realiza o serializa el objeto respuesta este sera reutilizados en los return de los action que se requiera llamar por medio de un ajax
        /// y que el mismo renderice la parcial correspondiente que sea con Error
        /// </summary>
        /// <param name="respuesta">Objeto Respuesta que contiene los datos de cualquier tipo de respuesta</param>
        /// <param name="ruta"> Ruta de la vista que se desea realizar la renderizacion</param>
        /// <param name="modelo">Modelo que contiene los datos necesario para rellenar la vista o parcial correspondiente</param>
        /// <returns></returns>
        private JsonResult RespuestaParcialConError(Respuesta respuesta = null, string ruta = null, object modelo = null)
        {
            respuesta = respuesta ?? new Respuesta();
            int id = (int?)ViewBag.id ?? 0;
            respuesta.MensajeError(new Dictionary<string, object>()
                {
                    {
                        Parametros.Constantes.Html,
                        RenderPartialViewToString(ruta??Parametros.Rutas.Tratamientos.VistaParcialConsultarTratamiento,
                        ruta==null? (db.Tratamientos.Where(t => t.Id_Paciente == id).ToList()):modelo)
                    }

                });
            return Json(respuesta);
        }

    }
}
