using MOTCheck;
using System;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl(AppConstants.ROUTE_NAME.HOME, null));
    }
}