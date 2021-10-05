using MOTCheck;
using MOTCheck.Controller;
using MOTCheck.Model.GovUKService;
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
            CarModel carModel = GovUKServiceAdaptor.GetMotTest(RouteData.Values[AppConstants.ROUTE_DATA.REGISTRATION] as string, out sErrorMessage);

            if (carModel is null)
            {
                ErrorLabel.Text = sErrorMessage;
            }
            else
            {
                MakeLabel.Text = carModel.Make;
                ModelLabel.Text = carModel.Model;
                ColourLabel.Text = carModel.PrimaryColour;

                MotTestModel latestPassMotTestModel =
                    carModel.MotTests?
                    .Where(m => m.TestResult == "PASSED")
                    .OrderByDescending(m => m.CompletedDate)
                    .ThenByDescending(m => m.MotTestNumber)
                    .FirstOrDefault();

                if (latestPassMotTestModel != null) MotExpiryLabel.Text = latestPassMotTestModel.ExpiryDate?.ToShortDateString();

                MotTestModel latestMotTestModel =
                    carModel.MotTests?
                    .OrderByDescending(m => m.CompletedDate)
                    .ThenByDescending(m => m.MotTestNumber)
                    .FirstOrDefault();

                if (latestMotTestModel != null) LastMotMileageLabel.Text = latestMotTestModel.OdometerValue.ToString() + " " + latestMotTestModel.OdometerUnit;
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