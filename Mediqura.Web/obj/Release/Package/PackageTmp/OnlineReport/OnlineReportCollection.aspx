<%@ Page Title="" Language="C#" MasterPageFile="~/OnlineReportMst.Master" AutoEventWireup="true" CodeBehind="OnlineReportCollection.aspx.cs" Inherits="Mediqura.Web.OnlineReport.OnlineReportCollection" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="main-container">
                <div class="card_wrapper" id="div_search" runat="server">
                    <div class="row">
                        <div class="col-md-3 mt-5">
                            <asp:Label ID="Label8" Font-Bold="true" runat="server" Text="Collection Center"></asp:Label>
                            <asp:DropDownList ID="ddl_center" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 mt-5">
                            <asp:Label ID="lbl_Invno" Font-Bold="true" runat="server" Text="Inv No."></asp:Label>
                            <asp:TextBox ID="txt_InvNumber" placeholder="search.." AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6 mt-5">
                            <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="Patient Name"></asp:Label>
                            <asp:TextBox ID="txt_patientName" placeholder="search.." AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row ">
                        <div class="col-md-3 mt-5">
                            <asp:Label ID="Label3" Font-Bold="true" runat="server" Text="Date From"></asp:Label>
                            <asp:TextBox ID="txtdatefrom" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                TargetControlID="txtdatefrom" />
                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />
                        </div>
                        <div class="col-md-3 mt-5">
                            <asp:Label ID="Label4" Font-Bold="true" runat="server" Text="Date To"></asp:Label>
                            <asp:TextBox ID="txtDateto" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                TargetControlID="txtDateto" />
                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdateto" />
                        </div>
                        <div class="col-md-3 mt-5">
                            <asp:Label ID="lbl_totalrecords" Font-Bold="true" runat="server" Text="Show Entries"></asp:Label>
                            <asp:Label ID="lblshow" Font-Bold="true" runat="server" Text="Show Entries"></asp:Label>
                            <asp:DropDownList ID="ddl_show" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox">
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="20"> 20 </asp:ListItem>
                                <asp:ListItem Value="50"> 50 </asp:ListItem>
                                <asp:ListItem Value="100"> 100 </asp:ListItem>
                                <asp:ListItem Value="10000"> all</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group pull-right" style="margin-top: 1.8em; padding-left: 50px;">
                                <asp:Button ID="Button1" runat="server" class="btn btn-sm btn-success button" OnClientClick="return Validate();" Text="Search" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card_wrapper mt-5" id="div1" runat="server">
                    <asp:GridView ID="GvTestList" AllowPaging="true" AllowCustomPaging="true" EmptyDataText="No record found..." OnPageIndexChanging="GvTestList_PageIndexChanging"
                        CssClass="footable table-striped" AllowSorting="true" OnSorting="GvTestList_Sorting" OnRowCommand="GvTestList_RowCommand" runat="server" AutoGenerateColumns="false"
                        Style="width: 100%">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Sl.No
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# (Container.DataItemIndex+1)+(GvTestList.PageIndex)*GvTestList.PageSize %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Inv.No.
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lvl_LabInv" Visible="false" runat="server" Text='<%# Eval("InVnumber")%>'></asp:Label>
                                    <asp:Label ID="lblPID" Font-Underline="true" ForeColor="Red" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                    <asp:Label ID="lblHeaderID" Visible="false" runat="server" Text='<%# Eval("HeaderID")%>'></asp:Label>
                                    <asp:Label ID="lblLabGroup" Style="text-align: left !important;" runat="server" Visible="false"
                                        Text='<%# Eval("LabGrpID") %>'></asp:Label>
                                    <asp:Label ID="lbl_subgroup" Style="text-align: left !important;" runat="server" Visible="false"
                                        Text='<%# Eval("LabSubGrpID") %>'></asp:Label>
                                    <asp:Label ID="lblreporttypeid" Style="text-align: left !important;" runat="server" Visible="false"
                                        Text='<%# Eval("ReportTypeID") %>'></asp:Label>
                                    <asp:Label ID="lblID" Style="text-align: left !important;" runat="server" Visible="false"
                                        Text='<%# Eval("ID") %>'></asp:Label>
                                    <asp:Label ID="lbl_id" Style="text-align: left !important;" runat="server" Visible="false"
                                        Text='<%# Eval("LabID") %>'></asp:Label>
                                    <asp:Label ID="lbl_BillID" Style="text-align: left !important;" runat="server" Visible="false"
                                        Text='<%# Eval("BillID") %>'></asp:Label>
                                    <asp:Label ID="lblReportID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                    <asp:Label ID="lblIsReportPrinted" Visible="false" runat="server" Text='<%# Eval("isPrinted")%>'></asp:Label>
                                    <asp:Label ID="lbldeliverystatus" Visible="false" runat="server" Text='<%# Eval("DeliveryStatus")%>'></asp:Label>
                                    <asp:Label ID="lblLabTestID" Visible="false" runat="server" Text='<%# Eval("LabTestID") %>'></asp:Label>
                                    <asp:Label ID="lbl_invnumber" runat="server" Text='<%# Eval("InVnumber") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Patient details
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_PatUHID" Visible="false" runat="server" Text='<%# Eval("UHID")%>'></asp:Label>
                                    <asp:Label ID="lbl_PatientName" runat="server" Text='<%# Eval("PatientName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Test Name
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUHID" Visible="false" runat="server" Text='<%# Eval("UHID") %>'></asp:Label>
                                    <asp:Label ID="lbltemplate" Style="text-align: left !important;" runat="server" Visible="false"
                                        Text='<%# Eval("Template") %>'></asp:Label>
                                    <asp:Label ID="lblTestID" Style="text-align: left !important;" runat="server" Visible="false"
                                        Text='<%# Eval("TestID") %>'></asp:Label>
                                    <asp:Label ID="lblurgency" Visible="false" runat="server" Text='<%# Eval("Urgency")%>'></asp:Label>
                                    <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("TestName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Test On
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbltestOn" runat="server" Text='<%# Eval("TestDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Bill Status
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBillStatusID" Visible="false" runat="server" Text='<%# Eval("UHID")%>'></asp:Label>
                                    <asp:Label ID="lblBillStatus" runat="server" Text='<%# Eval("PageName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <HeaderTemplate>
                                    View
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkview" Visible="true" runat="server" CommandName="View" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"><span style="color:red">View</span></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Print
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIsVerified" Visible="false" runat="server" Text='<%# Eval("isVerified")%>'></asp:Label>
                                    <asp:Label ID="lblNotEntry" runat="server" Text='Not Verified'></asp:Label>
                                    <asp:LinkButton ID="lnkprint" Font-Underline="true" Visible="false" runat="server" CommandName="Print" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none" OnClientClick="btnConfirm();"><span style="color:red">Print</span></asp:LinkButton>
                                    <asp:LinkButton ID="lnkreprint" Font-Underline="true" Visible="false" runat="server" CommandName="Print" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none" OnClientClick="btnConfirm();"><span style="color:red">Delivered/Print</span></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    HPrint
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNotEntr2" runat="server" Text='Not Verified'></asp:Label>
                                    <asp:LinkButton ID="lnkEmailPrint" Font-Underline="true" Visible="false" runat="server" CommandName="EmailPrint" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none" OnClientClick="btnConfirm();"><span style="color:red">HPrint</span></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                            </asp:TemplateField>
                            <asp:ButtonField Visible="false" CommandName="Update" HeaderText="Update Receiving Time" ItemStyle-Width="1%" ButtonType="Button" Text="Update" />
                        </Columns>
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<" LastPageText=">" />
                        <PagerStyle CssClass="gridpager" HorizontalAlign="left" Height="1em" Width="2%" />
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:GridView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        $(function () {
            $('[id*=GvTestList]').footable();
        });
    </script>
</asp:Content>
