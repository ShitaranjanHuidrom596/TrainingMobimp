using Mediqura.CommonData.MedUtilityData;
using Mediqura.DAL.MedUtilityDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedUtilityBO
{
    public class LabTestFormulaBO
    {
        public List<LabTestFormulaData> SearchFormulaDetails(LabTestFormulaData obj)
        {
            List<LabTestFormulaData> result = null;
            try
            {
                LabTestFormulaDA objLabFormulaDA = new LabTestFormulaDA();
                result = objLabFormulaDA.SearchLabTestFormulaDetails(obj);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public int UpdateTestFormulaDetails(LabTestFormulaData obj)
        {
            int result = 0;
            try
            {
                LabTestFormulaDA objLabFormulaDA = new LabTestFormulaDA();
                result = objLabFormulaDA.UpdateLabTestFormula(obj);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteTestFormulaDetailsByID(LabTestFormulaData obj)
        {
            int result = 0;
            try
            {
                LabTestFormulaDA objLabFormulaDA = new LabTestFormulaDA();
                result = objLabFormulaDA.DeleteLabTestFormulaDetailsByID(obj);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
    }
}
