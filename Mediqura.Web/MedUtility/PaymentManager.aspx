<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="PaymentManager.aspx.cs" Inherits="Mediqura.Web.MedUtility.PaymentManager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <h2 class="breadcumb_cus">Payment Manager</h2>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="custab-panel" id="panellabgroupmaster">
                <div class="fixeddiv">
                    <div class="row fixeddiv" id="divmsg1" runat="server">
                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group input-group">
                            <span id="Span10" class="input-group-addon cusspan" runat="server">Source Type</span>
                            <asp:DropDownList ID="ddl_source" AutoPostBack="true" OnSelectedIndexChanged="ddl_source_SelectedIndexChanged" runat="server" class="form-control input-sm col-sm custextbox">
                               <%-- <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">Walkin</asp:ListItem>
                                <asp:ListItem Value="2">Referal Hospital</asp:ListItem>
                                <asp:ListItem Value="3">Referal Doctor</asp:ListItem>--%>
                                <%-- <asp:ListItem Value="4">Referal Employee</asp:ListItem>--%>
                                <%--<asp:ListItem Value="5">Other Referals</asp:ListItem>--%>
                                <%--<asp:ListItem Value="6">Runners</asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                    </div>                   
                    <div class="col-sm-5">
                        <div class="form-group input-group">
                            <span id="Span4" class="input-group-addon cusspan" runat="server">Source Name <span
                                style="color: red">*</span></span>
                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                ID="txt_sourcename"  AutoPostBack="True" OnTextChanged="txt_sourcename_TextChanged" ></asp:TextBox>
                            <asp:AutoCompleteExtender ID="ACE_referal" runat="server"
                                ServiceMethod="Getautoreferals" MinimumPrefixLength="1"
                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_sourcename"
                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                            </asp:AutoCompleteExtender>
                        </div>
                    </div>
                    <div class="col-sm-4">
                            <div class="form-group input-group">
                                <span id="Span1" class="input-group-addon cusspan" runat="server">Address<span
                                    style="color: red">*</span></span>
                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                    ID="txt_referaladrress" AutoPostBack="True" MaxLength="10"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                       <div class="col-sm-8">
                             <asp:Button ID="btn_nopclist" runat="server"  Class="btn  btn-sm cusbtn" Text="NO PC List" OnClick="btn_nopclist_Click" />
                       </div>
                        <div class="col-sm-4">
                            <div class="form-group input-group cuspanelbtngrp">
                                <asp:Button ID="btn_Print" runat="server"  Class="btn  btn-sm cusbtn" Text="Print Pc List" OnClick="btn_Print_Click" />

                                <asp:Button ID="btn_reset" runat="server" UseSubmitBehavior="False" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btn_reset_Click" />
                            </div>
                         
                        </div>
                       
                    </div>
                    <div class="row cusrow pad-top ">
                         <div class="col-sm-4">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg3" runat="server">
                                    <asp:Label ID="lblresult" runat="server" Height="13px"></asp:Label>
                                    <asp:Label ID="lbl_totalrecords" Visible="false" runat="server" Height="13px"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div>
                                <div class="pbody">
                                    <div class="grid" style="float: left; width: 100%; height: 50vh; overflow: auto">
                                        <asp:GridView ID="GvPaymentDetails" runat="server" CssClass="table-hover grid_table result-table" OnPageIndexChanging="GvPaymentDetails_PageIndexChanging" AllowCustomPaging="true" AllowPaging="true" PageSize="20" EmptyDataText="No record found..." AutoGenerateColumns="False" Width="100%" 
                                            HorizontalAlign="Center" OnRowDataBound="GvPaymentDetails_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Sl.No
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1%>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Lab Group
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_groupid" Visible="false" runat="server" Text='<%# Eval("LabGroupID")%>'></asp:Label>
                                                        <asp:Label ID="lbl_groupname" Style="text-align: left !important;" runat="server" Text='<%# Eval("LabGroupName")%>'></asp:Label>
                                                         <asp:Label ID="lbl_referaltypeid" Visible="false"  runat="server" Text='<%# Eval("ReferalTypeID")%>'></asp:Label>
                                                         <asp:Label ID="lbl_referalid" Visible="false"  runat="server" Text='<%# Eval("ReferalID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="70%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        % Commission
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_commission" onfocus="this.select();" Style="text-align: left !important;"  runat="server" MaxLength="7" Text='<%# Eval("Commission", "{0:0#.##}")%>'></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender TargetControlID="txt_commission" ID="FilteredTextBoxExtender2"
                                                            runat="server" FilterType="Custom,Numbers"
                                                            FilterMode="ValidChars"
                                                            ValidChars=". " Enabled="True">
                                                        </asp:FilteredTextBoxExtender>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                            <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Right" Height="1em" Width="2%" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-9">

                        </div>
                        <div class="col-sm-2">
                            <div class="form-group input-group cuspanelbtngrp  pull-right">
                                <asp:Button ID="btn_update" runat="server" UseSubmitBehavior="False" Class="btn  btn-sm cusbtn" Text="Update" OnClick="btn_update_Click" />                             
                            </div>
                        </div>
                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
