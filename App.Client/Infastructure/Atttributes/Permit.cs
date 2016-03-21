using System.Web.Mvc;
using System.Web.Routing;

namespace App.Client.Infastructure.Atttributes
{
    public class Permit : AuthorizeAttribute
    {
        /// <summary>
        /// Custom authorization attribute.
        /// Add [Permit] to a controller to only allow authenticated users access.
        /// Add [Permit(Roles = "Some Role")] to only allow access to users of a particular role.
        /// </summary>
        /// <param name="ctx"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext ctx)
        {
            if (!ctx.HttpContext.User.Identity.IsAuthenticated)
            {
                ctx.Result = new RedirectToRouteResult(new
                RouteValueDictionary(new
                {
                    action = "Login",
                    controller = "Account",
                    area = ""
                }));
            }
            
        }
    }
}
