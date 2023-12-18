using Mediqura.CommonData.MedUtilityData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.DAL.MedUtilityDA
{
    public class LabTestFormulaDA
    {
      
        public int UpdateLabTestFormula(LabTestFormulaData obj)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];


                    arParms[0] = new SqlParameter("@TestID", SqlDbType.Int);
                    arParms[0].Value = obj.TestID;

                    arParms[1] = new SqlParameter("@Formula", SqlDbType.VarChar);
                    arParms[1].Value = obj.Formula;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[2].Value = obj.EmployeeID;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[3].Direction = ParameterDirection.Output;

                    arParms[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[4].Value = obj.IsActive;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateTestFormulaMST", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[3].Value);
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public List<LabTestFormulaData> SearchLabTestFormulaDetails(LabTestFormulaData objlabformula)
        {
            List<LabTestFormulaData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@TestID", SqlDbType.Int);
                    arParms[0].Value = objlabformula.TestID;

                    arParms[1] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[1].Value = objlabformula.IsActive;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchFormulaDetails", arParms);
                    List<LabTestFormulaData> lstPaymentTypeDetails = ORHelper<LabTestFormulaData>.FromDataReaderToList(sqlReader);
                    result = lstPaymentTypeDetails;
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public int DeleteLabTestFormulaDetailsByID(LabTestFormulaData obj)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    //arParms[0] = new SqlParameter("@StateID", SqlDbType.Int);
                    //arParms[0].Value = obj.StateID;

                    //arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    //arParms[1].Value = obj.EmployeeID;

                    //arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    //arParms[2].Direction = ParameterDirection.Output;


                    //arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    //arParms[3].Value = obj.Remarks;

                    //arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    //arParms[4].Value = obj.IPaddress;

                    //int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteStateDetailsByID", arParms);
                    //if (result_ > 0 || result_ == -1)
                    //    result = Convert.ToInt32(arParms[2].Value);
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
    }
}
