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
    public partial class LabTestFormula : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                ddlbind();

                bindgrid(0);
            }
        }
       
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
          
            Commonfunction.PopulateDdl(ddl_profilegroup, mstlookup.GetLookupsList(LookupName.ProfileGroups));
            Commonfunction.PopulateDdl(ddl_testlistinsideprofile, mstlookup.GetTestlistByProfileID(Convert.ToInt32(ddl_profilegroup.SelectedValue)));
        }

        private void bindgrid(int page)
        {
            try
            {

                List<LabTestFormulaData> lstemp = GetFormulaDetails(page);
                if (lstemp.Count > 0)
                {
                    //GvFormula.VirtualItemCount = lstemp[0].MaximumRows;//total item is required for custom paging
                    //GvFormula.PageIndex = page - 1;

                    GvFormula.DataSource = lstemp;
                    GvFormula.DataBind();
                    GvFormula.Visible = true;
                    //Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    //divmsg3.Attributes["class"] = "SucessAlert";
                    //divmsg3.Visible = true;
                    //ddlexport.Visible = true;
                    //btnexport.Visible = true;
                }
                else
                {
                    GvFormula.DataSource = null;
                    GvFormula.DataBind();
                    GvFormula.Visible = true;
                    //lblresult.Visible = false;
                    //ddlexport.Visible = false;
                    //btnexport.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private List<LabTestFormulaData> GetFormulaDetails(int p)
        {
            LabTestFormulaData objformuladata = new LabTestFormulaData();
            LabTestFormulaBO objformulaBO = new LabTestFormulaBO();

            objformuladata.TestID = 0;
            objformuladata.IsActive =true;
            return objformulaBO.SearchFormulaDetails(objformuladata);
        }

        protected void ddl_profilegroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_testlistinsideprofile, mstlookup.GetTestlistByProfileID(Convert.ToInt32(ddl_profilegroup.SelectedValue)));
           Commonfunction.Populatelistbox(lbx_testlistinsideprofile, mstlookup.GetTestlistByProfileID(Convert.ToInt32(ddl_profilegroup.SelectedValue)));

           // Commonfunction.Populatecheckboxlist(chbx_testlistinsideprofile, mstlookup.GetTestlistByProfileID(Convert.ToInt32(ddl_profilegroup.SelectedValue)));
        }

        protected void ddl_testlistinsideprofile_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_testlistinsideprofile.SelectedValue != "0")
            {
                txt_formulafortest.Text = ddl_testlistinsideprofile.SelectedItem.ToString();
                txt_formulafortest.Attributes["disabled"] = "disabled";
            }
            else
            {
                txt_formulafortest.Text = "";
                txt_formulafortest.Attributes["disabled"] = "disabled";
            }
           
        }

        protected void lbx_testlistinsideprofile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbx_testlistinsideprofile.SelectedItem != null)
            {
               
                txt_formula.Text = txt_formula.Text + "{" + lbx_testlistinsideprofile.SelectedItem.ToString() + "}";
               
            }

        }

        protected void chbx_testlistinsideprofile_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (chbx_testlistinsideprofile.SelectedItem != null)
            //{
            //    txt_formula.Text = txt_formula.Text + "{" + chbx_testlistinsideprofile.SelectedItem.ToString() + "}";
            //}
           
        }

        protected void btnadd_Click(object sender, EventArgs e)
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

                if (ddl_testlistinsideprofile.SelectedIndex==0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_formula.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                if (txt_formula.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_formula.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                LabTestFormulaData objformula = new LabTestFormulaData();
                LabTestFormulaBO objCountryMSTBO = new LabTestFormulaBO();
               

                objformula.TestID = Convert.ToInt32(ddl_testlistinsideprofile.SelectedValue == "0" ? null : ddl_testlistinsideprofile.SelectedValue);
                objformula.EmployeeID = LogData.EmployeeID;
                objformula.Formula = txt_formula.Text.ToString().Trim();
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
                    }
                 
                }
                int result = objCountryMSTBO.UpdateTestFormulaDetails(objformula);  // funtion at DAL
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    ViewState["ID"] = null;
                    bindgrid(0);
                   
                }
                else if (result == 5)
                {
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
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

        protected void clearall()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            ddl_profilegroup.SelectedIndex = 0;
            Commonfunction.Insertzeroitemindex(ddl_testlistinsideprofile);
            txt_formulafortest.Text = "";
            txt_formula.Text = "";
        }

       
    }
}
