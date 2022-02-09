using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZeppelinWebsite.Filters
{
    public class CustomAuthorizeAttribute: AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult routeData = null;
            if (HttpContext.Current.User == null || String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            {
                routeData=new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                    values:new
                    {

                        Controller = "Account",
                        action= "Index",

                    
                
                }));
            }

            else
            {
                routeData = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                   values: new
                   {

                       Controller = "Flight",
                       action = "Index",



                   }));
            }

            filterContext.Result = routeData;
        }

        }
    }
