using MOTCheck;
using MOTCheck.Controller;
using MOTCheck.Model;
using System;
using System.Linq;
using System.Web.Routing;

public partial class View_Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sRegistration = RouteData.Values[AppConstants.ROUTE_DATA.REGISTRATION] as string;

        if (!string.IsNullOrWhiteSpace(sRegistration))
        {
            string sErrorMessage;
            GovUKServiceCarModel govUKServiceCarModel = GovUKServiceAdaptor.GetMotTest(RouteData.Values[AppConstants.ROUTE_DATA.REGISTRATION] as string, out sErrorMessage);

            MakeLabel.Text = govUKServiceCarModel.Make;
            ModelLabel.Text = govUKServiceCarModel.Model;
            ColourLabel.Text = govUKServiceCarModel.PrimaryColour;

            GovUKServiceMotTestModel latestGovUKServiceMotTestModel = govUKServiceCarModel.MotTests.FirstOrDefault();

            if (latestGovUKServiceMotTestModel != null)
            {
                MotExpiryLabel.Text = latestGovUKServiceMotTestModel.ExpiryDate?.ToShortDateString();
                LastMotMileageLabel.Text = latestGovUKServiceMotTestModel.OdometerValue?.ToString() + " " + latestGovUKServiceMotTestModel.OdometerUnit;
            }
        }
    }

    private bool Valid(string a_sRegistration)
    {
        //TODO
        return true;
    }

    protected void CheckRegistrationButton_Click(object sender, EventArgs e)
    {
        string sRegistration = RegistrationTextBox.Text.Replace(" ", string.Empty);

        if (!Valid(sRegistration)) return;

        RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
        routeValueDictionary.Add(AppConstants.ROUTE_DATA.REGISTRATION, sRegistration);

        Response.Redirect(GetRouteUrl(AppConstants.ROUTE_NAME.REGISTRATION, routeValueDictionary));
    }
}