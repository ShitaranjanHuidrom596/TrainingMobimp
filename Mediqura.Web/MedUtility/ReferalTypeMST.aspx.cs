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

namespace Mediqura.Web.MedUtility
{
    public partial class ReferalType : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;

            }
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
                if (txtReferalTypeCode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txtReferalTypeCode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtReferalType.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "dept", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txtReferalType.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                ReferalTypeData objReferalTypeData = new ReferalTypeData();
                ReferalTypeBO objReferalTypeBO = new ReferalTypeBO();
                objReferalTypeData.ReferalTypeCode = txtReferalTypeCode.Text == "" ? null : txtReferalTypeCode.Text;
                objReferalTypeData.ReferalType = txtReferalType.Text == "" ? null : txtReferalType.Text;
                objReferalTypeData.EmployeeID = LogData.EmployeeID;
                objReferalTypeData.IPaddress = LogData.IPaddress;
                objReferalTypeData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objReferalTypeData.FinancialYearID = LogData.FinancialYearID;
                objReferalTypeData.ActionType = Enumaction.Insert;
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
                        objReferalTypeData.ActionType = Enumaction.Update;
                        objReferalTypeData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objReferalTypeBO.UpdateReferalTypeDetails(objReferalTypeData);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";

                    ViewState["ID"] = null;
                    bindgrid();
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
        protected void GvReferalType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    ReferalTypeData objReferalTypeData = new ReferalTypeData();
                    ReferalTypeBO objReferalTypeBO = new ReferalTypeBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvReferalType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblReferalTypeID");
                    objReferalTypeData.ID = Convert.ToInt32(ID.Text);
                    objReferalTypeData.ActionType = Enumaction.Select;

                    List<ReferalTypeData> GetResult = objReferalTypeBO.GetReferalTypeDetailsByID(objReferalTypeData);
                    if (GetResult.Count > 0)
                    {
                        txtReferalTypeCode.Text = GetResult[0].ReferalTypeCode;
                        txtReferalType.Text = GetResult[0].ReferalType;
                        ViewState["ID"] = GetResult[0].ID;
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
                    ReferalTypeData objReferalTypeData = new ReferalTypeData();
                    ReferalTypeBO objReferalTypeBO = new ReferalTypeBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvReferalType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblReferalTypeID");
                    objReferalTypeData.ID = Convert.ToInt32(ID.Text);
                    objReferalTypeData.EmployeeID = LogData.EmployeeID;
                    objReferalTypeData.ActionType = Enumaction.Delete;
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
                        objReferalTypeData.Remarks = txtremarks.Text;
                    }

                    ReferalTypeBO objReferalTypeBO1 = new ReferalTypeBO();
                    int Result = objReferalTypeBO1.DeleteReferalTypeDetailsByID(objReferalTypeData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";

                        bindgrid();
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
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }
        }
        private void bindgrid()
        {
            try
            {

                List<ReferalTypeData> lstemp = GetReferalType(0);

                if (lstemp.Count > 0)
                {
                    GvReferalType.DataSource = lstemp;
                    GvReferalType.DataBind();
                    GvReferalType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;

                }
                else
                {
                    GvReferalType.DataSource = null;
                    GvReferalType.DataBind();
                    GvReferalType.Visible = true;
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
        private List<ReferalTypeData> GetReferalType(int p)
        {
            ReferalTypeData objReferalTypeData = new ReferalTypeData();
            ReferalTypeBO objReferalTypeBO = new ReferalTypeBO();
            objReferalTypeData.ReferalTypeCode = txtReferalTypeCode.Text == "" ? "" : txtReferalTypeCode.Text;
            objReferalTypeData.ReferalType = txtReferalType.Text == "" ? "" : txtReferalType.Text;
            objReferalTypeData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objReferalTypeBO.SearchReferalTypeDetails(objReferalTypeData);
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

            bindgrid();

        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            clearall();
            lblmessage.Visible = false;
            lblresult.Visible = false;
        }
        private void clearall()
        {
            txtReferalTypeCode.Text = "";
            txtReferalType.Text = "";
            ddlstatus.SelectedIndex = 0;
            GvReferalType.DataSource = null;
            GvReferalType.DataBind();
            GvReferalType.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;

        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvReferalType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvReferalType.Columns[4].Visible = false;
                    GvReferalType.Columns[5].Visible = false;
                    GvReferalType.Columns[6].Visible = false;
                    GvReferalType.Columns[7].Visible = false;

                    GvReferalType.RenderControl(hw);
                    GvReferalType.HeaderRow.Style.Add("width", "15%");
                    GvReferalType.HeaderRow.Style.Add("font-size", "10px");
                    GvReferalType.Style.Add("text-decoration", "none");
                    GvReferalType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvReferalType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=ReferalTypeDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        protected void GvReferalType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvReferalType.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Department Type Detail List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=ReferalTypeDetails.xlsx");
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
            List<ReferalTypeData> ReferalTypeDetails = GetReferalTypes(0);
            List<ReferalTypeDatatoExcel> ListexcelData = new List<ReferalTypeDatatoExcel>();
            int i = 0;
            foreach (ReferalTypeData row in ReferalTypeDetails)
            {
                ReferalTypeDatatoExcel ExcelSevice = new ReferalTypeDatatoExcel();
                ExcelSevice.ID = ReferalTypeDetails[i].ID;
                ExcelSevice.ReferalTypeCode = ReferalTypeDetails[i].ReferalTypeCode;
                ExcelSevice.ReferalType = ReferalTypeDetails[i].ReferalType;
                ExcelSevice.AddedBy = ReferalTypeDetails[i].EmpName;
                GvReferalType.Columns[4].Visible = false;
                GvReferalType.Columns[5].Visible = false;
                GvReferalType.Columns[6].Visible = false;
                GvReferalType.Columns[7].Visible = false;
                ListexcelData.Add(ExcelSevice);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        private List<ReferalTypeData> GetReferalTypes(int p)
        {
            ReferalTypeData objReferalTypeData = new ReferalTypeData();
            ReferalTypeBO objReferalTypeBO = new ReferalTypeBO();
            objReferalTypeData.ReferalTypeCode = txtReferalTypeCode.Text == "" ? "" : txtReferalTypeCode.Text;
            objReferalTypeData.ReferalType = txtReferalType.Text == "" ? "" : txtReferalType.Text;
            //  objPatientTypeMasterData.ServiceTypeID = Convert.ToInt32(ddlservicetype.SelectedValue == "" ? "0" : ddlservicetype.SelectedValue);
            objReferalTypeData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objReferalTypeBO.SearchReferalTypeDetails(objReferalTypeData);
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
    }
}