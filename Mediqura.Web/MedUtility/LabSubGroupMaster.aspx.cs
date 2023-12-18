﻿using ClosedXML.Excel;
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

namespace Mediqura.Web.MedUtility
{
    public partial class LabSubGroupMaster : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                ddlbind();
                bindgrid(1);
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_labgroup, mstlookup.GetLookupsList(LookupName.LabGroups));
        }
        protected void ddl_show_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindgrid(1);
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (LogData.SaveEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_labgroup.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Group", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddl_labgroup.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_labsubgrouptype.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "SubgroupType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_labsubgrouptype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                LabSubGroupMasterData objlabsubgroupData = new LabSubGroupMasterData();
                LabSubGroupMasterBO objlabsubgroupBO = new LabSubGroupMasterBO();
                objlabsubgroupData.LabGroupID = Convert.ToInt32(ddl_labgroup.SelectedValue == "0" ? null : ddl_labgroup.SelectedValue);
                objlabsubgroupData.SubGroupType = txt_labsubgrouptype.Text == "" ? null : txt_labsubgrouptype.Text;
                objlabsubgroupData.EmployeeID = LogData.EmployeeID;
                objlabsubgroupData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objlabsubgroupData.FinancialYearID = LogData.FinancialYearID;
                objlabsubgroupData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        objlabsubgroupData.ActionType = Enumaction.Update;
                        objlabsubgroupData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objlabsubgroupBO.UpdateLabSubGroupDetails(objlabsubgroupData);
                if (result == 1 || result == 2)
                {
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";

                    ViewState["ID"] = null;
                    bindgrid(1);
                }
                else if (result == 5)
                {
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);

            }
        }
        protected void GvLabSubGroupType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edits")
                {
                    if (LogData.EditEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "EditEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    LabSubGroupMasterData objlabsubgroupData = new LabSubGroupMasterData();
                    LabSubGroupMasterBO objlabsubgroupBO = new LabSubGroupMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvLabSubGroupType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblid");
                    objlabsubgroupData.ID = Convert.ToInt32(ID.Text);
                    objlabsubgroupData.ActionType = Enumaction.Select;

                    List<LabSubGroupMasterData> GetResult = objlabsubgroupBO.GetLabSubGroupTypeDetailsByID(objlabsubgroupData);
                    if (GetResult.Count > 0)
                    {
                        ddl_labgroup.SelectedValue = GetResult[0].LabGroupID.ToString();
                        txt_labsubgrouptype.Text = GetResult[0].SubGroupType;
                        ddlstatus.SelectedValue = GetResult[0].IsActive.ToString();
                        ViewState["ID"] = GetResult[0].ID;
                        btnsave.Text = "Update";
                    }
                }
                if (e.CommandName == "Deletes")
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
                    LabSubGroupMasterData objlabsubgroupData = new LabSubGroupMasterData();
                    LabSubGroupMasterBO objlabsubgroupBO = new LabSubGroupMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvLabSubGroupType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblid");
                    objlabsubgroupData.ID = Convert.ToInt32(ID.Text);
                    objlabsubgroupData.EmployeeID = LogData.EmployeeID;
                    objlabsubgroupData.ActionType = Enumaction.Delete;
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "FailAlert";

                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objlabsubgroupData.Remarks = txtremarks.Text;
                    }

                    LabSubGroupMasterBO objItemSubGroupTypeMasterBO1 = new LabSubGroupMasterBO();
                    int Result = objItemSubGroupTypeMasterBO1.DeleteLabSubGroupTypeDetailsByID(objlabsubgroupData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";

                        bindgrid(1);
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";

                    }
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private void bindgrid(int page)
        {
            try
            {
                List<LabSubGroupMasterData> lstemp = GetLabSubGroupType(page);

                if (lstemp.Count > 0)
                {
                    GvLabSubGroupType.VirtualItemCount = lstemp[0].MaximumRows;//total item is required for custom paging
                    GvLabSubGroupType.PageIndex = page - 1;
                    lbl_totalrecords.Text = lstemp[0].MaximumRows.ToString();
                    GvLabSubGroupType.DataSource = lstemp;
                    GvLabSubGroupType.DataBind();
                    GvLabSubGroupType.Visible = true;

                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);


                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    btnsave.Text = "Add";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvLabSubGroupType.DataSource = null;
                    GvLabSubGroupType.DataBind();
                    GvLabSubGroupType.Visible = true;
                    lblresult.Visible = false;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private List<LabSubGroupMasterData> GetLabSubGroupType(int p)
        {
            LabSubGroupMasterData objlabsubgroupData = new LabSubGroupMasterData();
            LabSubGroupMasterBO objlabsubgroupBO = new LabSubGroupMasterBO();
            objlabsubgroupData.LabGroupID = Convert.ToInt32(ddl_labgroup.SelectedValue == "" ? null : ddl_labgroup.SelectedValue);
            // objlabsubgroupData.SubGroupCode = txt_labsubgroupcode.Text == "" ? "" : txt_labsubgroupcode.Text;
            objlabsubgroupData.SubGroupType = txt_labsubgrouptype.Text == "" ? "" : txt_labsubgrouptype.Text;
            objlabsubgroupData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objlabsubgroupData.CurrentIndex = p;
            objlabsubgroupData.PageSize = Convert.ToInt32(ddl_show.SelectedValue == "10000" ? lbl_totalrecords.Text : ddl_show.SelectedValue);
            return objlabsubgroupBO.SearchSubGroupDetails(objlabsubgroupData);
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            bindgrid(1);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            clearall();
            lblmessage.Visible = false;
            lblresult.Visible = false;
            ddl_show.SelectedIndex = 0;
            btnsave.Text = "Add";
            bindgrid(1);
        }
        private void clearall()
        {
            ddl_labgroup.SelectedIndex = 0;
            //txt_labsubgroupcode.Text = "";
            txt_labsubgrouptype.Text = "";
            ddlstatus.SelectedIndex = 0;
            GvLabSubGroupType.DataSource = null;
            GvLabSubGroupType.DataBind();
            GvLabSubGroupType.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvLabSubGroupType.AllowPaging = false;
                    GvLabSubGroupType.DataSource = GetLabSubGroupDetails(0);
                    GvLabSubGroupType.DataBind();

                    GvLabSubGroupType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvLabSubGroupType.Columns[6].Visible = false;
                    GvLabSubGroupType.Columns[7].Visible = false;
                    GvLabSubGroupType.Columns[8].Visible = false;

                    GvLabSubGroupType.RenderControl(hw);
                    GvLabSubGroupType.HeaderRow.Style.Add("width", "15%");
                    GvLabSubGroupType.HeaderRow.Style.Add("font-size", "10px");
                    GvLabSubGroupType.Style.Add("text-decoration", "none");
                    GvLabSubGroupType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvLabSubGroupType.Style.Add("font-size", "8px");
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
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Item Type Detail List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=LabSubGroupDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private DataTable GetDatafromDatabase()
        {
            List<LabSubGroupMasterData> LabSubGroupTypeDetails = GetLabSubGroupDetails(0);
            List<LabSubGroupMasterDatatoExcel> ListexcelData = new List<LabSubGroupMasterDatatoExcel>();
            int i = 0;
            foreach (LabSubGroupMasterData row in LabSubGroupTypeDetails)
            {
                LabSubGroupMasterDatatoExcel ExcelSevice = new LabSubGroupMasterDatatoExcel();
                ExcelSevice.ID = LabSubGroupTypeDetails[i].ID;
                ExcelSevice.LabGroup = LabSubGroupTypeDetails[i].LabGroup;
                ExcelSevice.SubGroupCode = LabSubGroupTypeDetails[i].SubGroupCode;
                ExcelSevice.SubGroupType = LabSubGroupTypeDetails[i].SubGroupType;
                ExcelSevice.AddedBy = LabSubGroupTypeDetails[i].EmpName;
                GvLabSubGroupType.Columns[5].Visible = false;
                GvLabSubGroupType.Columns[6].Visible = false;
                GvLabSubGroupType.Columns[7].Visible = false;
                GvLabSubGroupType.Columns[8].Visible = false;
                ListexcelData.Add(ExcelSevice);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        private List<LabSubGroupMasterData> GetLabSubGroupDetails(int p)
        {
            LabSubGroupMasterData objlabsubgroupData = new LabSubGroupMasterData();
            LabSubGroupMasterBO objlabsubgroupBO = new LabSubGroupMasterBO();
            objlabsubgroupData.LabGroupID = Convert.ToInt32(ddl_labgroup.SelectedValue == "" ? null : ddl_labgroup.SelectedValue);
            // objlabsubgroupData.SubGroupCode = txt_labsubgroupcode.Text == "" ? "" : txt_labsubgroupcode.Text;
            objlabsubgroupData.SubGroupType = txt_labsubgrouptype.Text == "" ? "" : txt_labsubgrouptype.Text;
            objlabsubgroupData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objlabsubgroupBO.SearchSubGroupTypeDetails(objlabsubgroupData);
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
                Messagealert_.ShowMessage(lblresult, "ExportType", 0);
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "FailAlert";

                ddlexport.Focus();
                return;
            }
        }
        protected void GvLabSubGroupType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }
    }
}