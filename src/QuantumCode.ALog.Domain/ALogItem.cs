using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuantumCode.ALog.Domain
{
    [DataContract]
    public class ALogItem
    {
        [DataMember]
        public virtual UInt64 ID { get; set; }

        [DataMember]
        public virtual string Level { get; set; }

        [DataMember]
        public virtual string Origin { get; set; }

        [DataMember]
        public virtual string Message { get; set; }

        [DataMember]
        public virtual string Exception { get; set; }

        [DataMember]
        public virtual DateTime DateTimeStamp { get; set; }

        [DataMember]
        public virtual string Creator { get; set; }

        [DataMember]
        public virtual string Action { get; set; }
    }
}
