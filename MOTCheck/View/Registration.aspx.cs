using MOTCheck.Controller;
using MOTCheck.Model;
using System;

public partial class View_Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sErrorMessage;
        GovUKServiceCarModel govUKServiceCarModel = GovUKServiceAdaptor.GetMotTest("AM03FXJ", out sErrorMessage);
    }
}