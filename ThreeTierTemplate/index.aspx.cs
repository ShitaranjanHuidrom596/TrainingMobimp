using BO.StudentBO;
using Data.StudentData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace ThreeTierTemplate
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string gender = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void regBtn_Click(object sender, EventArgs e)
        {
            StdData student = new StdData();
            StdBO<int> objbo = new StdBO<int>();

            student.Name = tbName.Text;
            student.Address = tbAddress.Text;
            student.Age = int.Parse(tbAge.Text);
            student.Email = tbEmail.Text;
            student.Phone_No = tbPhone.Text;
            student.Sex = char.Parse(gender);
            student.Date = tbReg.Text;

            int result=objbo.updateStudentDetails("INSERT",student);
            if (result == 2)
            {
                MessageBox.Show("You are Registered");
                Response.Redirect("home.aspx");
            }
            else
            {
                MessageBox.Show("You are already there");
            }
        }

        protected void rdgen1_CheckedChanged(object sender, EventArgs e)
        {
            gender = "M";

        }

        protected void rdgen2_CheckedChanged(object sender, EventArgs e)
        {
            gender = "F";
        }
    }
}