<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="ThreeTierTemplate.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main_container" runat="server">
          <div id="register_container">
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
                         <asp:RadioButton ID="rdgen1" GroupName="gender" Text="M" runat="server" />
                         <asp:RadioButton GroupName="gender" Text="F" ID="rdgen2" runat="server" />
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
                <asp:Button ID="regBtn" CssClass="buttons" OnClick="regBtn_Click" Text="Register" runat="server" />
            </div>
        </div>
</asp:Content>
