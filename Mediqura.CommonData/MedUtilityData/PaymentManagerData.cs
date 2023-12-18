using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class PaymentManagerData : BaseData
    {
        [DataMember]
        public  Int64 ID {get;set;}
        [DataMember]
        public Int64 ReferalTypeID { get; set; }
        [DataMember]
        public Int64 ReferalID { get; set; }
        [DataMember]
        public string ReferalName { get; set; }
        [DataMember]
        public Int64 LabGroupID { get; set; }
        [DataMember]
        public string LabGroupName { get; set; }
        [DataMember]
        public decimal Commission { get; set; }
        [DataMember]
        public string CurrentAddress { get; set; }

    }
}
