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

namespace Mediqura.Web.MedUtility
{
    public partial class AntibioticMaster : BasePage
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
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_antibioticcode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_antibioticcode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_antibioticname.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_antibioticname.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                AntiBioticNameData objAntiBioticNameData = new AntiBioticNameData();
                AntiBioticNameBO objantibioticbo = new AntiBioticNameBO();
                objAntiBioticNameData.Code = txt_antibioticcode.Text == "" ? null : txt_antibioticcode.Text;
                objAntiBioticNameData.AntiBioticName = txt_antibioticname.Text == "" ? null : txt_antibioticname.Text;
                objAntiBioticNameData.EmployeeID = LogData.EmployeeID;
                objAntiBioticNameData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objAntiBioticNameData.HospitalID = LogData.HospitalID;
                objAntiBioticNameData.Unit= txt_unit.Text == "" ? "" : txt_unit.Text;
                objAntiBioticNameData.FinancialYearID = LogData.FinancialYearID;
                objAntiBioticNameData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_antibioticcode.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objAntiBioticNameData.ActionType = Enumaction.Update;
                    objAntiBioticNameData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objantibioticbo.UpdateAntibioticDetails(objAntiBioticNameData);  // funtion at DAL
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    ViewState["ID"] = null;
                    bindgrid(1);
                }
                else if (result == 5)
                {
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                }
                else
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void GvLabAntibiotic_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edits")
                {
                    if (LogData.EditEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "EditEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    AntiBioticNameData objAntiBioticNameData = new AntiBioticNameData();
                    AntiBioticNameBO objantibioticbo = new AntiBioticNameBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvLabAntibiotic.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("code");
                    objAntiBioticNameData.ID = Convert.ToInt32(ID.Text);
                    objAntiBioticNameData.ActionType = Enumaction.Select;

                    List<AntiBioticNameData> GetResult = objantibioticbo.GetAntibioticDetailsByID(objAntiBioticNameData);
                    if (GetResult.Count > 0)
                    {
                        txt_antibioticcode.Text = GetResult[0].Code;
                        txt_antibioticname.Text = GetResult[0].AntiBioticName;
                        txt_unit.Text= GetResult[0].Unit;
                        ViewState["ID"] = GetResult[0].ID;
                    }
                }
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    AntiBioticNameData objAntiBioticNameData = new AntiBioticNameData();
                    AntiBioticNameBO objantibioticbo = new AntiBioticNameBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvLabAntibiotic.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objAntiBioticNameData.ID = Convert.ToInt32(ID.Text);
                    objAntiBioticNameData.EmployeeID = LogData.EmployeeID;
                    objAntiBioticNameData.ActionType = Enumaction.Delete;
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
                        objAntiBioticNameData.Remark = txtremarks.Text;
                    }

                    AntiBioticNameBO objantibioticbo1 = new AntiBioticNameBO();
                    int Result = objantibioticbo1.DeleteAntibioticDetailsByID(objAntiBioticNameData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        div1.Visible = true;
                        div1.Attributes["class"] = "SucessAlert";
                        bindgrid(1);
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";

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

                List<AntiBioticNameData> lstemp = GetAntiBioticDetails(page);
                if (lstemp.Count > 0)
                {
                    GvLabAntibiotic.VirtualItemCount = lstemp[0].MaximumRows;//total item is required for custom paging
                    GvLabAntibiotic.PageIndex = page - 1;

                    GvLabAntibiotic.DataSource = lstemp;
                    GvLabAntibiotic.DataBind();
                    GvLabAntibiotic.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvLabAntibiotic.DataSource = null;
                    GvLabAntibiotic.DataBind();
                    GvLabAntibiotic.Visible = true;
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
        private List<AntiBioticNameData> GetAntiBioticDetails(int p)
        {
            AntiBioticNameData objAntiBioticNameData = new AntiBioticNameData();
            AntiBioticNameBO objantibioticbo = new AntiBioticNameBO();
            objAntiBioticNameData.Code = txt_antibioticcode.Text == "" ? "" : txt_antibioticcode.Text;
            objAntiBioticNameData.AntiBioticName = txt_antibioticname.Text == "" ? "" : txt_antibioticname.Text;
            objAntiBioticNameData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objAntiBioticNameData.CurrentIndex = p;
            return objantibioticbo.SearchAntibioticDetails(objAntiBioticNameData);
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
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
        }
        private void clearall()
        {
            txt_antibioticcode.Text = "";
            txt_antibioticname.Text = "";
            txt_unit.Text = "";
            GvLabAntibiotic.DataSource = null;
            GvLabAntibiotic.DataBind();
            GvLabAntibiotic.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            ddlstatus.SelectedIndex = 0;
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
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
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvLabAntibiotic.AllowPaging = false;
                    GvLabAntibiotic.DataSource = GetAntiBioticName(0);
                    GvLabAntibiotic.DataBind();

                    GvLabAntibiotic.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvLabAntibiotic.Columns[5].Visible = false;
                    GvLabAntibiotic.Columns[6].Visible = false;
                    GvLabAntibiotic.Columns[7].Visible = false;

                    GvLabAntibiotic.RenderControl(hw);
                    GvLabAntibiotic.HeaderRow.Style.Add("width", "15%");
                    GvLabAntibiotic.HeaderRow.Style.Add("font-size", "10px");
                    GvLabAntibiotic.Style.Add("text-decoration", "none");
                    GvLabAntibiotic.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvLabAntibiotic.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=AntiBioticList.pdf");
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
                wb.Worksheets.Add(dt, "AntiBiotic List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=AntiBiotic List.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        private DataTable GetDatafromDatabase()
        {
            List<AntiBioticNameData> antibioticdetails = GetAntiBioticName(0);
            List<AntiBioticNameDatatoExcel> ListexcelData = new List<AntiBioticNameDatatoExcel>();
            int i = 0;
            foreach (AntiBioticNameData row in antibioticdetails)
            {
                AntiBioticNameDatatoExcel ExcelSevice = new AntiBioticNameDatatoExcel();
                ExcelSevice.ID = antibioticdetails[i].ID;
                ExcelSevice.Code = antibioticdetails[i].Code;
                ExcelSevice.AntiBioticName = antibioticdetails[i].AntiBioticName;
                ExcelSevice.AddedBy = antibioticdetails[i].EmpName;
                ExcelSevice.AddedDate = antibioticdetails[i].AddedDate;
                GvLabAntibiotic.Columns[4].Visible = false;
                GvLabAntibiotic.Columns[5].Visible = false;
                GvLabAntibiotic.Columns[6].Visible = false;
                GvLabAntibiotic.Columns[7].Visible = false;
                ListexcelData.Add(ExcelSevice);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }

        private List<AntiBioticNameData> GetAntiBioticName(int p)
        {
            AntiBioticNameData objAntiBioticNameData = new AntiBioticNameData();
            AntiBioticNameBO objantibioticbo = new AntiBioticNameBO();
            objAntiBioticNameData.Code = txt_antibioticcode.Text == "" ? "" : txt_antibioticcode.Text;
            objAntiBioticNameData.AntiBioticName = txt_antibioticname.Text == "" ? "" : txt_antibioticname.Text;
            objAntiBioticNameData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objantibioticbo.SearchAntiBioticName(objAntiBioticNameData);
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
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

        protected void GvLabAntibiotic_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }
    }

}