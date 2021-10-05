using System.Web.Routing;

namespace MOTCheck
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection aRouteCollection)
        {
            aRouteCollection.MapPageRoute(
                AppConstants.ROUTE_NAME.HOME,
                string.Empty,
                "~/View/Registration.aspx"
            );

            aRouteCollection.MapPageRoute(
                AppConstants.ROUTE_NAME.REGISTRATION,
                "{" + AppConstants.ROUTE_DATA.REGISTRATION + "}",
                "~/View/Registration.aspx"
            );
        }
    }
}