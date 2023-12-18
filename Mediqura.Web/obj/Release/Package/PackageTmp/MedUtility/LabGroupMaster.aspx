<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="LabGroupMaster.aspx.cs" Inherits="Mediqura.Web.MedUtility.LabGroupMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        var Page
        function pageLoad() {

            Page = Sys.WebForms.PageRequestManager.getInstance()
            Page.add_initializeRequest(OnInitializeRequest)
        }
        function OnInitializeRequest(sender, args) {

            var postBackElement = args.get_postBackElement()

            if (Page.get_isInAsyncPostBack()) {
                ddl_department_SelectedIndexChanged
                alert('One request is already in progress....')
                args.set_cancel(true)
            }
        }
    </script>

    <h2 class="breadcumb_cus">Add Test Group</h2>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <contenttemplate>

            <asp:Panel ID="panel2" runat="server" DefaultButton="btnsave">
                <div class="custab-panel" id="panellabgroupmaster">
                    <div class="fixeddiv">
                        <div class="row fixeddiv" id="divmsg1" runat="server">
                            <asp:Label ID="lblmessage" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group input-group">
                                <span id="Span1" class="input-group-addon cusspan mandbg" runat="server">Code <span
                                    style="color: red">*</span></span>
                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="10"
                                    ID="txt_labgroupcode">
                                </asp:TextBox>
                                <asp:FilteredTextBoxExtender TargetControlID="txt_labgroupcode" ID="FilteredTextBoxExtender5"
                                    runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                    FilterMode="ValidChars"
                                    ValidChars=" " Enabled="True">
                                </asp:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group input-group">
                                <span id="Span2" class="input-group-addon cusspan mandbg" runat="server">Group Type <span
                                    style="color: red">*</span></span>
                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="50"
                                    ID="txt_labgrouptype">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group input-group">
                                <span id="Span6" class="input-group-addon cusspan" runat="server">Status</span>
                                <asp:DropDownList ID="ddlstatus" runat="server" class="form-control input-sm col-sm custextbox">
                                    <asp:ListItem Value="0">Active</asp:ListItem>
                                    <asp:ListItem Value="1">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span17" class="input-group-addon cusspan" runat="server">Show Entries </span>
                                <asp:DropDownList ID="ddl_show" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_show_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="10000">All</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group input-group cuspanelbtngrp  pull-right">
                                <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm scusbtn" Text="Add" OnClick="btnsave_Click" />
                                <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm scusbtn" Text="Search" OnClick="btnsearch_Click" />
                                <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm scusbtn" Text="Reset" OnClick="btnresets_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg3" runat="server">
                                    <asp:Label ID="lblresult" runat="server" Height="13px"></asp:Label>
                                <asp:Label ID="lbl_totalrecords" Visible="false" runat="server" Height="13px"></asp:Label>
                                    </div>
                            </div>
                        </div>
                    </div>
                    <div class="row cusrow pad-top ">
                        <div class="col-sm-12">
                            <div>
                                <div class="pbody">
                                    <div class="grid" style="float: left; width: 100%; height: 48vh; overflow: auto">
                                        <asp:UpdateProgress ID="updateProgress2" runat="server">
                                            <progresstemplate>
                                                <div id="DIVloading" class="text-center loading" runat="server">
                                                    <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                        AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                </div>
                                            </progresstemplate>
                                        </asp:UpdateProgress>
                                        <asp:GridView ID="GvLabGroupType" runat="server" CssClass="table-hover grid_table result-table"
                                            OnPageIndexChanging="GvLabGroupType_PageIndexChanging" AllowCustomPaging="true" AllowPaging="true" PageSize="10"
                                            EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowCommand="GvLabGroupType_RowCommand"
                                            Width="100%" HorizontalAlign="Center">
                                            <columns>
                                                <asp:TemplateField>
                                                    <headertemplate>
                                                        Sl.No
                                                    </headertemplate>
                                                    <itemtemplate>
                                                        <%# Container.DataItemIndex+1%>
                                                    </itemtemplate>
                                                    <itemstyle horizontalalign="Left" width="1%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <headertemplate>
                                                        Code
                                                    </headertemplate>
                                                    <itemtemplate>
                                                        <asp:Label ID="code" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                        <asp:Label ID="lbl_labgrouptypecode" Style="text-align: left !important;" runat="server"
                                                            Text='<%# Eval("LabGroupCode") %>'></asp:Label>
                                                    </itemtemplate>
                                                    <itemstyle horizontalalign="Left" width="1%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <headertemplate>
                                                        Group Type
                                                    </headertemplate>
                                                    <itemtemplate>
                                                        <asp:Label ID="lbl_labgrouptype" Style="text-align: left !important;" runat="server"
                                                            Text='<%# Eval("LabGroupType") %>'></asp:Label>
                                                    </itemtemplate>
                                                    <itemstyle horizontalalign="Left" width="1%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <headertemplate>
                                                        Added By
                                                    </headertemplate>
                                                    <itemtemplate>
                                                        <asp:Label ID="lbladdedBy" runat="server" Text='<%# Eval("EmpName")%>'></asp:Label>
                                                    </itemtemplate>
                                                    <itemstyle horizontalalign="Left" width="1%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <headertemplate>
                                                        Added On
                                                    </headertemplate>
                                                    <itemtemplate>
                                                        <asp:Label ID="lbladt" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                    </itemtemplate>
                                                    <itemstyle horizontalalign="Left" width="1%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <headertemplate>
                                                        Remarks
                                                    </headertemplate>
                                                    <itemtemplate>
                                                        <asp:TextBox ID="txtremarks" Width="170px" Height="18px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
                                                    </itemtemplate>
                                                    <itemstyle horizontalalign="Left" width="1%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <headertemplate>
                                                        <span class="cus-Edit-header">Edit</span>
                                                    </headertemplate>
                                                    <itemtemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" CommandName="Edits" ForeColor="Blue">
                                                            <i class="fa fa-pencil-square-o  cus-edit-color"></i>
                                                        </asp:LinkButton>
                                                    </itemtemplate>
                                                    <itemstyle horizontalalign="Left" width="1%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <headertemplate>
                                                        <span class="cus-Delete-header">Delete</span>
                                                    </headertemplate>
                                                    <itemtemplate>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                            OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                            <i class="fa fa-trash-o cus-delete-color"></i>
                                                        </asp:LinkButton>
                                                    </itemtemplate>
                                                    <itemstyle horizontalalign="Left" width="1%" />
                                                </asp:TemplateField>

                                            </columns>
                                            <pagersettings mode="NumericFirstLast" pagebuttoncount="5" firstpagetext="<<" lastpagetext=">>" />
                                            <pagerstyle backcolor="#CFEDE3" cssclass="gridpager" horizontalalign="right" height="1em" width="2%" />

                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-8">
                            <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
                            <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <contenttemplate>
                                        <asp:DropDownList ID="ddlexport" Visible="false" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                            runat="server">
                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Excel"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="PDF"></asp:ListItem>
                                        </asp:DropDownList>
                                    </contenttemplate>
                                    <triggers>
                                        <asp:PostBackTrigger ControlID="btnexport" />
                                    </triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </contenttemplate>
    </asp:UpdatePanel>
</asp:Content>
