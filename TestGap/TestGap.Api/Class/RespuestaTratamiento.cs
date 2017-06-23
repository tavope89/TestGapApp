using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using TestGap.Api.Models;

namespace TestGap.Api.Class
{
    public class RespuestaTratamiento:RespuestaJsonWebApi
    {
        [DefaultValue(false)]
        public Tratamiento Tratamiento { get; set; }

        [DefaultValue(false)]
        public ICollection<Tratamiento> Tratamientos { get; set; }
    }
}