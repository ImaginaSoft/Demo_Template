using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

using CustomLog;

namespace PeruTourism.Filters
{
    public class CustomErrorAttribute : IExceptionFilter
    {
        // =============================
        // eventos

        #region "eventos"

        public void OnException(ExceptionContext filterContext)
        {
            if (Bitacora.Current.IsErrorEnabled<CustomErrorAttribute>())
            {
                // registrando evento
                Bitacora.Current.Error<CustomErrorAttribute>(
                    filterContext.Exception,
                    new
                    {
                        action = filterContext.RouteData.Values["action"].ToString(),
                        controller = filterContext.RouteData.Values["controller"].ToString()
                    });
            }

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            if (filterContext.HttpContext.Request.IsAjaxRequest() || filterContext.IsChildAction)
            {
                filterContext.Result = new PartialViewResult
                {
                    ViewName = "~/Views/Shared/Error.cshtml"
                };

            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "controller", "Home" },
                            { "action", "Error" }
                        });
            }
        }

        #endregion
    }
}
