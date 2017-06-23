using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestGap.Api.Controllers;
using TestGap.Api.Models;

namespace TestGapUnitTest.Api
{
    [TestClass]
    public class UnitTestPaciente
    {

        /// <summary>
        /// Metodo que se encarga de validar que la insercion se realice de manera de correcta
        /// </summary>
        [TestMethod]
        public void TestMethodRegistroPaciente()
        {
            var pController = new PacientesController();
            var temp = pController.PostPaciente(new Paciente()
            {
                Identificacion = "304370390",
                Nombre = "Gustavo Perez",
                Edad = 29,
                Telefono = "304370390",
                Fecha_Ultima_Visita = new DateTime(2017,06,06,02,30,25),
                Fecha_Proxima_Visita = new DateTime(2017, 06, 06, 02, 30, 25),
                Correo = "tavope89@hotmail.com"
                
            });
            Assert.IsNotNull(temp);
        }

        /// <summary>
        /// Metodo que se encarga de validar que la obtencion se realice de manera de correcta
        /// </summary>
        [TestMethod]
        public void TestMethodObtenerPaciente()
        {
            var pController = new PacientesController();
            var pacientes = pController.GetPaciente(pController.GetPacientes().ToList().Last().Id_Paciente);
            Assert.IsNotNull(pacientes);
        }

        /// <summary>
        /// Metodo que se encarga de validar que la obtencion se realice de manera de correcta
        /// </summary>
        [TestMethod]
        public void TestMethodConsultarPacientes()
        {
            var pController= new PacientesController();
            var pacientes = pController.GetPacientes();
            Assert.IsTrue(pacientes.Any());
        }

        /// <summary>
        /// Metodo que se encarga de validar que la actualizacion se realice de manera de correcta
        /// </summary>
        [TestMethod]
        public void TestMethodActualizarPaciente()
        {
            var pController = new PacientesController();
            var paciente = pController.GetPacientes().ToList().Last();
            int edadActual = paciente.Edad;
            paciente.Edad = (short)(paciente.Edad + 1);
            pController.PutPaciente(paciente.Id_Paciente, paciente);
            var pacienteNuevo = pController.GetPacientes().ToList().Last();
            Assert.IsTrue(pacienteNuevo.Edad != edadActual);
        }

        /// <summary>
        /// Metodo que se encarga de validar que la eliminacion se realice de manera de correcta
        /// </summary>
        [TestMethod]
        public void TestMethodEliminarPaciente()
        {
            var pController = new PacientesController();
            var paciente = pController.GetPacientes().ToList().Last();
            int idPaciente = paciente.Id_Paciente;
            pController.DeletePaciente(idPaciente);
            var t=pController.GetPaciente(idPaciente);

            Assert.IsTrue(t.ToString() =="System.Web.Http.Results.NotFoundResult");
        }
    }
}
