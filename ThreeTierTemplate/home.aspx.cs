using BO.StudentBO;
using Common;
using Data.StudentData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace ThreeTierTemplate
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(Request.Form["__EVENTTARGET"]=="del_stud")
            {
                string id = Request.Form["__EVENTARGUMENT"];
                deleteItem(id);
            }
            
            StdBO<int> objbo = new StdBO<int>();

            if (!IsPostBack)
            {
                get_delete_or_not_delete_stud_data(1);

            }
            
        }

        public void deleteItem(string id)
        {
            StdData student = new StdData();
            StdBO<int> objbo = new StdBO<int>();

            student.StudentID = id;

            int result = objbo.updateStudentDetails("DELETE",student);
            get_delete_or_not_delete_stud_data(1);
        }


        protected void namedd_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*StdData student = new StdData();
            StdBO stdbo = new StdBO();
            student.Name= namedd.Text;
            SqlDataReader reader = stdbo.GetID(student);
            while (reader.Read())
            {
                delete_tb.Text = reader["id"].ToString();
            }*/
        }

        public void loadResource()
        {
            StdBO<int> objbo = new StdBO<int>();

            SqlDataReader rd = objbo.ShowAllStudData();
            DataTable dt = new DataTable();
            dt.Load(rd);
            students_grid.DataSource = dt;
            students_grid.DataBind();
        }

        protected void get_ID_Click(object sender, EventArgs e)
        {
       

        }


        // Method to select the data wheter it was delete, not delete or all
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (student_delete_dd.Text)
            {
                case "All":
                    get_delete_or_not_delete_stud_data(1);
                    break;
                case "Deleted":
                    get_delete_or_not_delete_stud_data(0);
                    break;
                default:
                    break;
            }


          /*  string inputString = "john";
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);

            using (SHA256 sha256Hash = SHA256.Create())
            {
            byte[] hashBytes = sha256Hash.ComputeHash(inputBytes);
            string hashString = BitConverter.ToString(hashBytes).Replace("-", "");
                MessageBox.Show(hashString);
            }*/
        }

        // Method to select all the students data based on the students are deleted or not deleted
        public void get_delete_or_not_delete_stud_data(int active_state)
        {
            StdBO<SqlDataReader> stdbo = new StdBO<SqlDataReader>();
            SqlDataReader reader = active_state == 0 ? stdbo.updateStudentDetails("SELECT_DELETE") : stdbo.updateStudentDetails("SELECT_ACTIVE");
            DataTable dt = new DataTable();
            dt.Load(reader);

            students_grid.DataSource = dt;
            students_grid.DataBind();
        }

        protected void search_stud_button_Click(object sender, EventArgs e)
        {
            StdData student = new StdData();
            student.Name = name_search.Text;
            if (name_search.Text != "")
            {

                StdBO<SqlDataReader> stdbo = new StdBO<SqlDataReader>();

                SqlDataReader reader = stdbo.updateStudentDetails("SELECT_ON_NAME", student);
                if (reader.HasRows)
                {
                    grid_student_namemacth.Visible = true;
                    lb_no_result.Visible = false;
                    DataTable dt = new DataTable();

                    dt.Load(reader);
                    grid_student_namemacth.DataSource = dt;
                    grid_student_namemacth.DataBind();
                }
                else
                {
                    grid_student_namemacth.Visible = false;
                    lb_no_result.Text = "No Result Found";
                    lb_no_result.Visible = true;
                }
            }
            else
            {
                grid_student_namemacth.Visible = false;
                lb_no_result.Text = "No Result Found";
                lb_no_result.Visible = true;
            }
        }

        protected void students_grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            if (e.CommandName == "DeleteRow")
            {
                deleteItem(id);
            }
            else
            {
                Response.Redirect("edit.aspx"+"?id="+id);
            }
        }
    }
}