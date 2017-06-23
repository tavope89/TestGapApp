using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TestGap.Api.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Sobreescritura de metodo para agregar una viewBag Global
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.VirtualPahtGlobal = Request.ApplicationPath != "/" ? Request.ApplicationPath : "";
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Método que renderiza una vista o una parcial para retornarlo como string
        /// </summary>
        /// <param name="nombreVista">Nombre de la vista a renderizar</param>
        /// <param name="model">Objeto model que se le pasa por parámetro a la vista</param>
        /// <returns></returns>
        protected string RenderPartialViewToString(string nombreVista, object model = null)
        {
            if (string.IsNullOrEmpty(nombreVista))
            {
                nombreVista = ControllerContext.RouteData.GetRequiredString("action");
            }
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, nombreVista);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Se utiliza para determinar la dirección del identificador de ordenación utilizado al filtrar listas
        /// </summary>
        /// <param name = "htmlHelper"> </param>
        /// <param name = "sortOrder"> el orden actual utilizado en la página </param>
        /// <param name = "field"> el campo al que estamos adjuntando este identificador de clasificación a </param>
        /// <returns> MvcHtmlString utilizado para indicar el orden de clasificación del campo </returns>
        public static IHtmlString SortIdentifier( HtmlHelper htmlHelper, string sortOrder, string field)
        {
            if (string.IsNullOrEmpty(sortOrder) || (sortOrder.Trim() != field && sortOrder.Replace("_desc", "").Trim() != field)) return null;

            string glyph = "glyphicon glyphicon-chevron-up";
            if (sortOrder.ToLower().Contains("desc"))
            {
                glyph = "glyphicon glyphicon-chevron-down";
            }

            var span = new TagBuilder("span");
            span.Attributes["class"] = glyph;

            return MvcHtmlString.Create(span.ToString());
        }

        /// <summary>
        /// Convierte una NameValueCollection en un RouteValueDictionary que contiene todos los elementos de la colección, y opcionalmente agrega
        /// {newKey: newValue} si no son nulos
        /// </summary>
        /// <param name = "collection"> Colección NameValue para convertir en un RouteValueDictionary </ param>
        /// <param name = "newKey"> el nombre de una clave para agregar a RouteValueDictionary </ param>
        /// <param name = "newValue"> el valor asociado con newKey para agregar a RouteValueDictionary </ param>
        /// <return> Un RouteValueDictionary que contiene todas las claves de la colección, así como {newKey: newValue} si no son nulos </return>
        public static RouteValueDictionary ToRouteValueDictionary( NameValueCollection collection, string newKey, string newValue)
        {
            var routeValueDictionary = new RouteValueDictionary();
            foreach (var key in collection.AllKeys)
            {
                if (key == null) continue;
                if (routeValueDictionary.ContainsKey(key))
                    routeValueDictionary.Remove(key);

                routeValueDictionary.Add(key, collection[key]);
            }
            if (string.IsNullOrEmpty(newValue))
            {
                routeValueDictionary.Remove(newKey);
            }
            else
            {
                if (routeValueDictionary.ContainsKey(newKey))
                    routeValueDictionary.Remove(newKey);

                routeValueDictionary.Add(newKey, newValue);
            }
            return routeValueDictionary;
        }
    }
}