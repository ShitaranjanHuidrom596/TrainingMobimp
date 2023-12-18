<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master"  AutoEventWireup="true" CodeBehind="LabSubTestMaster.aspx.cs"  Inherits="Mediqura.Web.MedUtility.LabSubTestMaster" %>

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
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span10" class="input-group-addon cusspan " runat="server">Group</span>
                                <asp:DropDownList ID="ddl_labgroup" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_labgroup_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span3" class="input-group-addon cusspan " runat="server">Sub-Group </span>
                                <asp:DropDownList runat="server" ID="ddl_labsubgroup" AutoPostBack="True" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_labsubgroup_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        
                        <div class="col-sm-3" runat="server">
                            <div class="form-group input-group">
                                <span id="Span12" class="input-group-addon cusspan " runat="server">Test Type  <span id="Span13"
                                    style="color: red" runat="server">*</span></span>
                                <asp:DropDownList runat="server" ID="ddl_testtype" AutoPostBack="True" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_testtype_SelectedIndexChanged">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">Single Test</asp:ListItem>
                                <asp:ListItem Value="2">Profile</asp:ListItem>
                                <asp:ListItem Value="3">Package</asp:ListItem>
                                </asp:DropDownList>
                              
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span2" class="input-group-addon cusspan mandbg" runat="server">Test <span
                                    style="color: red">*</span></span>
                                <asp:TextBox runat="server" ID="txt_test" onfocus="this.select();" AutoPostBack="true" OnTextChanged="txt_testname_TextChanged" class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                    ServiceMethod="GetServices" MinimumPrefixLength="1"
                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_test"
                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                </asp:AutoCompleteExtender>
                            </div>
                        </div>
                    </div>
                   
                    <div class="row" id="paramdiv" runat="server">
                        <div class="col-sm-3" runat="server" visible="true">
                            <div class="form-group input-group">
                                <span id="Span5" class="input-group-addon cusspan" runat="server">Parameter </span>
                                <asp:TextBox ID="txt_parameter" runat="server" onfocus="this.select();" placeholder="Searching ......." Font-Italic="true" class="form-control input-sm col-sm custextbox"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <asp:Button ID="btn_search" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btn_search_Click" />
                                <asp:Button ID="btnadd" runat="server" Class="btn  btn-sm cusbtn" Text="Add New Row" OnClick="btnadd_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row" id="elementdiv" visible="false" runat="server">
                        <div runat="server" id="addelementdiv">
                            <div class="col-sm-3" runat="server" visible="false">
                                <div class="form-group input-group">
                                    <span id="Span11" class="input-group-addon cusspan " runat="server">Parameter</span>
                                    <asp:DropDownList ID="ddl_ProfileParmlist" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_ProfileParmlist_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span4" class="input-group-addon cusspan" runat="server">Add Element </span>
                                    <asp:TextBox ID="txt_testelement" runat="server" OnTextChanged="txt_testelement_TextChanged" onfocus="this.select();" placeholder="" Font-Italic="true" class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="TestElementACE" runat="server"
                                        ServiceMethod="GetAllLabServicesbyTestType" MinimumPrefixLength="1"
                                        CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_testelement"
                                        UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </div>
                            <div class="col-sm-2" runat="server" visible="false">
                                <div class="form-group input-group">
                                    <span id="Span6" class="input-group-addon cusspan" runat="server">Order No</span>
                                    <asp:TextBox ID="txt_elementorderno" runat="server" onfocus="this.select();" placeholder="" Font-Italic="true" class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender TargetControlID="txt_elementorderno" ID="FilteredTextBoxExtenderYEAR"
                                        runat="server" ValidChars="0123456789Y"
                                        Enabled="True">
                                    </asp:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="col-sm-4" runat="server">
                                <div class="form-group input-group">
                                    <asp:Button ID="addelement_btn" runat="server" Class="btn  btn-sm cusbtn" Text="Add Element" OnClientClick="return confirm('Are you sure to add this element?');" OnClick="addelement_btn_Click" />
                                     <asp:Button ID="addrowprofile_btn" runat="server" Class="btn  btn-sm cusbtn" Text="Add S-Header" OnClick="addrowprofile_btn_Click" />
                                     <asp:Button ID="profile_searchbtn" Visible="true" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btn_search_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row cusrow pad-top ">
                        <div class="col-sm-12">
                            <div>
                                <div class="pbody">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="grid" style="float: left; width: 100%; height: 45vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="GvLabSubTest" runat="server" CssClass="table-hover grid_table result-table"
                                                    EmptyDataText="No record found..." OnRowDataBound="GvLabSubTest_RowDataBound" OnRowCommand="gviplabsubtestlist_RowCommand" AutoGenerateColumns="False"
                                                    Width="100%" HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Order
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ID" Visible="false" runat="server"
                                                                    Text='<%# Eval("ID") %>'></asp:Label>
                                                                <asp:TextBox ID="txt_order" onfocus="this.select();" Style="text-align: left !important;" MaxLength="5" Width="30px" runat="server" Text='<%# Eval("OrderNo")%>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_order" ID="FilteredTextBoxExtender4"
                                                                    runat="server" ValidChars="0123456789"
                                                                    Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Code
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_paramcode" onfocus="this.select();" Style="text-align: left !important;" runat="server" Width="50px"  Text='<%# Eval("ParamCode") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Parameter
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_subtest" onfocus="this.select();" Style="text-align: left !important;" runat="server" Width="150px" Text='<%# Eval("SubTestName")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                MachineName
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="rowlbl_unit" Visible="false" runat="server"
                                                                    Text='<%# Eval("UnitID") %>'></asp:Label>
                                                                <asp:Label ID="lbl_machineId" Visible="false" runat="server"
                                                                    Text='<%# Eval("MachineID") %>'></asp:Label>
                                                                <asp:DropDownList runat="server" ID="ddl_machinename" Width="65px"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Unit
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblunitID" Visible="false" runat="server"
                                                                    Text='<%# Eval("UnitID") %>'></asp:Label>
                                                                <asp:Label ID="lbl_mchineid" Visible="false" runat="server"
                                                                    Text='<%# Eval("MachineID") %>'></asp:Label>
                                                                <asp:DropDownList ID="ddl_unit" Width="65px" runat="server"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Sample
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_sampletypeID" Visible="false" runat="server"
                                                                    Text='<%# Eval("SampleTypeID") %>'></asp:Label>
                                                                <asp:Label ID="lbl_groupdID" Visible="false" runat="server"
                                                                    Text='<%# Eval("LabGroupID") %>'></asp:Label>
                                                                <asp:Label ID="lbl_subgroupID" Visible="false" runat="server"
                                                                    Text='<%# Eval("LabSubGroupID") %>'></asp:Label>
                                                                <asp:DropDownList ID="ddl_sample" Width="65px" runat="server"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Reagent
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_reagenttypeID" Visible="false" runat="server"
                                                                    Text='<%# Eval("ReagentTypeID") %>'></asp:Label>
                                                                <asp:DropDownList ID="ddl_reagent" Width="65px" runat="server"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Method
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmethodID" Visible="false" runat="server"
                                                                    Text='<%# Eval("MethodID") %>'></asp:Label>
                                                                <asp:DropDownList ID="ddl_method" Width="65px" runat="server"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Container
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcontainerID" Visible="false" runat="server"
                                                                    Text='<%# Eval("ContainerID") %>'></asp:Label>
                                                                <asp:DropDownList ID="ddl_containerID" Width="65px" runat="server"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Age (F) 
                                                               <p>(DD-MM-YY)</p>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_AgeFromtxt" Visible="false" runat="server" Text='<%# Eval("AgeRangeFrom") %>'></asp:Label>
                                                                <div style="width: 105px">
                                                                    <asp:TextBox ID="txt_day" MaxLength="3" Text='<%# Eval("DayFrom") %>' onfocus="this.select();" Style="text-align: left !important;" Width="30px" placeholder="DD" Font-Italic="true" runat="server"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txt_day" ID="FilteredTextBoxExtender2"
                                                                        runat="server" ValidChars="0123456789"
                                                                        Enabled="True">
                                                                    </asp:FilteredTextBoxExtender>


                                                                    <asp:TextBox ID="txt_month" MaxLength="3" Text='<%# Eval("MonthFrom") %>' onfocus="this.select();" Style="text-align: left !important;" Width="30px" placeholder="MM" Font-Italic="true" runat="server"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txt_month" ID="FilteredTextBoxExtender1"
                                                                        runat="server" ValidChars="0123456789"
                                                                        Enabled="True">
                                                                    </asp:FilteredTextBoxExtender>
                                                                    <asp:TextBox ID="txt_year" MaxLength="4" Text='<%# Eval("YearFrom") %>' onfocus="this.select();" Style="text-align: left !important;" Width="35px" placeholder="YYY" Font-Italic="true" runat="server"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txt_year" ID="FilteredTextBoxExtenderYEAR"
                                                                        runat="server" ValidChars="0123456789"
                                                                        Enabled="True">
                                                                    </asp:FilteredTextBoxExtender>

                                                                </div>


                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Age (T)
                                                                 <p>(DD-MM-YY)</p>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_AgeTotxt" Visible="false" runat="server" Text='<%# Eval("AgeRangeTo") %>'></asp:Label>
                                                                <div style="width: 105px">
                                                                    <asp:TextBox ID="txt_dayto" MaxLength="3" Text='<%# Eval("DayTo") %>' onfocus="this.select();" Style="text-align: left !important;" Width="30px" placeholder="DD" Font-Italic="true" runat="server"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txt_day" ID="FilteredTextBoxExtenderDAYTO"
                                                                        runat="server" ValidChars="0123456789"
                                                                        Enabled="True">
                                                                    </asp:FilteredTextBoxExtender>
                                                                    <asp:TextBox ID="txt_monthto" MaxLength="3" Text='<%# Eval("MonthTo") %>' onfocus="this.select();" Style="text-align: left !important;" Width="30px" placeholder="MM" Font-Italic="true" runat="server"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txt_month" ID="FilteredTextBoxExtenderMONTHTO"
                                                                        runat="server" ValidChars="0123456789"
                                                                        Enabled="True">
                                                                    </asp:FilteredTextBoxExtender>

                                                                    <asp:TextBox ID="txt_yearto" MaxLength="4" Text='<%# Eval("YearTo") %>' onfocus="this.select();" Style="text-align: left !important;" Width="35px" placeholder="YYY" Font-Italic="true" runat="server"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txt_yearto" ID="FilteredTextBoxExtenderYEARTO"
                                                                        runat="server" ValidChars="0123456789"
                                                                        Enabled="True">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </div>


                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                M-Nr  (F)
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_normalrangemale" onfocus="this.select();" Style="text-align: left !important;" Width="50px" runat="server" Text='<%# Eval("NormalRangeMaleFrom")%>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_normalrangemale" ID="mnFo_fbx"
                                                                    runat="server" ValidChars="0123456789."
                                                                    Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                M-Nr  (T)
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_normalrangemaleto" onfocus="this.select();" Style="text-align: left !important;" Width="50px" runat="server" Text='<%# Eval("NormalRangeMaleTo")%>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_normalrangemale" ID="mnTo_fbx"
                                                                    runat="server" ValidChars="0123456789."
                                                                    Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Range Wordings(M)
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_RangewordingM" Height="20px" TextMode="MultiLine" runat="server" Text='<%# Eval("RangeWordingM")%>' class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                F-Nr  (F)
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_normalrangefemale" onfocus="this.select();" Style="text-align: left !important;" Width="50px" runat="server" Text='<%# Eval("NormalRangeFeMaleFrom")%>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_normalrangefemale" ID="fnFo_fbx"
                                                                    runat="server" ValidChars="0123456789."
                                                                    Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                F-Nr  (T)
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_normalrangefemaleto" onfocus="this.select();" Style="text-align: left !important;" Width="50px" runat="server" Text='<%# Eval("NormalRangeFeMaleTo")%>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_normalrangefemaleto" ID="fnTo_fbx"
                                                                    runat="server" ValidChars="0123456789."
                                                                    Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <HeaderTemplate>
                                                                Tm-Nr  (F)
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_normalrangetransemalefrom" onfocus="this.select();" Style="text-align: left !important;" Width="50px" runat="server" Text='<%# Eval("NormalRangeTransMaleFrom")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <HeaderTemplate>
                                                                Tm-Nr  (T)
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox Visible="false" ID="txt_normalrangetransemaleto" onfocus="this.select();" Style="text-align: left !important;" Width="50px" runat="server" Text='<%# Eval("NormalRangeTransMaleTo")%>'></asp:TextBox>

                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <HeaderTemplate>
                                                                Tf-Nr  (F)
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_normalrangetransefemalefrom" onfocus="this.select();" Style="text-align: left !important;" Width="50px" runat="server" Text='<%# Eval("NormalRangeTransFeMaleFrom")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <HeaderTemplate>
                                                                Tf-Nr  (T)
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_normalrangetransefemaleto" onfocus="this.select();" Style="text-align: left !important;" Width="50px" runat="server" Text='<%# Eval("NormalRangeTransFeMaleTo")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Range Wordings(F)
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_RangewordingF" Height="20px" TextMode="MultiLine" runat="server" Text='<%# Eval("RangeWordingF")%>' class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Type
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_rowtypeID" Visible="false" runat="server"
                                                                    Text='<%# Eval("RowTypeID") %>'></asp:Label>
                                                                <asp:DropDownList ID="ddlrowtype" Width="70px" runat="server"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_defaultID" Visible="false" runat="server"
                                                                    Text='<%# Eval("defaultValue") %>'></asp:Label>
                                                                <asp:CheckBox ID="chekbox" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                    OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                    <i class="fa fa-trash-o cus-delete-color"></i>   </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>


                                                    </Columns>
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                    <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />
                                                </asp:GridView>

                                                <asp:GridView Visible="false" ID="GvProfile"  runat="server" CssClass="table-hover grid_table result-table"
                                                    EmptyDataText="No record found..." OnRowCommand="GvProfile_RowCommand" AutoGenerateColumns="False" OnRowDataBound="GvProfile_RowDataBound" 
                                                    Width="100%" HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Order
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ID" Visible="false" runat="server"
                                                                    Text='<%# Eval("ID") %>'></asp:Label>

                                                                <asp:TextBox ID="txt_order" onfocus="this.select();" Style="text-align: left !important;" MaxLength="5" Width="30px" runat="server" Text='<%# Eval("OrderNo")%>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_order" ID="FilteredTextBoxExtender4"
                                                                    runat="server" ValidChars="0123456789"
                                                                    Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Parameter Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_paramID" Visible="false" runat="server"
                                                                    Text='<%# Eval("ParamID") %>'></asp:Label>
                                                                <asp:TextBox ID="txt_paramname" Style="text-align: left !important;" runat="server" Width="300px"
                                                                    Text='<%# Eval("ParamName") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <HeaderTemplate>
                                                                Sample
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_paramsampletypeID" Visible="false" runat="server"
                                                                    Text='<%# Eval("SampleTypeID") %>'></asp:Label>
                                                                <asp:DropDownList ID="ddl_sample" Width="65px" runat="server"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Test Code
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_param" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ParamID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                          <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Type
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_rowtypeID_profile" Visible="false" runat="server"
                                                                    Text='<%# Eval("RowTypeID") %>'></asp:Label>
                                                                <asp:DropDownList ID="ddlrowtype_profile" Width="70px" runat="server"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="profileParmDelete" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                    OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                    <i class="fa fa-trash-o cus-delete-color"></i>   </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                    <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />
                                                </asp:GridView>
                                                </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div>
                            <p style="color:black;font-weight:600"  >Interpretation/Comments</p>
                            <asp:TextBox ID="txt_Reamrks" placeholder="Comments......" Height="70px" TextMode="MultiLine" runat="server" class="form-control input-sm col-sm custextbox"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group input-group">
                            <span id="Span9" class="input-group-addon cusspan" runat="server">Report Template</span>
                            <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                ID="ddl_template">
                            </asp:DropDownList>
                        </div>
                    </div>
                      <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span1" class="input-group-addon cusspan mandbg" runat="server">Machine Name<span
                                    style="color: red">* </span></span>
                                <asp:DropDownList runat="server" ID="ddl_machine" AutoPostBack="true"  class="form-control input-sm col-sm custextbox"></asp:DropDownList>
                            </div>
                        </div>

                    <div class="col-sm-3">
                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                            <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="False" Class="btn  btn-sm cusbtn" Text="Save" OnClick="Bulk_Update" />
                            <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
                        </div>
                    </div>

                    <div class="col-sm-3" style="margin-top: -1.2em;">
                        <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
                        <div class="form-group input-group cuspanelbtngrp drop-dwn">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlexport" Visible="false" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                        runat="server">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Excel"></asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnexport" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
