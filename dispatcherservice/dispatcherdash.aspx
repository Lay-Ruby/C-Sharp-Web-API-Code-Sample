<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dispatcherdash.aspx.cs" Inherits="dispatcherservice.dispatcherdash" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="region" DataValueField="region"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:efc_testConnectionString %>" SelectCommand="SELECT * FROM [operating_regions]"></asp:SqlDataSource>
            &nbsp;<asp:Button ID="Button1" runat="server" Text="Get Data" OnClick="Button1_Click" />
            <br />
            <br />
            <asp:Label ID="lblStatus" runat="server"></asp:Label>
            <br />
            <asp:GridView ID="grdFDE" runat="server" AllowSorting="True" AutoGenerateColumns="True" Width="50%"></asp:GridView>
        </div>
    </form>
</body>
</html>
