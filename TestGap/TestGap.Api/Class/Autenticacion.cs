using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Configuration;

namespace TestGap.Api
{
    public class Autenticacion : IHttpModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += OnAuthenticateRequest;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAuthenticateRequest(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            var request = new HttpRequestWrapper(application.Request);

            var authData = request.Headers["Autorizacion"];

            if (!string.IsNullOrEmpty(authData))
            {
                var user = authData.Substring(0, authData.IndexOf(':'));
                var password = authData.Substring(authData.IndexOf(':') + 1);
                var autorizado = WebConfigurationManager.AppSettings["user"].ToString() == user;
                autorizado = autorizado && WebConfigurationManager.AppSettings["password"].ToString() == password;
                if (autorizado) 
                {
                    var principal = new GenericPrincipal(new GenericIdentity(user), null);
                    PutPrincipal(principal);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="principal"></param>
        private void PutPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }
    }
}