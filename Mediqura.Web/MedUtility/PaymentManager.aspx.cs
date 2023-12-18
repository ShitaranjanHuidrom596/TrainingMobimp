using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedUtility
{
    public partial class PaymentManager :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                bindddl();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_source, mstlookup.GetLookupsList(LookupName.ReferalType));
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> Getautoreferals(string prefixText, int count, string contextKey)
        {
            ReferalData objreferal = new ReferalData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<ReferalData> getResult = new List<ReferalData>();
            objreferal.Referal = prefixText;
            objreferal.ID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetPayableReferalDetails(objreferal);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Referal.ToString());
            }
            return list;
        }
        protected void ddl_source_SelectedIndexChanged(object sender, EventArgs e)
        {
            divmsg1.Visible = false;
            txt_sourcename.Attributes.Remove("disabled");
            txt_sourcename.Text = "";
            txt_referaladrress.Text = "";
            ACE_referal.ContextKey = ddl_source.SelectedValue == "" ? "0" : ddl_source.SelectedValue;
            txt_sourcename.Focus();
            GvPaymentDetails.DataSource = null;
            GvPaymentDetails.DataBind();

        }

        protected void bindgrid()
        {
            List<PaymentManagerData> CommissionList = GetLabGroupCommissionList(0);


            if (CommissionList.Count > 0)
            {
                txt_referaladrress.Text = CommissionList[0].CurrentAddress.ToString();
                GvPaymentDetails.DataSource = CommissionList;
                GvPaymentDetails.DataBind();
                GvPaymentDetails.Visible = true;
            }
        }

        public List<PaymentManagerData>GetLabGroupCommissionList(int curIndex)
        {
            PaymentManagerData objData = new PaymentManagerData();
            PaymentManagerBO objBO = new PaymentManagerBO();
            IFormatProvider iformat = new System.Globalization.CultureInfo("en-GB", true);

            objData.ReferalTypeID = Convert.ToInt32(ddl_source.SelectedValue == "" ? "0" : ddl_source.SelectedValue); 
            objData.ReferalID = Commonfunction.SemicolonSeparation_String_64(txt_sourcename.Text.Trim());
            return objBO.GetServicesCommissionListByReferalId(objData);
        }


        protected void GvPaymentDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GvPaymentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void txt_sourcename_TextChanged(object sender, EventArgs e)
        {
            divmsg1.Visible = false;

            Int64 referalid=Commonfunction.SemicolonSeparation_String_64(txt_sourcename.Text.Trim());

            if (referalid == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SelectReferal", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_sourcename.Text = "";
                GvPaymentDetails.DataSource = null;
                GvPaymentDetails.DataBind();
                return;
            }

            bindgrid();
        }


        protected void btn_update_Click(object sender, EventArgs e)
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

                if (ddl_source.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "SourceType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_sourcename.Text=="")
                {
                    Messagealert_.ShowMessage(lblmessage, "SourceName", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                List<PaymentManagerData> List = new List<PaymentManagerData>();
                PaymentManagerData objpayment = new PaymentManagerData();
                PaymentManagerBO objBO = new PaymentManagerBO();

                foreach (GridViewRow row in GvPaymentDetails.Rows)
                {
                    Label groupid = (Label)GvPaymentDetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_groupid");
                    Label referaltypeid = (Label)GvPaymentDetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_referaltypeid");
                    Label referalid = (Label)GvPaymentDetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_referalid");
                    TextBox commission = (TextBox)GvPaymentDetails.Rows[row.RowIndex].Cells[0].FindControl("txt_commission");
                    PaymentManagerData obj = new PaymentManagerData();

                    obj.ReferalTypeID = Convert.ToInt64(referaltypeid.Text==""?"0":referaltypeid.Text);
                    obj.LabGroupID = Convert.ToInt64(groupid.Text == "" ? "0" : groupid.Text);
                    obj.ReferalID = Convert.ToInt64(referalid.Text == "" ? "0" : referalid.Text);
                    obj.Commission = Convert.ToInt32(commission.Text == "" ? "0" : commission.Text);


                    List.Add(obj);
                }

                objpayment.XMLData = XmlConvertor.PaymentManagerDatatoXML(List).ToString();
                objpayment.EmployeeID = LogData.EmployeeID;

                int result = objBO.UpdateReferalPaymentData(objpayment);
                if (result == 1)
                {
                    bindgrid();
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
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

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            ddl_source.SelectedIndex = 0;
            txt_sourcename.Text = "";
            txt_referaladrress.Text = "";
            GvPaymentDetails.DataSource = null;
            GvPaymentDetails.DataBind();
            ACE_referal.ContextKey = ddl_source.SelectedValue == "" ? "0" : ddl_source.SelectedValue;
            divmsg1.Visible = false;
        }

        protected void btn_Print_Click(object sender, EventArgs e)
        {
            if (ddl_source.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SourceType", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }

            string sourcetypeid = ddl_source.SelectedValue;
            Int64 intsourceid = Commonfunction.SemicolonSeparation_String_64(txt_sourcename.Text.Trim());
            string sourceid =intsourceid.ToString();
            string url = "../MedBills/Reports/ReportViewer.aspx?option=ReferalPCList&SourceTypeID=" + sourcetypeid + "&SourceID=" + sourceid;
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }

        protected void btn_nopclist_Click(object sender, EventArgs e)
        {
            if (ddl_source.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SourceType", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }

            string sourcetypeid = ddl_source.SelectedValue;
            string url = "../MedBills/Reports/ReportViewer.aspx?option=ReferalNOPCList&SourceTypeID=" + sourcetypeid ;
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
    }
}