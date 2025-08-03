<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OAuthAspNet.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnGoogle" runat="server" Text="Google" OnClick="btnGoogle_Click" />
            <asp:Button ID="btnFacebook" runat="server" Text="Facebook" OnClick="btnFacebook_Click" />
            <asp:Button ID="btnLine" runat="server" Text="LINE" OnClick="btnLine_Click" />
            <asp:Button ID="btnAzure" runat="server" Text="Azure" OnClick="btnAzure_Click" />
            <asp:Button ID="btnAuth0" runat="server" Text="Auth0" OnClick="btnAuth0_Click" />
        </div>
    </form>
</body>
</html>
