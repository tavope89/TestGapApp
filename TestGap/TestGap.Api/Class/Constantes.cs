using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestGap.Api.Class
{
    public class Parametros
    {
        /// <summary>
        /// Codigos de Mensajes
        /// </summary>
        public struct CodigosMensajes
        {
            public const int Exito = 0;
            public const int Error = -1;
        }

        /// <summary>
        /// Mensajes de Error
        /// </summary>
        public struct Mensajes
        {
            public const string MSJError = "Ocurrió un error en la aplicación, intente de nuevo.";
            public const string MSJExito = "La operación se realizo con éxito.";
        }

        /// <summary>
        /// Mensajes de Error
        /// </summary>
        public struct Constantes
        {
            public const string Html = "html";
        }

        /// <summary>
        /// Mensajes de Error
        /// </summary>
        public struct Rutas
        {
            /// <summary>
            /// Mensajes de Error
            /// </summary>
            public struct Tratamientos
            {
                public const string VistaParcialInsertarTratamiento = "../TratamientosTestGap/Crear";
                public const string VistaParcialDetalleTratamiento = "../TratamientosTestGap/Detalle";
                public const string VistaParcialEditarTratamiento = "../TratamientosTestGap/Editar";
                public const string VistaParcialConsultarTratamiento = "../TratamientosTestGap/Index";
                public const string VistaParcialEliminarTratamiento = "../TratamientosTestGap/Eliminar";
            }
            /// <summary>
            /// Mensajes de Error
            /// </summary>
            public struct Pacientes
            {
                public const string VistaParcialInsertarPaciente = "../PacientesWeb/InsertarEditarPaciente";
                public const string VistaParcialConsultarPaciente = "../PacientesWeb/ConsultarPacientes";
            }
            
        }

        /// <summary>
        /// Mensajes de Error
        /// </summary>
        public struct RutasRecursos
        {
            /// <summary>
            /// Mensajes de Error
            /// </summary>
            public struct Imagenes
            {
                public const string GifCargando = "/Content/Imagenes/loading.gif";
            }
            
            
        }


        
    }
}