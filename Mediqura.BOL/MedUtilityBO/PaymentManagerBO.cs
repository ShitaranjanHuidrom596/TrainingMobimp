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
    public class PaymentManagerBO
    {
        public List<PaymentManagerData> GetServicesCommissionListByReferalId(PaymentManagerData obj)
        {
            List<PaymentManagerData> result = null;
            try
            {
                PaymentManagerDA objDA = new PaymentManagerDA();
                result = objDA.GetServicesCommissionByReferalId(obj);

            }catch(Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public int UpdateReferalPaymentData(PaymentManagerData objData)
        {
            int result = 0;
            try
            {
                PaymentManagerDA objDA = new PaymentManagerDA();
                result = objDA.UpdateReferalPaymentData(objData);
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
