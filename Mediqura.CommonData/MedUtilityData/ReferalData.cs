﻿using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class ReferalData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public decimal Percentage { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string Referal { get; set; }
        [DataMember]
        public Int64 ReferalTypeID { get; set; }
        [DataMember]
        public string ReferalTypeName { get; set; }
        [DataMember]
        public Int64 DetailsTypeID { get; set; }
        [DataMember]
        public string SharePayType { get; set; }
        [DataMember]
        public Int32 SharePayTypeID { get; set; }
    }
    public class ReferalDatatoExcel
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
    }
}
