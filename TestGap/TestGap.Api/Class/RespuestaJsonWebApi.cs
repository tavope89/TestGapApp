using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using TestGap.Api.Class;

namespace TestGap.Api.Models
{
    public class RespuestaJsonWebApi
    {
        public bool success { get; set; }

        [DefaultValue(false)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? error_code { get; set; }

        [DefaultValue(false)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string error_msg { get; set; }

        public RespuestaJsonWebApi()
        {
            success = true;
        }

        public RespuestaJsonWebApi BadRequest()
        {
            success = false;
            error_code = Parametros.CodigosMensajes.BadRequest;
            error_msg = Parametros.Mensajes.BadRequest;
            return this;
        }

        public RespuestaJsonWebApi NotAuthorized()
        {
            success = false;
            error_code = Parametros.CodigosMensajes.NotAuthorized;
            error_msg = Parametros.Mensajes.NotAuthorized;
            return this;
        }

        public RespuestaJsonWebApi RecordNotFound()
        {
            success = false;
            error_code = Parametros.CodigosMensajes.RecordNotFound;
            error_msg = Parametros.Mensajes.RecordNotFound;
            return this;
        }

        public RespuestaJsonWebApi ServerError()
        {
            success = false;
            error_code = Parametros.CodigosMensajes.ServerError;
            error_msg = Parametros.Mensajes.ServerError;
            return this;
        }
    }
}