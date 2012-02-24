using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantumCode.NHEx
{
    public class ConnectionStringName : ConnectionStringValueType
    {
        public ConnectionStringName(string value) : base(value) { }

        public static implicit operator ConnectionStringName(string value)
        {
            return new ConnectionStringName(value);
        }

        public override bool IsName
        {
            get
            {
                return true;
            }
        }
    }
}
