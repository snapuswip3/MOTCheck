using MOTCheck;
using MOTCheck.Controller;
using MOTCheck.Extensions;
using MOTCheck.Model.GovUKService;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Routing;

public partial class View_Registration : System.Web.UI.Page
{
    protected string UK_REGISTRATION_REGEX = "(^[A-Z]{2}[0-9]{2}[A-Z]{3}$)|(^[A-Z][0-9]{1,3}[A-Z]{3}$)|(^[A-Z]{3}[0-9]{1,3}[A-Z]$)|(^[0-9]{1,4}[A-Z]{1,2}$)|(^[0-9]{1,3}[A-Z]{1,3}$)|(^[A-Z]{1,2}[0-9]{1,4}$)|(^[A-Z]{1,3}[0-9]{1,3}$)|(^[A-Z]{1,3}[0-9]{1,4}$)|(^[0-9]{3}[DX]{1}[0-9]{3}$)";

    private bool Valid(string a_sRegistration)
    {
        return Regex.IsMatch(a_sRegistration.ToUpper(), UK_REGISTRATION_REGEX);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string sRegistration = RouteData.Values[AppConstants.ROUTE_DATA.REGISTRATION] as string;

        if (!string.IsNullOrWhiteSpace(sRegistration))
        {
            if (!Valid(sRegistration)) Response.Redirect(GetRouteUrl(AppConstants.ROUTE_NAME.HOME, null));

            CarModel carModel = GovUKServiceAdaptor.GetMotTest(RouteData.Values[AppConstants.ROUTE_DATA.REGISTRATION] as string, out string sErrorMessage);

            if (carModel is null)
            {
                ErrorLabel.Text = "Error: ".TagWrap("b") + sErrorMessage;
            }
            else
            {
                MakeLabel.Text = "Make: ".TagWrap("b") + carModel.Make;
                ModelLabel.Text = "Model: ".TagWrap("b") + carModel.Model;
                ColourLabel.Text = "Colour: ".TagWrap("b") + carModel.PrimaryColour;

                MotTestModel latestPassMotTestModel =
                    carModel.MotTests?
                    .Where(m => m.TestResult == "PASSED")
                    .OrderByDescending(m => m.CompletedDate)
                    .ThenByDescending(m => m.MotTestNumber)
                    .FirstOrDefault();

                if (latestPassMotTestModel != null)
                {
                    MotExpiryLabel.Text = "MOT Expiry Date: ".TagWrap("b") + latestPassMotTestModel.ExpiryDate?.ToShortDateString();
                    if (latestPassMotTestModel.ExpiryDate < DateTime.Now.Date) MotExpiryLabel.Text += " (Expired)".TagWrap("i").TagWrap("b");
                }

                MotTestModel latestMotTestModel =
                    carModel.MotTests?
                    .OrderByDescending(m => m.CompletedDate)
                    .ThenByDescending(m => m.MotTestNumber)
                    .FirstOrDefault();

                if (latestMotTestModel != null) LastMotMileageLabel.Text = "Mileage At Last MOT: ".TagWrap("b") + latestMotTestModel.OdometerValue.ToString() + " " + latestMotTestModel.OdometerUnit;
            }
        }
    }

    protected void UKRegistrationTextBox_PreRender(object sender, EventArgs e)
    {
        UKRegistrationTextBox.Text = RouteData.Values[AppConstants.ROUTE_DATA.REGISTRATION] as string;
    }

    protected void CheckRegistrationButton_Click(object sender, EventArgs e)
    {
        string sRegistration = UKRegistrationTextBox.Text.Replace(" ", string.Empty);

        if (!Valid(sRegistration)) ErrorLabel.Text = "Error: ".TagWrap("b") + sRegistration + " is not a valid UK registration.";

        RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
        routeValueDictionary.Add(AppConstants.ROUTE_DATA.REGISTRATION, sRegistration);

        Response.Redirect(GetRouteUrl(AppConstants.ROUTE_NAME.REGISTRATION, routeValueDictionary));
    }
}