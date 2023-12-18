using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class LabTestFormulaData:BaseData
    {
        [DataMember]
        public Int32 FormulaID { get; set; }
        [DataMember]
        public string TestCode { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public Int32 TestID { get; set; }
        [DataMember]
        public string Formula { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }
}
