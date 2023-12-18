using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedDashboard
{
    public partial class CollectionCentreDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CollectionCentreName"] != null)
            {

                centrelbl.InnerText= Session["CollectionCentreName"].ToString();

            }
        }
    }
}