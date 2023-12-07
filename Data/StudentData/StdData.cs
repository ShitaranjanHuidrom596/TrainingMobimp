using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.StudentData
{
    public class StdData
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string StudentID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Phone_No { get; set; }

        [DataMember]
        public char Sex { get; set; }

        [DataMember]
        public int Age { get; set; }
        
        [DataMember]
        public string Date { get; set; }

    }
}
