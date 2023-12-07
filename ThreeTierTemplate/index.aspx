<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ThreeTierTemplate.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body{
            margin:0;
        }
        #nav{
            font-family:'Nirmala UI';
            font-weight:bold;
            background-color:lightgray;
            display:flex;
            width:100%;
            height:100px;
        }
        #nav div{
            margin:5px;
            font-size:1.2rem;
            cursor:pointer;
        }
        #nav a{
            text-decoration:none;
            color:black;
        }
        #main_container{
            display:flex;
            align-items:center;
            justify-content:center;
        }
        .form_container{
            font-family:'Nirmala UI';
            font-size:1.2rem;
            background-color:#27272a;
            border-radius:2px;
            /*box-shadow:gray 10px 10px 10px inset;*/
            box-shadow:10px 10px 6px black;
            padding:5px;
            margin:10px;
            margin-top:10px;
            width:500px;
            height:550px;
        }
        .form_field{
            color:white;
            display:grid;
            width:100%;
            grid-template-columns:1fr 2fr;
            align-items:center;
            justify-content:center;
        }
        .txbox{
            height:50%;
            border:none;
            border-radius:5px;
        }
        .buttons{
            font-weight:bold;
            font-size:2rem;
            background-color:turquoise;
            border-radius:4px;
            height:50px;
            width:100%;
            margin-top:30px;
        }
        #tb_roll{
            width:20%;
            height:30px;
        }
    </style>
</head>
<body>
    <div id="nav">
        <div><a href="index.aspx">Register</a></div>
        <div><a href="home.aspx">Home</a></div>
    </div>
    <form id="form1" runat="server">
        <div id="main_container">
            <div class="form_container">
                <div class="form_field">
                    <p>Name:</p>
                    <asp:TextBox CssClass="txbox" ID="tbName" runat="server" CausesValidation="False" MaxLength="35" TextMode="SingleLine" />
                </div>
                 <div class="form_field">
                    <p>Address:</p>
                    <asp:TextBox CssClass="txbox" ID="tbAddress" runat="server"/>
                </div>
                 <div class="form_field">
                    <p>Email:</p>
                    <asp:TextBox CssClass="txbox" ID="tbEmail" runat="server"/>
                </div>
                 <div class="form_field">
                    <p>Phone No:</p>
                    <asp:TextBox CssClass="txbox" ID="tbPhone" runat="server"/>
                </div>
                 <div class="form_field">
                    <p>Sex:</p>
                     <div>
                         <asp:RadioButton ID="rdgen1" GroupName="gender" Text="M" OnCheckedChanged="rdgen1_CheckedChanged" runat="server" />
                         <asp:RadioButton GroupName="gender" Text="F" ID="rdgen2" OnCheckedChanged="rdgen2_CheckedChanged" runat="server" />
                     </div>
                </div>
                <div class="form_field">
                    <p>Age:</p>
                    <asp:TextBox CssClass="txbox" ID="tbAge" runat="server"/>
                </div>
                 <div class="form_field">
                    <p>Registration Date:</p>
                    <asp:TextBox CssClass="txbox" ID="tbReg" TextMode="Date" runat="server"/>
                </div>
                <asp:Button ID="regBtn" CssClass="buttons" Text="Register" OnClick="regBtn_Click" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
