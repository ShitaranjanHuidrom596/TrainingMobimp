using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using Mediqura.Utility;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedUtility.Reports
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
        ReportDocument crystalReport = new ReportDocument();
        ReportDocument crSubreportDocument = new ReportDocument();
        Sections crSections;
        SubreportObject crSubreportObject;
        ReportObjects crReportObjects;
        ConnectionInfo crConnectionInfo;
        Database crDatabase;
        Tables crTables;
        TableLogOnInfo crTableLogOnInfo;
        string constr = ConfigurationManager.ConnectionStrings["SqlConnectionString11"].ConnectionString;
        string ReportUserId = ConfigurationManager.AppSettings["ReportUserId"];
        string ReportServerName = ConfigurationManager.AppSettings["ReportServerName"];
        string ReportDatabase = ConfigurationManager.AppSettings["ReportDatabase"];
        string ReportPassword = ConfigurationManager.AppSettings["ReportPassword"];

        protected void Page_Unload(Object sender, EventArgs evntArgs)
        {
            crystalReport.Close();
            crystalReport.Dispose();
            crystalReport = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["option"] != null)
            {
                switch (Request["option"].ToString())
                {

                    case "Designation":
                        DataTable dt10 = new DataTable();
                        crystalReport.Load(Server.MapPath("Designation.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_util_Designation_rpt";
                                    cmd.Parameters.Add("@DesignationTypeCode", SqlDbType.VarChar).Value = Request["Code"].ToString() == "" ? "" : Request["Code"].ToString();
                                    cmd.Parameters.Add("@DesignationType", SqlDbType.VarChar).Value = Request["Designation"].ToString() == "" ? "" : Request["Designation"].ToString();
                                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = Request["IsActive"].ToString() == "0" ? true : false;
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt10);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt10);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Designation");
                        break;
                }
            }
        }
    }
}