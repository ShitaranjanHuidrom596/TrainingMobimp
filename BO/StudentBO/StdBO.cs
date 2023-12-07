using Data.StudentData;
using DA.StudentDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BO.StudentBO
{
    public class StdBO<T>
    {
        public T updateStudentDetails(String mode,StdData objstd=null)
        {
            switch (mode)
            {
                case "INSERT":
                    T res = default(T);
                    try
                    {
                        StdDal stdDal = new StdDal();
                        res = (T)(dynamic)stdDal.insertStudentDetails(objstd);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    return res;
                case "DELETE":
                    T res2 = default(T);
                    try
                    {
                        StdDal stdDal = new StdDal();
                        res2 = (T)(dynamic)stdDal.DeleteStudentDetails(objstd);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    return res2;
                case "UPDATE":
                    T res3 = default(T);
                    try
                    {
                        StdDal stdDal = new StdDal();
                        res3 = (T)(dynamic)stdDal.UpdateStudentDetails(objstd);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    return res3;
                case "SHOWDETAILS":
                    T res4 = default(T);
                    try
                    {
                        StdDal stddal = new StdDal();
                        res4 = (T)(dynamic)stddal.ShowStudentData();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    return res4;
                case "SELECT_ACTIVE":
                    T res5 = default(T);
                    try
                    {
                        StdDal stddal = new StdDal();
                        res5 = (T)(dynamic)stddal.select_delete_or_not_delete_stud_data(1);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    return res5;
                case "SELECT_DELETE":
                    T res6=default(T);
                    try
                    {
                        StdDal stddal = new StdDal();
                        res6 = (T)(dynamic)stddal.select_delete_or_not_delete_stud_data(0);
                    }
                    catch(Exception ex){
                        MessageBox.Show(ex.ToString());
                    }
                    return res6;
                case "SELECT_ON_NAME":
                    T res7 = default(T);
                    try
                    {
                        StdDal stdDal = new StdDal();
                        res7 = (T)(dynamic)stdDal.select_student_on_name(objstd);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    return res7;
                case "SELECT_ON_ID":
                    T res8 = default(T);
                    try
                    {
                        StdDal stdDal = new StdDal();
                        res8 = (T)(dynamic)stdDal.select_student_on_id(objstd);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    return res8;
                default:
                    return default(T);
            }
        }

        public SqlDataReader ShowAllStudData()
        {
            SqlDataReader result = null;
            try
            {
                StdDal stddal = new StdDal();
                result = stddal.SelectAllStudData();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return result;
        }

        public SqlDataReader select_stud_on_name(StdData objstd)
        {
            SqlDataReader result = null;
            try
            {
                StdDal stddal = new StdDal();
                result = stddal.select_student_on_name(objstd);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return result; 
        }

        public SqlDataReader select_delete_or_not_delete_stud_data(int active_state){
            SqlDataReader result = null;
            try
            {
                StdDal stddal = new StdDal();
                result = stddal.select_delete_or_not_delete_stud_data(active_state);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return result;
        }
    }
}
