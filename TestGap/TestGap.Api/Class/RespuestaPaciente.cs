using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using TestGap.Api.Models;

namespace TestGap.Api.Class
{
    public class RespuestaPaciente : RespuestaJsonWebApi
    {

        public RespuestaPaciente()
            : base()
        {
        }

        [DefaultValue(false)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Paciente Paciente { get; set; }

        [DefaultValue(false)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Paciente> Pacientes { get; set; }

        public void JsonToPacientes()
        {
        }


    }
}