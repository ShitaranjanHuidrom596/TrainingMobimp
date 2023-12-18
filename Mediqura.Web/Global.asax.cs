using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Mediqura.Web;
using Mediqura.CommonData.LoginData;
using Mediqura.BOL.UserBO;
using DevExpress.Web.Office;

namespace Mediqura.Web
{
    public class Global : HttpApplication
    {
        LogData objLoginToken;

        void Application_Start(object sender, EventArgs e)
        {
            //RouteTable.Routes.MapHubs();
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            //AuthConfig.RegisterOpenAuth();
            //DocumentManager.HibernationStoragePath = Server.MapPath("~/App_Data/HibernationStorage/"); // Required setting
            //DocumentManager.HibernateTimeout = TimeSpan.FromMinutes(5); // Optional setting
            //DocumentManager.HibernatedDocumentsDisposeTimeout = TimeSpan.FromDays(1); // Optional setting
            //DocumentManager.HibernateAllDocumentsOnApplicationEnd = true; // Optional setting
            //DocumentManager.EnableHibernation = true; // Required setting to turn the hibernation on

        }
        void Application_End(object sender, EventArgs e)
        {
            //Response.Redirect("~/Login.aspx", false);
            //  Code that runs on application shutdown
        }
        void Application_Error(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx", false);
            //Session.Abandon();
        }
        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started  
            if (Session["LoginToken"] != null)
            {
                //Redirect to Welcome Page if Session is not null  
                Response.Redirect("Dashboard.aspx", false);
            }
            else
            {  //Redirect to Login Page if Session is null & Expires 
                Response.Redirect("~/LabLogin.aspx", false);
            }
        }
        void Session_End(object sender, EventArgs e)
        {
            objLoginToken = (LogData)Session["LoginToken"];
            if (objLoginToken != null)
            {
                if (objLoginToken.MedSubStockID == 2)
                {
                    LogData ObjLogdata = new LogData();
                    UserBO LogdataBO = new UserBO();
                    ObjLogdata.FinancialYearID = objLoginToken.FinancialYearID;
                    ObjLogdata.EmployeeID = objLoginToken.EmployeeID;
                    ObjLogdata.MedSubStockID = objLoginToken.MedSubStockID;
                    bool result = LogdataBO.ClearuncompleteTransactions(ObjLogdata);
                    if (result == false)
                    {
                        Response.Redirect("~/LabLogin.aspx", false);
                    }
                }
            }
            if (objLoginToken != null)
            {
                LogData ObjLogdata = new LogData();
                UserBO LogdataBO = new UserBO();
                ObjLogdata.IPaddress = "";
                ObjLogdata.IsActiveLogin = 0;
                ObjLogdata.UserName = objLoginToken.UserName;
                ObjLogdata.UserPassword = objLoginToken.UserPassword;
                ObjLogdata.RoleID = objLoginToken.RoleID;
                bool result = LogdataBO.UpdateLoginStatus(ObjLogdata);
                if (result == true)
                {
                    Response.Redirect("~/Login.aspx", false);
                }
            }

            // Code that runs when a session ends.   
            // Note: The Session_End event is raised only when the sessionstate mode  
            // is set to InProc in the Web.config file. If session mode is set to StateServer   
            // or SQLServer, the event is not raised.  
        }
    }
}
