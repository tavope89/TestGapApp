﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TestGap.Api.Models;

namespace TestGap.Api.Controllers
{
    public class TratamientosController : ApiController
    {
        private GapTestEntitiesFramework db = new GapTestEntitiesFramework();

        // GET: api/Tratamientos
        /// <summary>
        /// Metodo que se encarga de consultar todos los tratamientos registrados
        /// </summary>
        /// <returns></returns>
        public IQueryable<Tratamiento> GetTratamientos()
        {
            return db.Tratamientos;
        }

        // GET: api/Tratamientos/5
        /// <summary>
        /// Metodo que obtiene un tratamiento en especifico
        /// </summary>
        /// <param name="id">identificador del tratamiento que se desea consultar</param>
        /// <returns></returns>
        [ResponseType(typeof(Tratamiento))]
        public IHttpActionResult GetTratamiento(int id)
        {
            Tratamiento tratamiento = db.Tratamientos.Find(id);
            if (tratamiento == null)
            {
                return NotFound();
            }

            return Ok(tratamiento);
        }

        // PUT: api/Tratamientos/5
        /// <summary>
        /// Metodo que se encarga de la actualizacion de un registro de un tratamiento
        /// </summary>
        /// <param name="id">Identificador del tramtamiento que se deasea actualizar</param>
        /// <param name="tratamiento">objeto que contiene los datos de la actualizacion a realizarse</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTratamiento(int id, Tratamiento tratamiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tratamiento.Id_Tratamiento)
            {
                return BadRequest();
            }

            db.Entry(tratamiento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TratamientoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tratamientos
        /// <summary>
        /// Metodo que realiza el registro de un tratamiento en especifico
        /// </summary>
        /// <param name="tratamiento">Objeto que contiene los datos dek tratamiento</param>
        /// <returns></returns>
        [ResponseType(typeof(Tratamiento))]
        public IHttpActionResult PostTratamiento(Tratamiento tratamiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tratamientos.Add(tratamiento);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tratamiento.Id_Tratamiento }, tratamiento);
        }

        // DELETE: api/Tratamientos/5
        /// <summary>
        /// Metodo que elimina un registro de tratamiento permanentemente
        /// </summary>
        /// <param name="id">Identificador del tratamiento a eliminar</param>
        /// <returns></returns>
        [ResponseType(typeof(Tratamiento))]
        public IHttpActionResult DeleteTratamiento(int id)
        {
            Tratamiento tratamiento = db.Tratamientos.Find(id);
            if (tratamiento == null)
            {
                return NotFound();
            }

            db.Tratamientos.Remove(tratamiento);
            db.SaveChanges();

            return Ok(tratamiento);
        }

        /// <summary>
        /// Metodo que realiza un dispose al objeto que contiene la inicializacion de los datos utilizados en las consultas o registros
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        /// <summary>
        /// Meotodo que verifica si un tratamiento se encuentra registrado o no
        /// </summary>
        /// <param name="id">Identificador del tratamiento a consultar</param>
        /// <returns></returns>
        private bool TratamientoExists(int id)
        {
            return db.Tratamientos.Count(e => e.Id_Tratamiento == id) > 0;
        }
    }
}