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
    public class AntiBioticNameBO
    {
        public int UpdateAntibioticDetails(AntiBioticNameData objAntiBioticNameData)
        {
            int result = 0;
            try
            {
                AntiBioticNameDA objPaymentTypeMasteDA = new AntiBioticNameDA();
                result = objPaymentTypeMasteDA.UpdateAntiBioticNameDetails(objAntiBioticNameData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AntiBioticNameData> SearchAntibioticDetails(AntiBioticNameData objAntiBioticNameData)
        {
            List<AntiBioticNameData> result = null;
            try
            {
                AntiBioticNameDA objPaymentTypeMasteDA = new AntiBioticNameDA();
                result = objPaymentTypeMasteDA.SearchAntiBioticNameDetails(objAntiBioticNameData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AntiBioticNameData> GetAntibioticDetailsByID(AntiBioticNameData objAntiBioticNameData)
        {
            List<AntiBioticNameData> result = null;

            try
            {
                AntiBioticNameDA objPaymentTypeMasteDA = new AntiBioticNameDA();
                result = objPaymentTypeMasteDA.GetAntiBioticeNameDetailsByID(objAntiBioticNameData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteAntibioticDetailsByID(AntiBioticNameData objAntiBioticNameData)
        {
            int result = 0;
            try
            {
                AntiBioticNameDA objantibioticDA = new AntiBioticNameDA();
                result = objantibioticDA.DeleteAntiBioticNameDetailsByID(objAntiBioticNameData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AntiBioticNameData> SearchAntiBioticName(AntiBioticNameData objAntiBioticNameData)
        {
            List<AntiBioticNameData> result = null;

            try
            {
                AntiBioticNameDA objantibioticDA = new AntiBioticNameDA();
                result = objantibioticDA.SearcAntiBioticName(objAntiBioticNameData);

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
