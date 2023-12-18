using Mediqura.BOL.CommonBO;
using Mediqura.CommonData.Common;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web
{
    public partial class CollectionCentreLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {            
                bindddl();   
            }

        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_center, mstlookup.GetLookupsList(LookupName.RunnerList));

        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MedDashboard/CollectionCentreDashboard.aspx");
        }
    }
}