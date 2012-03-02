using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuantumCode.ALog.Domain
{
    [DataContract(Name = "ALogItem")]
    public class ALogItem
    {
        [DataMember(Name = "id")]
        public virtual UInt64 ID { get; set; }

        [DataMember(Name = "level")]
        public virtual string Level { get; set; }

        [DataMember(Name = "origin")]
        public virtual string Origin { get; set; }

        [DataMember(Name = "message")]
        public virtual string Message { get; set; }

        [DataMember(Name = "exception")]
        public virtual string Exception { get; set; }

        [DataMember(Name = "datetimestamp")]
        public virtual DateTime DateTimeStamp { get; set; }

        [DataMember(Name = "creator")]
        public virtual string Creator { get; set; }

        [DataMember(Name = "action")]
        public virtual string Action { get; set; }
    }
}
