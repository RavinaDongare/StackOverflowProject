using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackOverflowProject.CustomFilter
{
    public class UserAuthorizationFilterAttribute:FilterAttribute,IAuthorizationFilter
    {
        public void OnAuthorization( AuthorizationContext authorizationContext)
        {
            if (authorizationContext.HttpContext.Session["CurrentUserName"]==null)
            {
                authorizationContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new {Controller="Home",Action="Index"
                }));
            }
        }

        }
}