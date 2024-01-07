<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ApplicationBibliothequeProjet.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Login</title>
    <link rel="shortcut icon" href="LogoImg/logo.png" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!-- Bootstrap CSS -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Fontawesome CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg navbar-dark bg-primary">
            <a class="navbar-brand" href="default.aspx">
                <img src="LogoImg/logo.png" alt="logo" width="49" height="49" />My library
            </a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#collapsibleNavbar">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="collapsibleNavbar">
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item">
                        <a class="nav-link" href="default.aspx"><b>Home</b></a>
                    </li>

                    <li class="nav-item">
                        <a class="nav-link" href="SignUp.aspx"><b>Sign Up</b></a>
                    </li>
                </ul>
            </div>
        </nav>

        <div class="jumbotron text-center alert alert-primary" style="margin-bottom: 0">
            <h1>My library</h1>
        </div>

        <div class="container mt-4">
            <div class="row">
                <div class="col-md-6 mx-auto">
                    <div class="card">
                        <div class="card-body">
                            <img class="mx-auto d-block" width="200" src="LogoImg/login.jpg" alt="User Image" />
                            <h3 class="text-center">Member/User Login</h3>
                            <hr />

                            <div class="form-group">
                                <label for="txtMemberID">Member ID</label>
                                <asp:TextBox ID="txtMemberID" CssClass="form-control" placeholder="Member ID" runat="server"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="txtPassword">Password</label>
                                <asp:TextBox ID="txtPassword" CssClass="form-control" placeholder="Password" TextMode="Password" runat="server"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:Button ID="btnLogin" class="btn btn-info btn-block" runat="server" Text="Login" OnClick="btnLogin_Click" />
                            </div>

                            <div class="form-group text-center">
                                <a href="SignUp.aspx" class="btn btn-info btn-block">Sign Up</a>
                            </div>

                            <p class="text-center">
                                <a href="default.aspx"><< Back to Home screen</a>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Bootstrap JS, Popper JS, and jQuery JS -->
        <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    </form>
</body>

</html>
