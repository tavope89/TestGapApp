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
            var id =
               (((System.Web.Http.Results.JsonResult<TestGap.Api.Class.RespuestaPaciente>)(pController.GetPacientes()))
                   .Content.Pacientes).ToList().Last().Id_Paciente;
            var pacientes = pController.GetPaciente(id);
            Assert.IsTrue((((TestGap.Api.Models.RespuestaJsonWebApi)(((System.Web.Http.Results.JsonResult<TestGap.Api.Class.RespuestaPaciente>)(pacientes)).Content)).success));
        }

        /// <summary>
        /// Metodo que se encarga de validar que la obtencion se realice de manera de correcta
        /// </summary>
        [TestMethod]
        public void TestMethodConsultarPacientes()
        {
            var pController= new PacientesController();
            var pacientes =
                ((System.Web.Http.Results.JsonResult<TestGap.Api.Class.RespuestaPaciente>)(pController.GetPacientes()))
                    .Content.Pacientes;
            Assert.IsTrue(pacientes.Any());
        }

        /// <summary>
        /// Metodo que se encarga de validar que la actualizacion se realice de manera de correcta
        /// </summary>
        [TestMethod]
        public void TestMethodActualizarPaciente()
        {
            var pController = new PacientesController();
            var paciente = ((System.Web.Http.Results.JsonResult<TestGap.Api.Class.RespuestaPaciente>)(pController.GetPacientes()))
                    .Content.Pacientes.Last();
            int edadActual = paciente.Edad;
            paciente.Edad = (short)(paciente.Edad + 1);
            pController.PutPaciente(paciente.Id_Paciente, paciente);
            var pacienteNuevo = ((System.Web.Http.Results.JsonResult<TestGap.Api.Class.RespuestaPaciente>)(pController.GetPacientes()))
                    .Content.Pacientes.Last();
            Assert.IsTrue(pacienteNuevo.Edad != edadActual);
        }

        /// <summary>
        /// Metodo que se encarga de validar que la eliminacion se realice de manera de correcta
        /// </summary>
        [TestMethod]
        public void TestMethodEliminarPaciente()
        {
            var pController = new PacientesController();
            var paciente = ((System.Web.Http.Results.JsonResult<TestGap.Api.Class.RespuestaPaciente>)(pController.GetPacientes()))
                    .Content.Pacientes.Last();
            int idPaciente = paciente.Id_Paciente;
            pController.DeletePaciente(idPaciente);
            var t = pController.GetPaciente(idPaciente);

            Assert.IsTrue(!(((System.Web.Http.Results.JsonResult<TestGap.Api.Models.RespuestaJsonWebApi>)(t)).Content).success);
        }
    }
}
