<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="ThreeTierTemplate.update" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
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
    <form id="form1" runat="server">
        <div id="main_container">
           <div class="form_container">
                <div class="form_field">
                    <p>Name:</p>
                    <asp:TextBox CssClass="txbox" ID="tbNamereg" runat="server"/>
                </div>
                 <div class="form_field">
                    <p>Address:</p>
                    <asp:TextBox CssClass="txbox" ID="tbAddressreg" runat="server"/>
                </div>
                 <div class="form_field">
                    <p>Email:</p>
                    <asp:TextBox CssClass="txbox" ID="tbEmailreg" runat="server"/>
                </div>
                 <div class="form_field">
                    <p>Phone No:</p>
                    <asp:TextBox CssClass="txbox" ID="tbPhonereg" runat="server"/>
                </div>
                 <div class="form_field">
                    <p>Sex:</p>
                     <div>
                         <asp:RadioButton ID="rdgen1reg" GroupName="gender" Text="M" runat="server" OnCheckedChanged="rdgen1reg_CheckedChanged" />
                         <asp:RadioButton GroupName="gender" Text="F" ID="rdgen2reg" runat="server" OnCheckedChanged="rdgen2reg_CheckedChanged" />
                     </div>
                </div>
                <div class="form_field">
                    <p>Age:</p>
                    <asp:TextBox CssClass="txbox" ID="tbAgereg" runat="server"/>
                    <asp:RegularExpressionValidator ID="revNumber" runat="server"
                    ControlToValidate="tbAgereg"
                    ErrorMessage="Please enter Number." ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                </div>
                 <div class="form_field">
                    <p>Registration Date:</p>
                    <asp:TextBox CssClass="txbox" ID="tbRegreg" TextMode="Date" runat="server"/>
                </div>
                <asp:Button ID="updateBtn" CssClass="buttons" Text="Update"  runat="server" OnClick="updateBtn_Click" />
            </div>
        </div>
    </form>
    <script>
        function updt(){
            alert("Update Successfully");
        }
    </script>
</body>
</html>
