using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;
using Mediqura.Utility;


namespace Mediqura.Web.MedUtility
{
    public partial class LabSubTestMaster : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                ddlbind();
                supplementoryvalues();

            }
        }

        private void makegridviewempty()
        {
            txt_test.Text = "";
            Session["LabSubtestlist"] = null;
            GvLabSubTest.DataSource = Session["LabSubtestlist"];
            GvLabSubTest.DataBind();
            GvLabSubTest.Visible = false;


            Session["ProfileParamList"] = null;
            GvProfile.DataSource = Session["ProfileParamList"];
            GvProfile.DataBind();
            GvProfile.Visible = false;
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_labgroup, mstlookup.GetLookupsList(LookupName.LabGroups));
            Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(Convert.ToInt32(ddl_labgroup.SelectedValue)));
            txt_parameter.Text = "";
            Commonfunction.PopulateDdl(ddl_machine, mstlookup.GetLookupsList(LookupName.MachineName));       
            Commonfunction.PopulateDdl(ddl_template, mstlookup.GetLookupsList(LookupName.LabTemplate));
        }
        protected void supplementoryvalues()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Session["ProfileParamList"] = null;
            Session["LabSubtestlist"] = null;
            Session["machinelist"] = null;
            Session["unitlist"] = null;
            Session["methodlist"] = null;
            Session["reagentlist"] = null;
            Session["samplelist"] = null;
            Session["rowtypelist"] = null;
            Session["containerlist"] = null;

            List<LookupItem> machinelist = Session["machinelist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["machinelist"];
            Session["machinelist"] = mstlookup.GetLookupsList(LookupName.MachineName);
            List<LookupItem> unitlist = Session["unitlist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["unitlist"];
            Session["unitlist"] = mstlookup.GetLookupsList(LookupName.Unit);
            List<LookupItem> methodlist = Session["methodlist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["methodlist"];
            Session["methodlist"] = mstlookup.GetLookupsList(LookupName.Method);
            List<LookupItem> reagentlist = Session["reagentlist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["reagentlist"];
            Session["reagentlist"] = mstlookup.GetLookupsList(LookupName.Reagent);
            List<LookupItem> samplelist = Session["samplelist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["samplelist"];
            Session["samplelist"] = mstlookup.GetLookupsList(LookupName.SmpleType);
            List<LookupItem> rowtypelist = Session["rowtypelist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["rowtypelist"];
            Session["rowtypelist"] = mstlookup.GetLookupsList(LookupName.RowType);
            List<LookupItem> containerlist = Session["containerlist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["containerlist"];
            Session["containerlist"] = mstlookup.GetLookupsList(LookupName.Container);

        }
        public static int x = 0;
        protected void ddl_labgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            makegridviewempty();
            if (ddl_labgroup.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(Convert.ToInt32(ddl_labgroup.SelectedValue)));
                txt_parameter.Text = "";
                x = Convert.ToInt32(ddl_labgroup.SelectedValue == "" ? "0" : ddl_labgroup.SelectedValue);
            }
        }
        public static int y = 0;
  
        protected void ddl_labsubgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            makegridviewempty();
            MasterLookupBO mstlookup = new MasterLookupBO();         
            y = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "" ? "0" : ddl_labsubgroup.SelectedValue);
           
        }
        public static int z = 0;
        protected void ddl_testtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            makegridviewempty();
            txt_test.Text = "";
            z = Convert.ToInt32(ddl_testtype.SelectedValue == "" ? "0" : ddl_testtype.SelectedValue);

            if (ddl_testtype.SelectedValue == "1")
            {
                elementdiv.Visible = false;
                paramdiv.Visible = true;
                GvLabSubTest.Visible = true;
                GvProfile.Visible = false;


            }
            else
            {
                elementdiv.Visible = true;
                paramdiv.Visible = false;
                GvLabSubTest.Visible = false;
                GvProfile.Visible = true;

            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetServices(string prefixText, int count, string contextKey)
        {
            ServicesData Objpaic = new ServicesData();
            ServiceBO objInfoBO = new ServiceBO();
            List<ServicesData> getResult = new List<ServicesData>();
            Objpaic.ServiceName = prefixText;
            Objpaic.ServiceTypeID = x;
            Objpaic.SubServiceTypeID = y;
            Objpaic.TestTypeID = z;
            getResult = objInfoBO.GetCenterwisetestservices(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetAllLabServicesbyTestType(string prefixText, int count, string contextKey)
        {
            ServicesData Objpaic = new ServicesData();
            ServiceBO objInfoBO = new ServiceBO();
            List<ServicesData> getResult = new List<ServicesData>();
            Objpaic.ServiceName = prefixText;
            getResult = objInfoBO.GetAllLabServicesbyTestType(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceName.ToString());
            }
            return list;
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetLabServices(string prefixText, int count, string contextKey)
        {
            LabServiceMasterData Objpaic = new LabServiceMasterData();
            LabServiceMasterBO objInfoBO = new LabServiceMasterBO();
            List<LabServiceMasterData> getResult = new List<LabServiceMasterData>();
            Objpaic.TestName = prefixText;
            Objpaic.LabSubGroupID = Convert.ToInt32(contextKey == "" ? "0" : contextKey);
            getResult = objInfoBO.GetLabServicesByserviceTypeID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].TestName.ToString());
            }
            return list;
        }
        protected void txt_test_TextChanged(object sender, EventArgs e)
        {
            bindgrid();
        }

        protected void txt_testname_TextChanged(object sender, EventArgs e)
        {
            bindgrid();
        }

        public void cleardatasource()
        {
            GvLabSubTest.DataSource = null;
            GvLabSubTest.DataBind();
            GvProfile.DataSource = null;
            GvProfile.DataBind();
        }
        protected void bindgrid()
        {
            try
            {
                if (Commonfunction.SemicolonSeparation_String_32(txt_test.Text) == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "TestName", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_test.Text = "";
                    txt_test.Focus();
                    cleardatasource();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                
                ddl_machine.SelectedIndex = 0;
                List<LabServiceMasterData> objsubtestlist = GetLabSubtestlist(0, Commonfunction.SemicolonSeparation_String_32(txt_test.Text));
                if (objsubtestlist.Count > 0)
                {
                    int TestType = Convert.ToInt32(ddl_testtype.SelectedValue == "" ? "0" : ddl_testtype.SelectedValue);
                    if (TestType==1)
                    {
                       
                        Session["LabSubtestlist"] = null;
                        Session["LabSubtestlist"] = objsubtestlist;
                      
                        GvLabSubTest.DataSource = Session["LabSubtestlist"];
                        GvLabSubTest.DataBind();
                        GvLabSubTest.Visible = true;
                        ddlexport.Visible = true;
                        btnexport.Visible = true;
                    }
                    else
                    {
                        Session["ProfileParamList"] = objsubtestlist;
                        GvProfile.DataSource = objsubtestlist;
                        GvProfile.DataBind();
                        GvProfile.Visible = true;
                        ddlexport.Visible = true;
                        btnexport.Visible = true;
                    }
                    ddl_machine.SelectedValue = objsubtestlist[0].TestDoneMachineID.ToString();
                    ddl_template.SelectedValue = objsubtestlist[0].TemplateType.ToString();
                    txt_Reamrks.Text = objsubtestlist[0].Remarks.ToString();

                }
                else
                {

                        Session["LabSubtestlist"] = null;
                        GvLabSubTest.DataSource = Session["LabSubtestlist"];
                        GvLabSubTest.DataBind();
                        GvProfile.Visible = false;


                        Session["ProfileParamList"] = null;
                        GvProfile.DataSource = Session["ProfileParamList"];
                        GvProfile.DataBind();
                        GvProfile.Visible = false;

                        txt_Reamrks.Text = "";
                        ddl_template.SelectedIndex = 0;
                        ddl_machine.SelectedIndex = 0;
                        ddlexport.Visible = false;
                        btnexport.Visible = false;

                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void GvLabSubTest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            List<LookupItem> machinelist = Session["machinelist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["machinelist"];
            List<LookupItem> unitlist = Session["unitlist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["unitlist"];
            List<LookupItem> methodlist = Session["methodlist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["methodlist"];
            List<LookupItem> reagentlist = Session["reagentlist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["reagentlist"];
            List<LookupItem> samplelist = Session["samplelist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["samplelist"];
            List<LookupItem> rowtypelist = Session["rowtypelist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["rowtypelist"];
            List<LookupItem> containerlist = Session["containerlist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["containerlist"];

            foreach (GridViewRow row in GvLabSubTest.Rows)
            {
                try
                {
                    DropDownList row_ddlmachine = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[1].FindControl("ddl_machinename");
                    DropDownList ddl1 = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[2].FindControl("ddl_unit");
                    DropDownList ddl2 = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[3].FindControl("ddl_sample");
                    DropDownList ddl3 = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[3].FindControl("ddlrowtype");
                    DropDownList ddl4 = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[3].FindControl("ddl_method");
                    DropDownList ddl5 = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[3].FindControl("ddl_reagent");
                    DropDownList ddl6 = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[3].FindControl("ddl_containerID");

                    Label MachineID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[1].FindControl("lbl_machineId");
                    Label UnitID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[2].FindControl("lblunitID");
                    Label SampleID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_sampletypeID");
                    Label RowTypeID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_rowtypeID");
                    Label MethodID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lblmethodID");
                    Label ReagentID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_reagenttypeID");
                    Label ContainerID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lblcontainerID");
                    Label Defaultchek = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_defaultID");

                    Label AgeFromtxt = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[9].FindControl("lbl_AgeFromtxt");
                    Label AgeTotxt = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[10].FindControl("lbl_AgeTotxt");

                    TextBox YearFromtxt = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[9].FindControl("txt_year");
                    TextBox MonthFromtxt = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[9].FindControl("txt_month");
                    TextBox DayFromtxt = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[9].FindControl("txt_day");


                    TextBox YearTotxt = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[10].FindControl("txt_yearto");
                    TextBox MonthTotxt = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[10].FindControl("txt_monthto");
                    TextBox DayTotxt = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[10].FindControl("txt_dayto");




                    CheckBox chk = (CheckBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("chekbox");

                    if (Defaultchek.Text == "1")
                    {
                        chk.Checked = true;
                    }
                    else
                    {
                        chk.Checked = false;
                    }

                    Commonfunction.PopulateDdl(row_ddlmachine, machinelist);
                    Commonfunction.PopulateDdl(ddl1, unitlist);
                    Commonfunction.PopulateDdl(ddl2, samplelist);
                    Commonfunction.PopulateDdl(ddl3, rowtypelist);
                    Commonfunction.PopulateDdl(ddl4, methodlist);
                    Commonfunction.PopulateDdl(ddl5, reagentlist);
                    Commonfunction.PopulateDdl(ddl6, containerlist);

                    if (MachineID.Text != "0")
                    {
                        row_ddlmachine.Items.FindByValue(MachineID.Text).Selected = true;
                    }

                    if (UnitID.Text != "0")
                    {
                        ddl1.Items.FindByValue(UnitID.Text).Selected = true;
                    }
                    if (UnitID.Text != "0")
                    {
                        ddl1.Items.FindByValue(UnitID.Text).Selected = true;
                    }
                    if (SampleID.Text != "0")
                    {
                        ddl2.Items.FindByValue(SampleID.Text).Selected = true;
                    }
                    if (RowTypeID.Text != "0")
                    {
                        ddl3.Items.FindByValue(RowTypeID.Text).Selected = true;
                    }
                    if (MethodID.Text != "0")
                    {
                        ddl4.Items.FindByValue(MethodID.Text).Selected = true;
                    }
                    if (ReagentID.Text != "0")
                    {
                        ddl5.Items.FindByValue(ReagentID.Text).Selected = true;
                    }
                    if (ContainerID.Text != "0")
                    {
                        ddl6.Items.FindByValue(ContainerID.Text).Selected = true;
                    }

                    


                }
                catch (Exception ex)
                {
                    PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                    LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                }
            }

        }
        //protected void btnsave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (LogData.SaveEnable == 0)
        //        {
        //            Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
        //            divmsg1.Visible = true;
        //            divmsg1.Attributes["class"] = "FailAlert";
        //            return;
        //        }
        //        else
        //        {
        //            lblmessage.Visible = false;
        //        }
        //        if (ddl_labgroup.SelectedIndex == 0)
        //        {
        //            Messagealert_.ShowMessage(lblmessage, "Group", 0);
        //            divmsg1.Visible = true;
        //            divmsg1.Attributes["class"] = "FailAlert";

        //            ddl_labgroup.Focus();
        //            return;
        //        }
        //        else
        //        {
        //            lblmessage.Visible = false;
        //        }
        //        if (ddl_labsubgroup.SelectedIndex == 0)
        //        {
        //            Messagealert_.ShowMessage(lblmessage, "Subgroup", 0);
        //            divmsg1.Visible = true;
        //            divmsg1.Attributes["class"] = "FailAlert";

        //            ddl_labgroup.Focus();
        //            return;
        //        }
        //        else
        //        {
        //            lblmessage.Visible = false;
        //        }
        //        if (ddl_machine.SelectedIndex == 0)
        //        {
        //            Messagealert_.ShowMessage(lblmessage, "MachineName", 0);
        //            divmsg1.Visible = true;
        //            divmsg1.Attributes["class"] = "FailAlert";

        //            ddl_machine.Focus();
        //            return;
        //        }
        //        else
        //        {
        //            lblmessage.Visible = false;
        //        }
        //        List<LabServiceMasterData> List = new List<LabServiceMasterData>();
        //        LabServiceMasterData objlabserviceData = new LabServiceMasterData();
        //        LabServiceMasterBO objlabserviceBO = new LabServiceMasterBO();
        //        foreach (GridViewRow row in GvLabSubTest.Rows)
        //        {
        //            TextBox order = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_order");
        //            Label ID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
        //            DropDownList unit = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_unit");
        //            DropDownList sample = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_sample");
        //            DropDownList reagent = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_reagent");
        //            DropDownList method = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_method");
        //            DropDownList ContainerID = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_containerID");

        //            TextBox Description = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_subtest");
        //            TextBox agefrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_age");
        //            TextBox ageto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_ageto");
        //            TextBox MNrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangemale");
        //            TextBox MNrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangemaleto");
        //            TextBox FNrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangefemale");
        //            TextBox FNrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangefemaleto");
        //            TextBox Tm_Nrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransemalefrom");
        //            TextBox Tm_Nrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransemaleto");
        //            TextBox Tf_Nrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransefemalefrom");
        //            TextBox Tf_Nrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransefemaleto");
        //            CheckBox chk = (CheckBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("chekbox");
        //            DropDownList rowtype = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddlrowtype");

        //            LabServiceMasterData obj1 = new LabServiceMasterData();

        //            obj1.UnitID = Convert.ToInt32(unit.SelectedValue == "" ? "0" : unit.SelectedValue);
        //            obj1.OrderNo = Convert.ToInt32(order.Text == "" ? "0" : order.Text);
        //            obj1.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
        //            obj1.SampleTypeID = Convert.ToInt32(sample.SelectedValue == "" ? "0" : sample.SelectedValue);
        //            obj1.ReagentTypeID = Convert.ToInt32(reagent.SelectedValue == "" ? "0" : reagent.SelectedValue);
        //            obj1.ContainerID = Convert.ToInt32(ContainerID.SelectedValue == "" ? "0" : ContainerID.SelectedValue);
        //            obj1.MethodID = Convert.ToInt32(method.SelectedValue == "" ? "0" : method.SelectedValue);
        //            obj1.defaultValue = chk.Checked ? 1 : 0;
        //            obj1.SubTestName = Description.Text.Trim();
        //            obj1.AgeRangeFrom = agefrom.Text.Trim();
        //            obj1.AgeRangeTo = ageto.Text.Trim();
        //            obj1.NormalRangeMaleFrom = MNrfrom.Text.Trim();
        //            obj1.NormalRangeMaleTo = MNrto.Text.Trim();
        //            obj1.NormalRangeFeMaleFrom = FNrfrom.Text.Trim();
        //            obj1.NormalRangeFeMaleTo = FNrto.Text.Trim();
        //            obj1.NormalRangeTransFeMaleFrom = Tf_Nrfrom.Text.Trim();
        //            obj1.NormalRangeTransFeMaleTo = Tf_Nrto.Text.Trim();
        //            obj1.NormalRangeTransMaleFrom = Tm_Nrfrom.Text.Trim();
        //            obj1.NormalRangeTransMaleTo = Tm_Nrto.Text.Trim();
        //            obj1.RowTypeID = Convert.ToInt32(rowtype.SelectedValue == "" ? "0" : rowtype.SelectedValue);
        //            List.Add(obj1);
        //        }
        //        objlabserviceData.XMLData = XmlConvertor.LabSubTestRecordDatatoXML(List).ToString();
        //        objlabserviceData.LabGroupID = Convert.ToInt32(ddl_labgroup.SelectedValue == "" ? "0" : ddl_labgroup.SelectedValue);
        //        objlabserviceData.LabSubGroupID = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "" ? "0" : ddl_labsubgroup.SelectedValue);
        //        objlabserviceData.TestID = Convert.ToInt32(ddl_test.SelectedValue == "" ? "0" : ddl_test.SelectedValue);
        //        objlabserviceData.MachineID = Convert.ToInt32(ddl_machine.SelectedValue == "" ? "0" : ddl_machine.SelectedValue);
        //        objlabserviceData.Remarks = txt_Reamrks.Text.Trim();
        //        objlabserviceData.EmployeeID = LogData.EmployeeID;
        //        objlabserviceData.FinancialYearID = LogData.FinancialYearID;
        //        objlabserviceData.IPaddress = LogData.IPaddress;
        //        objlabserviceData.HospitalID = LogData.HospitalID;
        //        objlabserviceData.ActionType = Enumaction.Insert;

        //        int result = objlabserviceBO.UpdateLabSubTest(objlabserviceData);
        //        if (result == 1)
        //        {
        //            bindgrid();
        //            Messagealert_.ShowMessage(lblmessage, "save", 1);
        //            supplementoryvalues();
        //            divmsg1.Visible = true;
        //            divmsg1.Attributes["class"] = "SucessAlert";

        //        }
        //        else
        //        {
        //            Messagealert_.ShowMessage(lblmessage, "system", 0);
        //            divmsg1.Visible = true;
        //            divmsg1.Attributes["class"] = "FailAlert";
        //        }

        //    }
        //    catch (Exception ex) //Exception in agent layer itself
        //    {
        //        PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
        //        LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
        //        Messagealert_.ShowMessage(lblmessage, "system", 0);

        //    }
        //}
        protected void Bulk_Update(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[47] {
                 new DataColumn("PID", typeof(Int32)),
                 new DataColumn("OrderNo", typeof(Int32)),
                 new DataColumn("LabGroupID", typeof(Int32)),
                 new DataColumn("LabSubGroupID", typeof(Int32)),
                 new DataColumn("TestID", typeof(Int32)),
                 new DataColumn("ParamID", typeof(Int32)),
                 new DataColumn("ParamCode", typeof(string)),
                 new DataColumn("SubTestName", typeof(string)),
                 new DataColumn("Unit", typeof(Int32)),
                 new DataColumn("Sample", typeof(Int32)),
                 new DataColumn("Machine", typeof(Int32)),
                 new DataColumn("Method", typeof(Int32)),
                 new DataColumn("Reagent", typeof(Int32)),
                 new DataColumn("ContainerID", typeof(Int32)),
                 new DataColumn("AgeFrom", typeof(string)),
                 new DataColumn("AgeTo", typeof(string)),
                 new DataColumn("YearFrom", typeof(string)),
                 new DataColumn("MonthFrom", typeof(string)),
                 new DataColumn("DayFrom", typeof(string)),
                 new DataColumn("YearTo", typeof(string)),
                 new DataColumn("MonthTo", typeof(string)),
                 new DataColumn("DayTo", typeof(string)),
                 new DataColumn("AgeFromD", typeof(Int32)),
                 new DataColumn("AgeToD", typeof(Int32)),
                 new DataColumn("NormalRangeMaleFrom", typeof(string)),
                 new DataColumn("NormalRangeMaleTo", typeof(string)),
                 new DataColumn("NormalRangeFeMaleFrom", typeof(string)),
                 new DataColumn("NormalRangeFeMaleTo", typeof(string)),
                 new DataColumn("NormalRangeTransFeMaleFrom", typeof(string)),
                 new DataColumn("NormalRangeTransFeMaleTo", typeof(string)),
                 new DataColumn("NormalRangeTransMaleFrom", typeof(string)),
                 new DataColumn("NormalRangeTransMaleTo", typeof(string)),
                 new DataColumn("RowType", typeof(Int32)),
                 new DataColumn("TemplateType", typeof(Int32)),
                 new DataColumn("TestDoneMachine", typeof(Int32)),
                 new DataColumn("DefaultValue", typeof(Int32)),
                 new DataColumn("UserLoginID", typeof(Int32)),
                 new DataColumn("AddedBy", typeof(string)),
                 new DataColumn("AddedDate", typeof(DateTime)),
                 new DataColumn("ModifiedDate", typeof(DateTime)),
                 new DataColumn("ModifiedBy", typeof(string)),
                 new DataColumn("HospitalID", typeof(Int32)),
                 new DataColumn("FinancialYearID", typeof(Int32)),
                 new DataColumn("RangeWordingM", typeof(string)),
                  new DataColumn("RangeWordingF", typeof(string)),
                 new DataColumn("Remark", typeof(string)),
                 new DataColumn("IsActive", typeof(string))
               });
            int parametercount = 0;

            if (ddl_testtype.SelectedValue=="1")
            {
                foreach (GridViewRow row in GvLabSubTest.Rows)
                {
                    TextBox order = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_order");
                    TextBox parmcode_txt = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_paramcode");
                    Label IDs = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                    DropDownList machine = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[2].FindControl("ddl_machinename");
                    DropDownList unit = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_unit");
                    DropDownList samples = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_sample");
                    DropDownList reagent = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_reagent");
                    DropDownList method = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_method");
                    DropDownList ContainerIDs = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_containerID");


                    TextBox Description = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_subtest");

                    TextBox AgeYearFrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_year");
                    TextBox AgeMonthFrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_month");
                    TextBox AgeDayFrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_day");

                    TextBox AgeYearTo = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_yearto");
                    TextBox AgeMonthTo = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_monthto");
                    TextBox AgeDayTo = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_dayto");

                    TextBox MNrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangemale");
                    TextBox MNrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangemaleto");
                    TextBox FNrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangefemale");
                    TextBox FNrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangefemaleto");
                    TextBox Tm_Nrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransemalefrom");
                    TextBox Tm_Nrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransemaleto");
                    TextBox Tf_Nrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransefemalefrom");
                    TextBox Tf_Nrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransefemaleto");
                    Label GroupID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_groupdID");
                    Label SubGroupID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_subgroupID");
                    CheckBox chk = (CheckBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("chekbox");
                    DropDownList rowtype = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddlrowtype");
                    Label lblmachine = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_mchineid");
                    TextBox RangeWordingM = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_RangewordingM");
                    TextBox RangeWordingF = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_RangewordingF");

                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    DateTime pardate = System.DateTime.Now;
                    parametercount = parametercount + 1;
                    int PID = Convert.ToInt32(IDs.Text == "" ? "0" : IDs.Text);
                    int OrderNo = Convert.ToInt32(order.Text == "" ? "0" : order.Text);
                    int Unit = Convert.ToInt32(unit.SelectedValue == "" ? "0" : unit.SelectedValue);
                    int Sample = Convert.ToInt32(samples.SelectedValue == "" ? "0" : samples.SelectedValue);
                    int Reagent = Convert.ToInt32(reagent.SelectedValue == "" ? "0" : reagent.SelectedValue);
                    int ContainerID = Convert.ToInt32(ContainerIDs.SelectedValue == "" ? "0" : ContainerIDs.SelectedValue);
                    int Method = Convert.ToInt32(method.SelectedValue == "" ? "0" : method.SelectedValue);
                    int DefaultValue = chk.Checked ? 1 : 0;
                    string ParmCode = parmcode_txt.Text.Trim() == "" ? "" : parmcode_txt.Text.Trim();
                    string SubTestName = Description.Text;


                    string AgeFrom = (AgeYearFrom.Text.Trim() == "" ? "0Y" : AgeYearFrom.Text.Trim()+"Y")
                        + "-" + (AgeMonthFrom.Text.Trim() == "" ? "0M" : AgeMonthFrom.Text.Trim()+"M")
                        + "-" + (AgeDayFrom.Text.Trim() == "" ? "0D" : AgeDayFrom.Text.Trim()+"D");

                    string AgeTo = (AgeYearTo.Text.Trim() == "" ? "0Y" : AgeYearTo.Text.Trim()+"Y") + "-"
                        + (AgeMonthTo.Text.Trim() == "" ? "0M" : AgeMonthTo.Text.Trim()+"Y") + "-"
                        + (AgeDayTo.Text.Trim() == "" ? "0D" : AgeDayTo.Text.Trim()+"M");

                    string YearFrom = AgeYearFrom.Text.Trim() == "" ? "0" : AgeYearFrom.Text.Trim();
                    string MonthFrom = AgeMonthFrom.Text.Trim() == "" ? "0" : AgeMonthFrom.Text.Trim();
                    string DayFrom = AgeDayFrom.Text.Trim() == "" ? "0" : AgeDayFrom.Text.Trim();

                    string YearTo = AgeYearTo.Text.Trim() == "" ? "0" : AgeYearTo.Text.Trim();
                    string MonthTo = AgeMonthTo.Text.Trim() == "" ? "0" : AgeMonthTo.Text.Trim();
                    string DayTo = AgeDayTo.Text.Trim() == "" ? "0" : AgeDayTo.Text.Trim();

                    Int32 AgeFromD = 0;
                    Int32 AgeToD = 0;
                    string NormalRangeMaleFrom = MNrfrom.Text.Trim();
                    string NormalRangeMaleTo = MNrto.Text.Trim();
                    string NormalRangeFeMaleFrom = FNrfrom.Text.Trim();
                    string NormalRangeFeMaleTo = FNrto.Text.Trim();
                    string NormalRangeTransFeMaleFrom = Tf_Nrfrom.Text.Trim();
                    string NormalRangeTransFeMaleTo = Tf_Nrto.Text.Trim();
                    string NormalRangeTransMaleFrom = Tm_Nrfrom.Text.Trim();
                    string NormalRangeTransMaleTo = Tm_Nrto.Text.Trim();
                    int RowType = Convert.ToInt32(rowtype.SelectedValue == "" ? "0" : rowtype.SelectedValue);
                    int TemplateType = Convert.ToInt32(ddl_template.SelectedValue == "" ? "0" : ddl_template.SelectedValue);
                    int TestdoneMachine = Convert.ToInt32(ddl_machine.SelectedValue == "" ? "0" : ddl_machine.SelectedValue);
                    int LabGroupID = Convert.ToInt32(GroupID.Text == "" ? "0" : GroupID.Text);
                    int LabSubGroupID = Convert.ToInt32(SubGroupID.Text == "" ? "0" : SubGroupID.Text);
                    int TestID = Commonfunction.SemicolonSeparation_String_32(txt_test.Text);
                    int Machine = Convert.ToInt32(machine.Text == "" ? "0" : machine.Text);
                    string Remark = txt_Reamrks.Text.Trim();
                    string Mrangeremark = RangeWordingM.Text.Trim();
                    string Frangeremark = RangeWordingF.Text.Trim();
                    Int64 UserLoginID = LogData.EmployeeID;
                    int FinancialYearID = LogData.FinancialYearID;
                    int HospitalID = LogData.HospitalID;
                    int ParmID = 0;
                    string AddedBy = "";
                    DateTime AddedDate = pardate;
                    DateTime ModifiedDate = pardate;
                    string ModifiedBy = "";
                    int IsActive = 1;
                    dt.Rows.Add(PID, OrderNo, LabGroupID, LabSubGroupID, TestID, ParmID,ParmCode, SubTestName,
                     Unit, Sample, Machine, Method, Reagent, ContainerID, AgeFrom, AgeTo, YearFrom, MonthFrom, DayFrom, YearTo, MonthTo, DayTo, AgeFromD, AgeToD, NormalRangeMaleFrom,
                     NormalRangeMaleTo, NormalRangeFeMaleFrom, NormalRangeFeMaleTo, NormalRangeTransFeMaleFrom,
                     NormalRangeTransFeMaleTo, NormalRangeTransMaleFrom, NormalRangeTransMaleTo, RowType, TemplateType, TestdoneMachine,
                     DefaultValue, UserLoginID, AddedBy, AddedDate, ModifiedDate, ModifiedBy, HospitalID, FinancialYearID, Mrangeremark, Frangeremark, Remark, IsActive);
                }
            }
            else
            {
            
                foreach (GridViewRow row in GvProfile.Rows)
                {
                    TextBox order = (TextBox)GvProfile.Rows[row.RowIndex].Cells[0].FindControl("txt_order");
                    Label IDs = (Label)GvProfile.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                    Label rowParmID = (Label)GvProfile.Rows[row.RowIndex].Cells[0].FindControl("lbl_paramID");
                    TextBox rowParamName = (TextBox)GvProfile.Rows[row.RowIndex].Cells[0].FindControl("txt_paramname");
                    Label sampletypeID = (Label)GvProfile.Rows[row.RowIndex].Cells[0].FindControl("lbl_paramsampletypeID");
                    Label rowtypeID = (Label)GvProfile.Rows[row.RowIndex].Cells[0].FindControl("lbl_rowtypeID_profile");
                    DropDownList rowtype = (DropDownList)GvProfile.Rows[row.RowIndex].Cells[0].FindControl("ddlrowtype_profile");
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    DateTime pardate = System.DateTime.Now;

                    string blankstring = "";
                    int zeroint = 0;
                    parametercount = parametercount + 1;
                    int PID = Convert.ToInt32(IDs.Text == "" ? "0" : IDs.Text);
                    int OrderNo = Convert.ToInt32(order.Text == "" ? "0" : order.Text);
                    int Unit = zeroint;
                    int Sample = Convert.ToInt32(sampletypeID.Text == "" ? "0" : sampletypeID.Text);
                    int Reagent = zeroint;
                    int ContainerID = zeroint;
                    int Method = zeroint;
                    int DefaultValue = zeroint;
                    string ParmCode = blankstring;
                    string SubTestName = rowParamName.Text==""?"":rowParamName.Text;

                    string AgeFrom = blankstring;
                    string AgeTo = blankstring;

                    string YearFrom = blankstring;
                    string MonthFrom = blankstring;
                    string DayFrom = blankstring;

                    string YearTo = blankstring;
                    string MonthTo = blankstring;
                    string DayTo = blankstring;

                    int RowTypecheck = Convert.ToInt32(rowtype.SelectedValue == "" ? "0" : rowtype.SelectedValue);

                    if (RowTypecheck == 1)
                    {
                        AgeFrom = "0Y-0M-0D";
                        AgeTo = "120Y-0Y-0M";

                        YearFrom = "0";
                        MonthFrom = "0";
                        DayFrom = "0";

                        YearTo = "120";
                        MonthTo = "0";
                        DayTo = "0";
                    }


                    Int32 AgeFromD = 0;
                    Int32 AgeToD = 0;
                    string NormalRangeMaleFrom = blankstring;
                    string NormalRangeMaleTo = blankstring;
                    string NormalRangeFeMaleFrom = blankstring;
                    string NormalRangeFeMaleTo = blankstring;
                    string NormalRangeTransFeMaleFrom = blankstring;
                    string NormalRangeTransFeMaleTo = blankstring;
                    string NormalRangeTransMaleFrom = blankstring;
                    string NormalRangeTransMaleTo = blankstring;
                    int RowType = Convert.ToInt32(rowtype.SelectedValue == "" ? "0" : rowtype.SelectedValue);
                    int TemplateType = Convert.ToInt32(ddl_template.SelectedValue == "" ? "0" : ddl_template.SelectedValue);
                    int TestdoneMachine = Convert.ToInt32(ddl_machine.SelectedValue == "" ? "0" : ddl_machine.SelectedValue);
                    int LabGroupID = Convert.ToInt32(ddl_labgroup.Text == "" ? "0" : ddl_labgroup.Text);
                    int LabSubGroupID = Convert.ToInt32(ddl_labsubgroup.Text == "" ? "0" : ddl_labsubgroup.Text);
                    int TestID = Commonfunction.SemicolonSeparation_String_32(txt_test.Text);
                    int Machine = zeroint;
                    string Remark = txt_Reamrks.Text.Trim();
                    string Mrangeremark = blankstring;
                    string Frangeremark = blankstring;
                    Int64 UserLoginID = LogData.EmployeeID;
                    int FinancialYearID = LogData.FinancialYearID;
                    int HospitalID = LogData.HospitalID;
                    int ParmID = Convert.ToInt32(rowParmID.Text == "" ? "0" : rowParmID.Text);
                    string AddedBy = "";
                    DateTime AddedDate = pardate;
                    DateTime ModifiedDate = pardate;
                    string ModifiedBy = "";
                    int IsActive = 1;
                    dt.Rows.Add(PID, OrderNo, LabGroupID, LabSubGroupID, TestID, ParmID,ParmCode, SubTestName,
                     Unit, Sample, Machine, Method, Reagent, ContainerID, AgeFrom, AgeTo, YearFrom, MonthFrom, DayFrom, YearTo, MonthTo, DayTo, AgeFromD, AgeToD, NormalRangeMaleFrom,
                     NormalRangeMaleTo, NormalRangeFeMaleFrom, NormalRangeFeMaleTo, NormalRangeTransFeMaleFrom,
                     NormalRangeTransFeMaleTo, NormalRangeTransMaleFrom, NormalRangeTransMaleTo, RowType, TemplateType, TestdoneMachine,
                     DefaultValue, UserLoginID, AddedBy, AddedDate, ModifiedDate, ModifiedBy, HospitalID, FinancialYearID, Mrangeremark, Frangeremark, Remark, IsActive);
                }
            }
          
            


            if (parametercount == 0)
            {       
                txt_parameter.Focus();
                Messagealert_.ShowMessage(lblmessage, "Paracount", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            string constr = ConfigurationManager.ConnectionStrings["SqlConnectionString11"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_MDQ_Update_LabParameters"))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@tlabpara", dt);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        bindgrid();
                        Messagealert_.ShowMessage(lblmessage, "save", 1);
                        supplementoryvalues();
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                        LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                    }
                }
            }
        }
        private List<LabServiceMasterData> GetLabSubtestlist(int p, int testID)
        {
            LabServiceMasterData objlabserviceData = new LabServiceMasterData();
            LabServiceMasterBO objlabserviceBO = new LabServiceMasterBO();
            objlabserviceData.LabGroupID = Convert.ToInt32(ddl_labgroup.SelectedValue == "0" ? null : ddl_labgroup.SelectedValue);
            objlabserviceData.LabSubGroupID = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "0" ? null : ddl_labsubgroup.SelectedValue);
            objlabserviceData.MachineID = Convert.ToInt32(ddl_machine.SelectedValue == "0" ? null : ddl_machine.SelectedValue);
            objlabserviceData.TestID = testID;
            objlabserviceData.SubTestName = txt_parameter.Text.Trim();
            return objlabserviceBO.SearchLabSubTestDetails(objlabserviceData);
        }

        private List<LabServiceMasterData> GetLabParmListByTestID(int p, int testID)
        {
            LabServiceMasterData objlabserviceData = new LabServiceMasterData();
            LabServiceMasterBO objlabserviceBO = new LabServiceMasterBO();
            objlabserviceData.TestID = testID;
            return objlabserviceBO.SearchLabTestParamListByTestID(objlabserviceData);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            ddl_labgroup.SelectedIndex = 0;
            ddl_labsubgroup.SelectedIndex = 0;
            ddl_machine.SelectedIndex = 0;
            ddl_template.SelectedIndex = 0;
            Session["ProfileParamList"] = null;
            GvProfile.DataSource = Session["LabSubtestlist"];
            GvProfile.DataBind();
            GvProfile.Visible = false;
            Session["LabSubtestlist"] = null;
            GvLabSubTest.DataSource = Session["LabSubtestlist"];
            GvLabSubTest.DataBind();
            GvLabSubTest.Visible = false;
            ddl_testtype.SelectedIndex = 0;
            txt_test.Text = "";
            txt_parameter.Text = "";
            txt_Reamrks.Text = "";
            txt_elementorderno.Text = "";
            txt_testelement.Text = "";
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_labgroup, mstlookup.GetLookupsList(LookupName.LabGroups));
            ddl_labgroup.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(Convert.ToInt32(ddl_labgroup.SelectedValue)));
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvLabSubTest.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvLabSubTest.Columns[2].Visible = false;
                    GvLabSubTest.Columns[3].Visible = false;
                    GvLabSubTest.Columns[4].Visible = false;
                    GvLabSubTest.Columns[5].Visible = false;
                    GvLabSubTest.Columns[6].Visible = false;
                    GvLabSubTest.Columns[17].Visible = false;
                    GvLabSubTest.Columns[18].Visible = false;
                    GvLabSubTest.Columns[19].Visible = false;

                    GvLabSubTest.RenderControl(hw);
                    GvLabSubTest.HeaderRow.Style.Add("width", "15%");
                    GvLabSubTest.HeaderRow.Style.Add("font-size", "10px");
                    GvLabSubTest.Style.Add("text-decoration", "none");
                    GvLabSubTest.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvLabSubTest.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=LabSubGroupDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddlexport.SelectedIndex == 1)
            {
                ExportoExcel();
            }
            else if (ddlexport.SelectedIndex == 2)
            {
                ExportToPdf();
            }

            else
            {
                Messagealert_.ShowMessage(lblmessage, "ExportType", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";

                ddlexport.Focus();
                return;
            }
        }
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Service Detail List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=LabSubTestDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

        }
        protected DataTable GetDatafromDatabase()
        {
            List<LabServiceMasterData> LabServiceDetails = GetLabSubtestlist(0, Commonfunction.SemicolonSeparation_String_32(txt_test.Text));
            List<LabSubTestDatatoExcel> ListexcelData = new List<LabSubTestDatatoExcel>();
            int i = 0;
            foreach (LabServiceMasterData row in LabServiceDetails)
            {
                LabSubTestDatatoExcel ExcelSevice = new LabSubTestDatatoExcel();
                //ExcelSevice.SubTestCode = LabServiceDetails[i].Code;
                ExcelSevice.SubTestName = LabServiceDetails[i].SubTestName;
                ExcelSevice.Unit = LabServiceDetails[i].Unit;
                ExcelSevice.SampleType = LabServiceDetails[i].SampleType;
                ExcelSevice.Reagent = LabServiceDetails[i].Reagent;
                ExcelSevice.Method = LabServiceDetails[i].Method;
                ExcelSevice.container = LabServiceDetails[i].container;
                ExcelSevice.AgeRangeFrom = LabServiceDetails[i].AgeRangeFrom;
                ExcelSevice.AgeRangeTo = LabServiceDetails[i].AgeRangeTo;
                ExcelSevice.NormalRangeMaleFrom = LabServiceDetails[i].NormalRangeMaleFrom;
                ExcelSevice.NormalRangeMaleTo = LabServiceDetails[i].NormalRangeMaleTo;
                ExcelSevice.NormalRangeFeMaleFrom = LabServiceDetails[i].NormalRangeFeMaleFrom;
                ExcelSevice.NormalRangeFemaleTo = LabServiceDetails[i].NormalRangeFeMaleTo;

                ListexcelData.Add(ExcelSevice);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        public class ListtoDataTableConverter
        {

            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);

                //Get all the properties

                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];

                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows

                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable

                return dataTable;
            }


        }
        protected List<LabServiceMasterData> getgridviewdatalist()
        {
            List<LabServiceMasterData> LabParmList = new List<LabServiceMasterData>();

            foreach (GridViewRow row in GvLabSubTest.Rows)
            {
                TextBox order = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_order");
                Label IDs = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                DropDownList machine = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[2].FindControl("ddl_machinename");
                DropDownList unit = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_unit");
                DropDownList samples = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_sample");
                DropDownList reagent = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_reagent");
                DropDownList method = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_method");
                DropDownList ContainerID = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_containerID");
                TextBox paramcode = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_paramcode");
                TextBox Description = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_subtest");

                TextBox AgeDayFrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_day");
                TextBox AgeMonthFrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_month");
                TextBox AgeYearFrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_year");

                TextBox AgeDayTo = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_dayto");
                TextBox AgeMonthTo = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_monthto");
                TextBox AgeYearTo = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_yearto");
               
                TextBox MNrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangemale");
                TextBox MNrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangemaleto");
                TextBox RangeWordingM = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_RangewordingM");

                TextBox FNrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangefemale");
                TextBox FNrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangefemaleto");
                TextBox RangeWordingF = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_RangewordingF");

                TextBox Tm_Nrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransemalefrom");
                TextBox Tm_Nrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransemaleto");
                TextBox Tf_Nrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransefemalefrom");
                TextBox Tf_Nrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransefemaleto");

                Label GroupID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_groupdID");
                Label SubGroupID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_subgroupID");
                CheckBox chk = (CheckBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("chekbox");
                DropDownList rowtype = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddlrowtype");
                Label lblmachine = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_mchineid");
               
              

                LabServiceMasterData labservicedata = new LabServiceMasterData();
                labservicedata.ID= Convert.ToInt32(IDs.Text == "" ? "0" : IDs.Text);
                labservicedata.LabGroupID= Convert.ToInt32(GroupID.Text == "" ? "0" : GroupID.Text);
                labservicedata.LabSubGroupID = Convert.ToInt32(SubGroupID.Text == "" ? "0" : SubGroupID.Text);
                labservicedata.OrderNo = Convert.ToInt32(order.Text == "" ? "0" : order.Text);
                labservicedata.ParamCode = paramcode.Text.Trim() == ""?"": paramcode.Text.Trim();
                labservicedata.SubTestName= Description.Text;
                labservicedata.MachineID = Convert.ToInt32(machine.Text == "" ? "0" : machine.Text);
                labservicedata.UnitID= Convert.ToInt32(unit.Text == "" ? "0" : unit.Text);
                labservicedata.SampleTypeID= Convert.ToInt32(samples.Text == "" ? "0" : samples.Text);
                labservicedata.ReagentTypeID = Convert.ToInt32(reagent.Text == "" ? "0" : reagent.Text);
                labservicedata.MethodID = Convert.ToInt32(method.Text == "" ? "0" : method.Text);
                labservicedata.ContainerID = Convert.ToInt32(ContainerID.Text == "" ? "0" : ContainerID.Text);

                labservicedata.DayFrom = AgeDayFrom.Text.Trim() == "" ? "0" : AgeDayFrom.Text.Trim();
                labservicedata.MonthFrom = AgeMonthFrom.Text.Trim() == "" ? "0" : AgeMonthFrom.Text.Trim();
                labservicedata.YearFrom = AgeYearFrom.Text.Trim() == "" ? "0" : AgeYearFrom.Text.Trim();

                labservicedata.DayTo = AgeDayTo.Text.Trim() == "" ? "0" : AgeDayTo.Text.Trim();
                labservicedata.MonthTo = AgeMonthTo.Text.Trim() == "" ? "0" : AgeMonthTo.Text.Trim();
                labservicedata.YearTo = AgeYearTo.Text.Trim() == "" ? "0" : AgeYearTo.Text.Trim();

                labservicedata.NormalRangeMaleFrom = MNrfrom.Text.Trim();
                labservicedata.NormalRangeMaleTo = MNrto.Text.Trim();
                labservicedata.RangeWordingM = RangeWordingM.Text.Trim();

                labservicedata.NormalRangeFeMaleFrom = FNrfrom.Text.Trim();
                labservicedata.NormalRangeFeMaleTo = FNrto.Text.Trim();
                labservicedata.RangeWordingF = RangeWordingF.Text.Trim();
                
                labservicedata.RowTypeID= Convert.ToInt32(rowtype.Text == "" ? "0" : rowtype.Text);

                LabParmList.Add(labservicedata);

            }

            return LabParmList;        
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {

            if (Commonfunction.SemicolonSeparation_String_32(txt_test.Text) == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "test", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_test.Text = "";
                txt_test.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }


            // List<LabServiceMasterData> LabSubtestlist = Session["LabSubtestlist"] == null ? new List<LabServiceMasterData>() : (List<LabServiceMasterData>)Session["LabSubtestlist"];
            List<LabServiceMasterData> LabSubtestlist = getgridviewdatalist();
             LabServiceMasterData ObjService = new LabServiceMasterData();
            ObjService.SubTestName = txt_parameter.Text.ToString();
            ObjService.LabGroupID = Convert.ToInt32(ddl_labgroup.SelectedValue == "" ? "0" : ddl_labgroup.SelectedValue);
            ObjService.LabSubGroupID = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "" ? "0" : ddl_labsubgroup.SelectedValue);
            ObjService.MachineID = 0;
            ObjService.ID = 0;
            //Int32 lastOrderNo = 0;
            //if (GvLabSubTest.Rows.Count > 0)
            //{
            //    GridViewRow lastRows = GvLabSubTest.Rows[GvLabSubTest.Rows.Count - 1];
            //    string cellValue = lastRows.Cells[1].Text;
            //    TextBox lblorderno = (TextBox)lastRows.Cells[0].FindControl("txt_order");
            //    lastOrderNo = Convert.ToInt32(lblorderno.Text);

            //}
            ObjService.OrderNo = 1;
            LabSubtestlist.Add(ObjService);
            Session["LabSubtestlist"] = LabSubtestlist;
            if (LabSubtestlist.Count > 0)
            {
                GvLabSubTest.DataSource = LabSubtestlist;
                GvLabSubTest.DataBind();
                GvLabSubTest.Visible = true;
                Session["LabSubtestlist"] = LabSubtestlist;
                txt_parameter.Text = "";
                //txt_parameter.Focus();
            }
            else
            {
                GvLabSubTest.DataSource = null;
                GvLabSubTest.DataBind();
                GvLabSubTest.Visible = true;
            }
        }
        protected void gviplabsubtestlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvLabSubTest.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    if (ID.Text == "0")
                    {
                        List<LabServiceMasterData> LabServiceList = Session["LabSubtestlist"] == null ? new List<LabServiceMasterData>() : (List<LabServiceMasterData>)Session["LabSubtestlist"];
                        LabServiceList.RemoveAt(i);
                        if (LabServiceList.Count > 0)
                        {
                            Session["LabSubtestlist"] = LabServiceList;
                            GvLabSubTest.DataSource = LabServiceList;
                            GvLabSubTest.DataBind();
                        }
                        else
                        {
                            Session["LabSubtestlist"] = LabServiceList;
                            GvLabSubTest.DataSource = LabServiceList;
                            GvLabSubTest.DataBind();
                        }
                        if (LogData.DeleteEnable == 0)
                        {
                            Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                            divmsg1.Visible = true;
                            divmsg1.Attributes["class"] = "FailAlert";
                            return;
                        }
                        else
                        {
                            lblmessage.Visible = false;
                        }
                    }
                    else
                    {
                        LabServiceMasterData objlabserviceData = new LabServiceMasterData();
                        LabServiceMasterBO objlabserviceBO = new LabServiceMasterBO();
                        objlabserviceData.ID = Convert.ToInt32(ID.Text);
                        objlabserviceData.EmployeeID = LogData.EmployeeID;
                        objlabserviceData.ActionType = Enumaction.Delete;
                        LabServiceMasterBO objlabserviceBO1 = new LabServiceMasterBO();
                        int Result = objlabserviceBO1.DeleteLabSubTestDetailsByID(objlabserviceData);
                        if (Result == 1)
                        {
                            bindgrid();
                            //List<LabServiceMasterData> LabServiceList = Session["LabSubtestlist"] == null ? new List<LabServiceMasterData>() : (List<LabServiceMasterData>)Session["LabSubtestlist"];
                            //LabServiceList.RemoveAt(i);
                            //if (LabServiceList.Count > 0)
                            //{
                            //    Session["LabSubtestlist"] = LabServiceList;
                            //    GvLabSubTest.DataSource = LabServiceList;
                            //    GvLabSubTest.DataBind();
                            //}
                            //else
                            //{
                            //    Session["LabSubtestlist"] = LabServiceList;
                            //    GvLabSubTest.DataSource = LabServiceList;
                            //    GvLabSubTest.DataBind();
                            //}
                        }
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                lblmessage.Visible = true;
                lblmessage.CssClass = "Message";
            }
        }
        protected void btn_search_Click(object sender, EventArgs e)
        {
            bindgrid();
        }

      
        protected void txt_testelement_TextChanged(object sender, EventArgs e)
        {

        }

        protected void addelement_btn_Click(object sender, EventArgs e)
        {
            //if (txt_elementorderno.Text.Trim() == ""||txt_elementorderno.Text.Trim()=="0")
            //{
            //    Messagealert_.ShowMessage(lblmessage, "ElementOrderNo", 0);
            //    divmsg1.Visible = true;
            //    divmsg1.Attributes["class"] = "FailAlert";
            //    txt_elementorderno.Focus();
            //    return;
            //}
            //else
            //{
            //    divmsg1.Visible = false;


            //}
            if (Commonfunction.SemicolonSeparation_String_32(txt_testelement.Text) == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "test", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_test.Text = "";
                txt_test.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            List<LabServiceMasterData> ProfileParamList = Session["ProfileParamList"] == null ? new List<LabServiceMasterData>() : (List<LabServiceMasterData>)Session["ProfileParamList"];
            LabServiceMasterData ObjService = new LabServiceMasterData();
            ObjService.ID = 0;
            ObjService.TestID = Commonfunction.SemicolonSeparation_String_32(txt_test.Text);
            ObjService.ParamID = Commonfunction.SemicolonSeparation_String_32(txt_testelement.Text);
            ObjService.ParamName = Commonfunction.GetTestnameFromSuggestion(txt_testelement.Text);
            Int32 lastOrderNo = 0;

            string ID;
            var source = txt_testelement.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                
                // Check Duplicate data 
                foreach (GridViewRow row in GvProfile.Rows)
                {
                    Label ParamTestID = (Label)GvProfile.Rows[row.RowIndex].Cells[0].FindControl("lbl_paramID");
                    if (Convert.ToInt32(ParamTestID.Text == "" ? "0" : ParamTestID.Text) == Convert.ToInt32(ID == "" || ID == null ? "0" : ID))
                    {
                        txt_testelement.Text = "";
                        Messagealert_.ShowMessage(lblmessage, "DuplicateParam", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txt_testelement.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
            }

            if (GvProfile.Rows.Count > 0)
            {
                GridViewRow lastRows = GvProfile.Rows[GvProfile.Rows.Count - 1];
                string cellValue = lastRows.Cells[1].Text;
                TextBox lblorderno = (TextBox)lastRows.Cells[0].FindControl("txt_order");
                lastOrderNo = Convert.ToInt32(lblorderno.Text);

            }

            ObjService.OrderNo = (lastOrderNo + 1);
            ProfileParamList.Add(ObjService);
            if (ProfileParamList.Count > 0)
            {
                GvProfile.DataSource = ProfileParamList;
                GvProfile.DataBind();
                GvProfile.Visible = true;
                Session["ProfileParamList"] = ProfileParamList;
                txt_testelement.Text = "";
                txt_testelement.Focus();
            }
            else
            {
                GvProfile.DataSource = null;
                GvProfile.DataBind();
                GvProfile.Visible = true;
                Session["ProfileParamList"] = null;
            }
        }

        protected void GvProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvProfile.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    if (ID.Text == "0")
                    {
                        List<LabServiceMasterData> ProfileParamList = Session["ProfileParamList"] == null ? new List<LabServiceMasterData>() : (List<LabServiceMasterData>)Session["ProfileParamList"];
                        ProfileParamList.RemoveAt(i);
                        Session["ProfileParamList"] = ProfileParamList;
                        GvProfile.DataSource = ProfileParamList;
                        GvProfile.DataBind();

                    }
                    else
                    {
                        if (LogData.DeleteEnable == 0)
                        {
                            Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                            divmsg1.Visible = true;
                            divmsg1.Attributes["class"] = "FailAlert";
                            return;

                        }
                        else
                        {
                            lblmessage.Visible = false;
                        }

                        LabServiceMasterData objlabserviceData = new LabServiceMasterData();
                        LabServiceMasterBO objlabserviceBO = new LabServiceMasterBO();
                        objlabserviceData.ID = Convert.ToInt32(ID.Text);
                        objlabserviceData.EmployeeID = LogData.EmployeeID;
                        objlabserviceData.ActionType = Enumaction.Delete;
                        LabServiceMasterBO objlabserviceBO1 = new LabServiceMasterBO();
                        int Result = objlabserviceBO1.DeleteLabSubTestDetailsByID(objlabserviceData);
                        if (Result == 1)
                        {

                            bindgrid();                      
                        }
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                lblmessage.Visible = true;
                lblmessage.CssClass = "Message";
            }
        }

        protected void ddl_ProfileParmlist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GvProfile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            List<LookupItem> rowtypelist = Session["rowtypelist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["rowtypelist"];

            foreach (GridViewRow row in GvProfile.Rows)
            {
                try
                {
                    DropDownList ddl3 = (DropDownList)GvProfile.Rows[row.RowIndex].Cells[3].FindControl("ddlrowtype_profile");
                    Label ParamID = (Label)GvProfile.Rows[row.RowIndex].Cells[0].FindControl("lbl_param");
                    Label RowTypeID = (Label)GvProfile.Rows[row.RowIndex].Cells[0].FindControl("lbl_rowtypeID_profile");

                 
                    Commonfunction.PopulateDdl(ddl3, rowtypelist);

                    if (ParamID.Text != "0")
                    {
                        ddl3.Visible = false;
                    }

                    if (RowTypeID.Text != "0")
                    {
                        ddl3.Items.FindByValue(RowTypeID.Text).Selected = true;
                    }
                  
                }
                catch (Exception ex)
                {
                    PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                    LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                }
            }
        }

        protected void addrowprofile_btn_Click(object sender, EventArgs e)
        {
            if (Commonfunction.SemicolonSeparation_String_32(txt_test.Text) == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "test", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_test.Text = "";
                txt_test.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }

            List<LabServiceMasterData> ProfileParamList = Session["ProfileParamList"] == null ? new List<LabServiceMasterData>() : (List<LabServiceMasterData>)Session["ProfileParamList"];
            LabServiceMasterData ObjService = new LabServiceMasterData();

            //ObjService.ID = 0;
           // ObjService.TestID = 0;
            ObjService.ParamID = 0;
            ObjService.ParamName = "";
            ObjService.RowTypeID = 1;
            Int32 lastOrderNo = 0;
            if (GvProfile.Rows.Count > 0)
            {
                GridViewRow lastRows = GvProfile.Rows[GvProfile.Rows.Count - 1];
                string cellValue = lastRows.Cells[1].Text;
                TextBox lblorderno = (TextBox)lastRows.Cells[0].FindControl("txt_order");
                lastOrderNo = Convert.ToInt32(lblorderno.Text);

            }
            ObjService.OrderNo = (lastOrderNo + 1);
            ProfileParamList.Add(ObjService);
            Session["ProfileParamList"] = ProfileParamList;
            if (ProfileParamList.Count > 0)
            {
                GvProfile.DataSource = ProfileParamList;
                GvProfile.DataBind();
                GvProfile.Visible = true;
                Session["ProfileParamList"] = ProfileParamList;
                TextBox txtparam = (TextBox)GvProfile.Rows[GvProfile.Rows.Count-1].Cells[1].FindControl("txt_paramname");
                txtparam.Focus();


            }
            else
            {
                GvProfile.DataSource = null;
                GvProfile.DataBind();
                GvProfile.Visible = true;
            }
        }
    }
}
