using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PTWWebsite.CustomMiddleware
{
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }
    }
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        readonly Claim _claim;

        public ClaimRequirementFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && (c.Value == _claim.Value || c.Value.Split(",").Contains(_claim.Value)));
            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
    }

    public class RefererFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Headers["Referer"].Count > 0)
            {
                if (filterContext.HttpContext.Request.Headers["Referer"].ToString() != null)
                {
                    if (filterContext.HttpContext.Request.Host.ToString() != (new Uri(filterContext.HttpContext.Request.Headers["Referer"].ToString())).Authority.ToString())
                    {

                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "404", controller = "Error" }));
                    }

                }

            }
            base.OnActionExecuting(filterContext);
        }
    }
}
