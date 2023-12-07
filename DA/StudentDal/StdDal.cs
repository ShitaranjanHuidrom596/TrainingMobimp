using Data.StudentData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace DA.StudentDal
{
    public class StdDal
    {
        public int insertStudentDetails(StdData objstd)
        {
            int result = 0;
            try
            {
                SqlParameter[] argParams = new SqlParameter[8];

                argParams[0] = new SqlParameter("@Name", SqlDbType.VarChar);
                argParams[0].Value = objstd.Name;

                argParams[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                argParams[1].Value = ParameterDirection.Output;

                argParams[2] = new SqlParameter("@Address", SqlDbType.VarChar);
                argParams[2].Value = objstd.Address;

                argParams[3] = new SqlParameter("@Email", SqlDbType.VarChar);
                argParams[3].Value = objstd.Email;

                argParams[4] = new SqlParameter("@Phone_No", SqlDbType.VarChar);
                argParams[4].Value = objstd.Phone_No;

                argParams[5] = new SqlParameter("@Sex", SqlDbType.NVarChar);
                argParams[5].Value = objstd.Sex;

                argParams[6] = new SqlParameter("Age", SqlDbType.Int);
                argParams[6].Value = objstd.Age;

                argParams[7] = new SqlParameter("Reg_Date", SqlDbType.Date);
                argParams[7].Value = objstd.Date;




                int results_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "InsertStudData", argParams);
                if (results_ > 0 || results_ == 1)
                {
                    result = Convert.ToInt32(argParams[1].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on StudentDal");
                MessageBox.Show(Convert.ToString(ex));
            }
            return result;
        }

        public int DeleteStudentDetails(StdData obstd)
        {
            int result = 0;
            try
            {
                SqlParameter[] argParams = new SqlParameter[2];
                argParams[0] = new SqlParameter("@Student_ID", SqlDbType.VarChar);
                argParams[0].Value = obstd.StudentID;

                argParams[1] = new SqlParameter("Output", SqlDbType.SmallInt);
                argParams[1].Value = ParameterDirection.Output;

                int results_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "DeleteStudData", argParams);
                if (results_ > 0 || results_ == 1)
                {
                    result = Convert.ToInt32(argParams[1].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannont Delete");
            }
            return result;
        }
        public int UpdateStudentDetails(StdData objstd)
        {
            int result = 0;
            try
            {
                SqlParameter[] argParams = new SqlParameter[9];

                argParams[0] = new SqlParameter("@Name", SqlDbType.VarChar);
                argParams[0].Value = objstd.Name;

                argParams[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                argParams[1].Value = ParameterDirection.Output;

                argParams[2] = new SqlParameter("@Address", SqlDbType.VarChar);
                argParams[2].Value = objstd.Address;

                argParams[3] = new SqlParameter("@Email", SqlDbType.VarChar);
                argParams[3].Value = objstd.Email;

                argParams[4] = new SqlParameter("@Phone_No", SqlDbType.VarChar);
                argParams[4].Value = objstd.Phone_No;

                argParams[5] = new SqlParameter("@Sex", SqlDbType.NVarChar);
                argParams[5].Value = objstd.Sex;

                argParams[6] = new SqlParameter("@Age", SqlDbType.Int);
                argParams[6].Value = objstd.Age;

                argParams[7] = new SqlParameter("@Reg_Date", SqlDbType.Date);
                argParams[7].Value = objstd.Date;

                argParams[8] = new SqlParameter("@Student_ID", SqlDbType.VarChar);
                argParams[8].Value = objstd.StudentID;

                int results_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "UpdateStudData", argParams);
                if (results_ > 0 || results_ == 1)
                {
                    result = Convert.ToInt32(argParams[1].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return result;
        }

        /* public SqlDataReader ShowStudentData()
         {

         }*/

        public SqlDataReader ShowStudentData()
        {

            SqlConnection connection = new SqlConnection(GlobalConstant.ConnectionString);
            SqlCommand command = new SqlCommand("SelectAllStudData", connection);
            command.CommandType = CommandType.StoredProcedure;
            /*   SqlParameter[] parameters = new SqlParameter[1];
               parameters[0] = new SqlParameter("@ID", SqlDbType.VarChar);
               parameters[0].Value = obstd.ID;

               // Add the parameters to the SqlCommand object
               command.Parameters.AddRange(parameters);*/

            connection.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return null;
        }

        public SqlDataReader SelectAllStudData()
        {
            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection(GlobalConstant.ConnectionString);
            SqlCommand command = new SqlCommand("SelectAllStudData", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            try
            {
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return reader;
        }

        public SqlDataReader select_student_on_name(StdData objstd)
        {
            SqlDataReader reader = null;
            SqlParameter[] parameters = new SqlParameter[1];

            parameters[0] = new SqlParameter("@Name", SqlDbType.VarChar);
            parameters[0].Value = objstd.Name;

            try
            {
                reader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, "SelectStudentOnName", parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return reader;
        }

        public SqlDataReader select_student_on_id(StdData objstd)
        {
            SqlDataReader reader = null;
            SqlParameter[] parameters = new SqlParameter[1];

            parameters[0] = new SqlParameter("@Student_ID", SqlDbType.VarChar);
            parameters[0].Value = objstd.StudentID;

            try
            {
                reader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, "SelectStudOnID", parameters);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return reader; 
        }

        public SqlDataReader select_delete_or_not_delete_stud_data(int active_state)
        {
            SqlDataReader reader = null;
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Active_state", SqlDbType.Bit);
            parameters[0].Value = active_state;

            reader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, "SelectStudStateData", parameters);
            return reader;
        }

        public SqlDataReader getID(StdData objstd)
        {
            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection(GlobalConstant.ConnectionString);
            SqlCommand command = new SqlCommand("GetID", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Name", SqlDbType.VarChar);
            parameters[0].Value = objstd.Name;

            parameters[1] = new SqlParameter("@Email", SqlDbType.VarChar);
            parameters[1].Value = objstd.Email;

            // Add the parameters to the SqlCommand object
            command.Parameters.AddRange(parameters);

            connection.Open();
            try
            {
                reader = command.ExecuteReader();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return reader;
        }
    }
}
