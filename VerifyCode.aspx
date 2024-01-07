<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeBehind="VerifyCode.aspx.cs" Inherits="ApplicationBibliothequeProjet.VerifyCode" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Verify Code</title>
    <!-- Ajoutez vos balises head nécessaires ici -->
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Enter Verification Code</h2>
            <div>
                <label for="txtVerificationCode">Verification Code:</label>
                <asp:TextBox ID="txtVerificationCode" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <br />
            <div>
                <asp:Button ID="btnVerifyCode" runat="server" Text="Verify Code" OnClick="btnVerifyCode_Click" CssClass="btn btn-primary" />
            </div>
        </div>
    </form>
</body>
</html>
