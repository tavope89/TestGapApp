using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using TestGap.Api.Class;
using TestGap.Api.Models;

namespace TestGap.Api.Controllers
{
    //[Authorize]
    public class PacientesController : ApiController
    {
        private GapTestEntitiesFramework db = new GapTestEntitiesFramework();

        // GET: api/Pacientes
        /// <summary>
        /// Metodo que retorna los pacientes registrados
        /// </summary>
        /// <returns>Retorna la lista de pacientes</returns>
        public IHttpActionResult GetPacientes()
        {
            var respuesta= new RespuestaPaciente();
            try
            {
                respuesta.Pacientes = db.Pacientes.Take(5).ToList();
                return Json(respuesta);
            }
            catch (Exception)
            {
                return Json(respuesta.ServerError());
            }
        }

        // GET: api/Pacientes/5
        /// <summary>
        /// Metodo que obtiene un paciente especifico
        /// </summary>
        /// <param name="id">Identificador del paciente</param>
        /// <returns></returns>
        [ResponseType(typeof(Paciente))]
        public IHttpActionResult GetPaciente(int id)
        {
            var respuesta = new RespuestaPaciente();
            Paciente paciente = db.Pacientes.Find(id);
            if (paciente == null)
            {
                return Json(respuesta.RecordNotFound());
            }
            respuesta.Paciente = paciente;
            return Json(respuesta);
        }

        // PUT: api/Pacientes/5
        /// <summary>
        /// Metodo que actualiza un registro en especifico
        /// </summary>
        /// <param name="id">Identificador del paciente</param>
        /// <param name="paciente">Datos del paciente con las modificaciones</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPaciente(int id, Paciente paciente)
        {
            var respuesta = new RespuestaPaciente();
            if (!ModelState.IsValid || id != paciente.Id_Paciente)
            {
                return Json(respuesta.BadRequest());
            }

            db.Entry(paciente).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(id))
                {
                    return Json(respuesta.RecordNotFound()); ;
                }
                Json(respuesta.ServerError());
                
            }

            return Json(respuesta);
        }

        // POST: api/Pacientes
        /// <summary>
        /// Metodo que registra un paciente a la vez
        /// </summary>
        /// <param name="paciente">Objeto que contiene los datos del paciente</param>
        /// <returns></returns>
        [ResponseType(typeof(Paciente))]
        public IHttpActionResult PostPaciente(Paciente paciente)
        {
            var respuesta = new RespuestaPaciente();
            if (!ModelState.IsValid)
            {
                return Json(respuesta.BadRequest());
            }

            db.Pacientes.Add(paciente);
            db.SaveChanges();
            respuesta.Paciente = paciente;
            return Json(paciente);
        }

        // DELETE: api/Pacientes/5
        /// <summary>
        /// Meotodo que realiza la accion de eliminar un paciente
        /// </summary>
        /// <param name="id">identificador del paciente a eliminar</param>
        /// <returns></returns>
        [ResponseType(typeof(Paciente))]
        public IHttpActionResult DeletePaciente(int id)
        {
            var respuesta = new RespuestaPaciente();
            Paciente paciente = db.Pacientes.Find(id);
            if (paciente == null)
            {
                return Json(respuesta.RecordNotFound());
            }

            db.Pacientes.Remove(paciente);
            db.SaveChanges();

            respuesta.Paciente = paciente;
            return Json(respuesta);
        }

        /// <summary>
        /// metodo que realizar el dispose de la informacion consultada o registrada
        /// </summary>
        /// <param name="disposing">Boolean que define si se hace el dispose o no</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Metodo que verifica si existe o no un paciente
        /// </summary>
        /// <param name="id">Identificador del paciente a consultar</param>
        /// <returns></returns>
        private bool PacienteExists(int id)
        {
            return db.Pacientes.Count(e => e.Id_Paciente == id) > 0;
        }
    }
}