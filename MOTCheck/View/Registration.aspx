<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="View_Registration" %>

<%@ Import Namespace="MOTCheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-gb">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%= AppConstants.APPLICATION_NAME + new string((RouteData.Values[AppConstants.ROUTE_DATA.REGISTRATION] as string)?.Prepend(' ')?.ToArray()) %></title>
    <style type="text/css">
        html {
            font-family: Arial, sans-serif;
            font-weight: 400;
            line-height: 1.5;
        }

        body {
            margin: 0;
            padding: 0;
            background-color: #DADCE0;
        }

        #form1 {
            display: table;
            width: 100vw;
            height: 100vh;
            text-align: center;
        }

        .container {
            display: table-cell;
            vertical-align: middle;
        }

            .container > div {
                display: inline-block;
                text-align: left;
            }

                .container > div > input[type=text] {
                    margin-top: 1rem;
                }

                .container > div > input[type=submit] {
                    margin-top: 1rem;
                    margin-bottom: 1rem;
                }
    </style>
    <script type="text/javascript">
        function validateUKRegistration() {
            let registrationTextBox = document.querySelector('[name$="UKRegistrationTextBox"]');
            let registrationRegExp = new RegExp('<%= UK_REGISTRATION_REGEX %>');
            let message = '';
            if (!registrationRegExp.test(registrationTextBox.value.replace(' ', '').toUpperCase())) {
                message = 'Please enter a valid UK registration.';
            }
            registrationTextBox.setCustomValidity(message);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <div class="container">
            <div>
                <div>
                    <asp:Label ID="ErrorLabel" runat="server" ForeColor="DarkRed"></asp:Label>
                </div>
                <asp:TextBox ID="UKRegistrationTextBox" runat="server" MaxLength="8" Columns="10" placeholder="UK Registration" BackColor="Yellow" Font-Size="XX-Large" Font-Bold="true" OnPreRender="UKRegistrationTextBox_PreRender"></asp:TextBox>
                <asp:Button ID="CheckRegistrationButton" runat="server" Text="Check" Font-Size="XX-Large" OnClientClick="validateUKRegistration();" OnClick="CheckRegistrationButton_Click" />
                <div>
                    <asp:Label ID="MakeLabel" runat="server"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="ModelLabel" runat="server"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="ColourLabel" runat="server"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="MotExpiryLabel" runat="server"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="LastMotMileageLabel" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
