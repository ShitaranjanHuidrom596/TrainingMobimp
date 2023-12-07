using BO.StudentBO;
using Data.StudentData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace ThreeTierTemplate
{
    public partial class update : System.Web.UI.Page
    {
        string id = "";
        char gender;
        StdData student = new StdData();
        StdBO<SqlDataReader> objbo = new StdBO<SqlDataReader>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                id = Request.QueryString["id"];
                student.StudentID = id;
                SqlDataReader reader = objbo.updateStudentDetails("SELECT_ON_ID", student);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tbNamereg.Text = reader["Name"].ToString();
                        tbAddressreg.Text = reader["Address"].ToString();
                        tbPhonereg.Text = reader["Phone_No"].ToString();
                        tbEmailreg.Text = reader["Email"].ToString();
                        if (reader["Sex"].ToString() == "M")
                        {
                            rdgen1reg.Checked = true;
                            gender = 'M';
                        }
                        else
                        {
                            rdgen2reg.Checked = true;
                            gender = 'M';
                        }
                        tbAgereg.Text = reader["Age"].ToString();
                        tbRegreg.Text = reader["Reg_Date"].ToString();
                    }
                }
            }
        }

        //protected delegate void My_Callback(string result);

        protected void updateBtn_Click(object sender, EventArgs e)
        {
            StdData student = new StdData();
            StdBO<int> objbo = new StdBO<int>();
            student.Name = tbNamereg.Text;
            student.Address = tbAddressreg.Text;
            student.Age = int.Parse(tbAgereg.Text);
            student.Email = tbEmailreg.Text;
            student.Phone_No = tbPhonereg.Text;
            student.Sex = gender;
            student.Date = tbRegreg.Text;
            student.StudentID = Request.QueryString["id"];

            int result = objbo.updateStudentDetails("UPDATE", student);

            //update_successfully(res => my_callback(res));
            my_callback("Update successfully");
        }

        public void my_callback(string result)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Update Successfully');window.location.href = 'home.aspx';", true);
        }


/*        public void update_successfully(Action<string> callback)
        {
            string result = "Update Successfully";
            callback(result);
            MessageBox.Show("Hi");
        }

        public void my_callback(string result)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Update Successfully');",true);
        }*/


     /*   protected void my_callback(string result)
        {
            MessageBox.Show(result);
            //Response.Redirect("home.aspx");
        }

        protected void update_successfully(My_Callback callback)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Update Successfully');",true);
            callback("Callback call");
        }*/

        protected void rdgen1reg_CheckedChanged(object sender, EventArgs e)
        {
            gender = 'M';
        }

        protected void rdgen2reg_CheckedChanged(object sender, EventArgs e)
        {
            gender = 'F';
        }
    }
}