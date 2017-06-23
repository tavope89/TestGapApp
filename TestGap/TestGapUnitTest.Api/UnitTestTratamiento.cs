﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestGap.Api.Controllers;
using TestGap.Api.Models;

namespace TestGapUnitTest.Api
{
    [TestClass]
    public class UnitTestTratamiento
    {
        /// <summary>
        /// Metodo que se encarga de validar que la insercion se realice de manera de correcta
        /// </summary>
        [TestMethod]
        public void TestMethodRegistroTratamiento()
        {
            var pController = new PacientesController();
            var paciente = pController.PostPaciente(new Paciente()
            {
                Identificacion = "304370390",
                Nombre = "Gustavo Perez",
                Edad = 29,
                Telefono = "304370390",
                Fecha_Ultima_Visita = new DateTime(2017, 06, 06, 02, 30, 25),
                Fecha_Proxima_Visita = new DateTime(2017, 06, 06, 02, 30, 25),
                Correo = "tavope89@hotmail.com"

            });
            var idPaciente = pController.GetPacientes().ToList().Last().Id_Paciente;
            var tController = new TratamientosController();
            var temp = tController.PostTratamiento(new Tratamiento()
            {
                Costo = 25000,
                Fecha_Fin = new DateTime(2017, 06, 06, 02, 30, 25),
                Fecha_Inicio = new DateTime(2017, 06, 06, 02, 30, 25),
                Detalle = "tratamiento de coordales",
                Id_Paciente = idPaciente
            });
            Assert.IsNotNull(temp);
        }

        /// <summary>
        /// Metodo que se encarga de validar que la obtencion se realice de manera de correcta
        /// </summary>
        [TestMethod]
        public void TestMethodObtenerTratamiento()
        {
            var tController = new TratamientosController();
            var tratamientos = tController.GetTratamiento(tController.GetTratamientos().ToList().Last().Id_Tratamiento);
            Assert.IsNotNull(tratamientos);
        }

        /// <summary>
        /// Metodo que se encarga de validar que la consulta se realice de manera de correcta
        /// </summary>
        [TestMethod]
        public void TestMethodConsultarTratamientos()
        {
            var tController = new TratamientosController();
            var tratamientos = tController.GetTratamientos();
            Assert.IsTrue(tratamientos.Any());
        }

        /// <summary>
        /// Metodo que se encarga de validar que la actualizacion se realice de manera de correcta
        /// </summary>
        [TestMethod]
        public void TestMethodActualizarTratamiento()
        {
            var tController = new TratamientosController();
            var tratamiento = tController.GetTratamientos().ToList().Last();
            int costo = tratamiento.Costo;
            tratamiento.Costo = (short)(tratamiento.Costo + 1);
            tController.PutTratamiento(tratamiento.Id_Tratamiento, tratamiento);
            var tratamientoNuevo = tController.GetTratamientos().ToList().Last();
            Assert.IsTrue(tratamientoNuevo.Costo != costo);
        }

        /// <summary>
        /// Metodo que se encarga de validar que la eliminacion se realice de manera de correcta
        /// </summary>
        [TestMethod]
        public void TestMethodEliminarTratamiento()
        {
            var tController = new TratamientosController();
            var tratamiento = tController.GetTratamientos().ToList().Last();
            int idTratamiento = tratamiento.Id_Tratamiento;
            tController.DeleteTratamiento(idTratamiento);
            var t = tController.GetTratamiento(idTratamiento);

            Assert.IsTrue(t.ToString() == "System.Web.Http.Results.NotFoundResult");
        }
    }
}