<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAddress.aspx.cs" Inherits="ThreeTierTemplate.AddAddress" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Address:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="addrtb" runat="server" />
            <br />
            <asp:Button Text="Add Address" OnClick="Unnamed_Click" runat="server" />
        </div>
    </form>
</body>
</html>
