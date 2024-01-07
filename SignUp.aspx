 <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="ApplicationBibliothequeProjet.SignUp" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Sign Up</title>
    <meta charset="utf-8" />
    <link rel="shortcut icon" href="LogoImg/logo.png" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!-- Bootstrap CSS -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Fontawesome CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css" rel="stylesheet" />

    <!-- Bootstrap JS, Popper JS, and jQuery JS -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    <!-- SweetAlert -->
    <link href="SweetAlert/Styles/sweetalert.css" rel="stylesheet" />
    <script src="SweetAlert/Scripts/sweetalert.min.js"></script>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <nav class="navbar navbar-expand-sm navbar-dark bg-primary">
                <a class="navbar-brand" href="default.aspx">
                    <img src="LogoImg/logo.png" alt="logo" width="49" height="49" />My Library</a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#collapsibleNavbar">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="collapsibleNavbar">
                    <ul class="navbar-nav">
                        <li class="nav-item">
                            <a class="nav-link" href="default.aspx"><b>Home</b></a>
                        </li>
                        <!-- Add other navbar items as needed -->
                    </ul>
                </div>
                <!-- Navbar Right icon -->
                <div class="pmd-navbar-right-icon ml-auto">
                    <a class="btn btn-sm btn-primary" href="Login.aspx">Sign In</a>
                </div>
            </nav>

            <div class="jumbotron text-center alert alert-primary" style="margin-bottom: 0">
                <h1>My Library</h1>
            </div>
            <div class="container mt-3">
                <h2>Create New Account</h2>
                <ul class="nav nav-tabs">
                    <li class="nav-item">
                        <a class="nav-link active" data-toggle="tab" href="#signup">Sign Up</a>
                    </li>
                </ul>

                <div class="tab-content">
                    <div id="signup" class="container tab-pane active">
                        <br />
                        <h3>Sign Up</h3>
                        <div class="container">
                            <div class="row">
                                <div class="col-md-12 mx-auto">
                                    <div class="card">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col">
                                                    <center>
                                                        <img width="200" src="LogoImg/signin.jpg" />
                                                    </center>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col">
                                                    <center>
                                                        <h3>Member/User Sign Up</h3>
                                                    </center>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col">
                                                    <hr />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-4">
                                                    <label>Member ID</label>
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtMemberID" CssClass="form-control" placeholder="Member ID" runat="server"></asp:TextBox>
                                                    </div>

                                                    <label>Password</label>
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtPassword" CssClass="form-control" placeholder="Password" TextMode="Password" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*enter valid password" ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <label>Full Name</label>
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtFullName" CssClass="form-control" placeholder="Full Name" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*enter full name" ControlToValidate="txtFullName" Display="Dynamic" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <label>Date of Birth</label>
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtDOB" CssClass="form-control" placeholder="DOB" TextMode="Date" runat="server"></asp:TextBox>
                                                    </div>

                                                    <label>Contact No.</label>
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtContactNO" CssClass="form-control" placeholder="Contact No." runat="server"></asp:TextBox>
                                                    </div>
                                                    <label>EmailID</label>
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtEmail" CssClass="form-control" placeholder="Email" TextMode="Email" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="enter valid email addresss" ControlToValidate="txtEmail" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="enter valid email address" ControlToValidate="txtEmail" Display="Dynamic" ForeColor="#CC0099" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <label>Country</label>
                                                    <div class="form-group">
                                                                    <asp:DropDownList ID="ddlState" CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                            <asp:ListItem Text="Afghanistan" Value="Afghanistan" />
                                                            <asp:ListItem Text="Albania" Value="Albania" />
                                                            <asp:ListItem Text="United States" Value="United States" />
                                                            <asp:ListItem Text="Uruguay" Value="Uruguay" />
                                                            <asp:ListItem Text="Uzbekistan" Value="Uzbekistan" />
                                                            <asp:ListItem Text="Vanuatu" Value="Vanuatu" />
                                                            <asp:ListItem Text="Vatican City" Value="Vatican City" />
                                                            <asp:ListItem Text="Venezuela" Value="Venezuela" />
                                                            <asp:ListItem Text="Vietnam" Value="Vietnam" />
                                                            <asp:ListItem Text="Yemen" Value="Yemen" />
                                                            <asp:ListItem Text="Zambia" Value="Zambia" />
                                                            <asp:ListItem Text="Zimbabwe" Value="Zimbabwe" />
                                                        </asp:DropDownList>
                                                    </div>

                                                    <label>City</label>
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtCity" CssClass="form-control" placeholder="City" runat="server"></asp:TextBox>
                                                    </div>
                                                    <label>Postal PIN</label>
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtPIN" CssClass="form-control" placeholder="Postal PIN" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-12">
                                                    <label>Full Address</label>
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtAddress" CssClass="form-control" placeholder="Address" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-3">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnSignup" class="btn btn-info btn-block" runat="server" Text="Sign Up" OnClick="btnSignup_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <a href="default.aspx"><< Back to Home screen</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>

</html>


