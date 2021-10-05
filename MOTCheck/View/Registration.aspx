<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="View_Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="RegistrationTextBox" runat="server" MaxLength="8"></asp:TextBox>
            <asp:Button ID="CheckRegistrationButton" runat="server" Text="Check" OnClick="CheckRegistrationButton_Click" />
            <asp:Label ID="MakeLabel" runat="server"></asp:Label>
            <asp:Label ID="ModelLabel" runat="server"></asp:Label>
            <asp:Label ID="ColourLabel" runat="server"></asp:Label>
            <asp:Label ID="MotExpiryLabel" runat="server"></asp:Label>
            <asp:Label ID="LastMotMileageLabel" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
