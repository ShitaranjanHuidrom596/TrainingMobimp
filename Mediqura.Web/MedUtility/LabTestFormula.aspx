<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master"  AutoEventWireup="true" CodeBehind="LabTestFormula.aspx.cs"  Inherits="Mediqura.Web.MedUtility.LabTestFormula" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">

        function confirmClick() {
            return confirm("Are you sure to add this element?");
        }
    </script>
    <h2 class="breadcumb_cus">Add Test Parameter</h2>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="panel2" runat="server">
                <div class="custab-panel" id="panellabsubtestmaster">
                    <div class="fixeddiv">
                        <div class="row fixeddiv" id="divmsg1" runat="server">
                            <asp:Label ID="lblmessage" runat="server"></asp:Label>
                        </div>
                    </div>
                 
                    <div class="row">
                        <div class="col-sm-8">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan " runat="server">Profile Group </span>
                                        <asp:DropDownList ID="ddl_profilegroup" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_profilegroup_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan " runat="server">Add Formula For</span>
                                        <asp:DropDownList ID="ddl_testlistinsideprofile" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_testlistinsideprofile_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6" runat="server" visible="true">
                                    <div class="form-group input-group">
                                        <span id="Span14" class="input-group-addon cusspan" runat="server">Formula For </span>
                                        <asp:TextBox ID="txt_formulafortest" runat="server" onfocus="this.select();" Font-Italic="true" class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6" runat="server" visible="true">
                                    <div class="form-group input-group">
                                        <span id="Span15" class="input-group-addon cusspan" runat="server">Formula </span>
                                        <asp:TextBox ID="txt_formula" runat="server" onfocus="this.select();" Font-Italic="true" class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">                                        
                                        <asp:Button ID="btnadd" runat="server" Class="btn  btn-sm cusbtn" Text="Add"  OnClick="btnadd_Click" />
                                    </div>
                                </div>
                            </div>
                             
                        </div>
                        <div class="col-sm-4">
                            <div class="col-sm-12">
                                <h4>Select Test from the list to add for calculation</h4>
                                <div class="">
                                    
                                 <asp:ListBox ID="lbx_testlistinsideprofile" runat="server" AutoPostBack="true" Height="200px" Width="100%" SelectionMode="Single" OnSelectedIndexChanged="lbx_testlistinsideprofile_SelectedIndexChanged"></asp:ListBox>
                             <%-- <asp:CheckBoxList ID="chbx_testlistinsideprofile" runat="server"  AutoPostBack="true" SelectionMode="Single" OnSelectedIndexChanged="chbx_testlistinsideprofile_SelectedIndexChanged"></asp:CheckBoxList>--%>
                                </div>


                            </div>
                        </div>          
                         
                    </div> 
                    <div class="row">
                        <asp:GridView ID="GvFormula" runat="server" CssClass="table-hover grid_table result-table" EmptyDataText="No Record Found" PageSize="10" AllowPaging="true" AllowCustomPaging="true" AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center" >
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Sl.No
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                   <%# (Container.DataItemIndex+1)+(GvFormula.PageIndex)*GvFormula.PageSize %>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Left" Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Code
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_code" Style="text-align: left !important;" runat="server"
                                            Text='<%# Eval("TestCode") %>'></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Formula For
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_formulafor" Style="text-align: left !important;" runat="server"
                                            Text='<%# Eval("TestName") %>'></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                   <asp:TemplateField>
                                    <HeaderTemplate>
                                        Formula
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_formula" Style="text-align: left !important;" runat="server"
                                            Text='<%# Eval("Formula") %>'></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <span class="cus-Delete-header">Delete</span>

                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                            OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                    <i class="fa fa-trash-o cus-delete-color"></i>
                                        
                                        </asp:LinkButton>

                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                </asp:TemplateField>
                            </Columns>

                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />

                            <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Right" Height="1em" Width="2%" />
                        </asp:GridView>
                    </div>
                   
                </div>
              
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
