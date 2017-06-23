using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestGap.Api.Class;

namespace TestGap.Api.Models
{
    public class Respuesta
    {
        /// <summary>
        /// 
        /// </summary>
        public Respuesta()
        {
            MensajeError();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objetoRespuesta"></param>
        public void MensajeExito(object objetoRespuesta=null)
        {
            CodigoRepuesta = Parametros.CodigosMensajes.Exito;
            MensajeRespuesta = Parametros.Mensajes.MSJExito;
            ObjetoRetornado = objetoRespuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        public void MensajeError(object objetoRespuesta = null)
        {
            CodigoRepuesta = Parametros.CodigosMensajes.Error;
            MensajeRespuesta = Parametros.Mensajes.MSJError;
            ObjetoRetornado = objetoRespuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        public int CodigoRepuesta { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string MensajeRespuesta { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object ObjetoRetornado { get; set; }
    }
}