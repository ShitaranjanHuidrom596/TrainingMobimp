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
    public class ReferalTypeBO
    {
        public int UpdateReferalTypeDetails(ReferalTypeData objReferalTypeData)
        {
            int result = 0;
            try
            {
                ReferalTypeDA objReferalTypeMasteDA = new ReferalTypeDA();
                result = objReferalTypeMasteDA.UpdateReferalTypeDetails(objReferalTypeData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ReferalTypeData> GetReferalTypeDetailsByID(ReferalTypeData objReferalTypeData)
        {
            List<ReferalTypeData> result = null;

            try
            {
                ReferalTypeDA objReferalTypeMasteDA = new ReferalTypeDA();
                result = objReferalTypeMasteDA.GetReferalTypeDetailsByID(objReferalTypeData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteReferalTypeDetailsByID(ReferalTypeData objReferalTypeData)
        {
            int result = 0;
            try
            {
                ReferalTypeDA objPatientTypeMasteDA = new ReferalTypeDA();
                result = objPatientTypeMasteDA.DeleteReferalTypeDetailsByID(objReferalTypeData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ReferalTypeData> SearchReferalTypeDetails(ReferalTypeData objReferalTypeData)
        {
            List<ReferalTypeData> result = null;
            try
            {
                ReferalTypeDA objPatientTypeMasteDA = new ReferalTypeDA();
                result = objPatientTypeMasteDA.SearchReferalTypeDetails(objReferalTypeData);
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
