<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollectionCentreLogin.aspx.cs" Inherits="Mediqura.Web.CollectionCentreLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="mediqura lab login page">
    <meta name="author" content="MOBIMP">
    <title>Mediqura Lab</title>

    <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/style.css" rel="stylesheet" />

    <style>
        .testing {
            background: rgb(6, 135, 116);
            color: white;
            border-radius: 1em;
            padding: 1em;
            position: absolute;
            top: 50%;
            left: 50%;
            margin-right: -50%;
            width: 356px;
            height: 424px;
            transform: translate(-50%, -50%)
        }

        @media (max-width:700px) {
            .testing {
                width: 90%;
            }
        }
    </style>

</head>
<body class="login2background">
    <div class="testing">
        <form id="Form1" class="login" runat="server">
            <div class=" col-md-12" style="z-index: 22; border-radius: 8px;">
                <div class=" row">
                    <div class="col-md-12 text-center">
                        <img src="Images/logo.png" width="120" height="50" style="margin-top: 3%;" />
                    </div>
                </div>
                <br />
                <div class=" row ">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-map-marker" style="color: #fff;"></span>
                        </span>
                        <asp:DropDownList ID="ddl_center" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox">
                        </asp:DropDownList>
                    </div>
                    <br />
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-user" style="color: #fff;"></span>
                        </span>
                        <asp:TextBox runat="server" class="form-control" MaxLength="30" required="" ID="txtusername" oncopy="return false" onpaste="return false" autocomplete="off" placeholder="User Name"></asp:TextBox>

                    </div>
                    <br />
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-lock" style="color: #fff;"></span>
                        </span>
                        <asp:TextBox class="form-control" runat="server" MaxLength="30" required="" ID="txtpassword" oncopy="return false" onpaste="return false" autocomplete="off" TextMode="Password" placeholder="Password"></asp:TextBox>

                    </div>
                </div>
                <br />
                <div class="row " style="margin-top: 1em;">
                    <div class="col-md-12 text-center">
                        <asp:Button runat="server" ID="btnlogin" Text="Login" class="btn btn-primary" OnClick="btnlogin_Click" />
                    </div>
                    <div class="text-center col-md-12">
                        <div style="color: rgb(255, 255, 255); text-align: -webkit-center; background-color: #eb4336; font-size: .9em;">
                            <asp:Label ID="lblmessage" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <hr />
                <br />
                <div class=" row">
                    <div class="col-md-12 text-center">         
                          <a style="color: #FFFFFF" href="#">Powered By - Mobimp Services Pvt Ltd</a>
                    </div>
                </div>
                 <div class=" row">
                    <div class="col-md-12 text-center">         
                         <img src="assets/img/1.png" width="120" height="50" style="margin-top:1%; margin-bottom:1%" />
                    </div>
                </div>

            </div>
        </form>

    </div>

</body>
<script src="assets/assets/js/jquery.min.js"></script>
<script src="assets/bootstrap/js/bootstrap.min.js"></script>
</html>
