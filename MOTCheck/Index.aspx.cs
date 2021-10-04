using MOTCheck.Controller;
using System;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sTest;
        GovUKServiceAdaptor.GetMOTTests("AM03FXJ", out sTest);
        TestLabel.Text = sTest;
    }
}