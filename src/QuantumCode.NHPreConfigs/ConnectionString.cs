using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantumCode.NHEx
{
    public class ConnectionString : ConnectionStringValueType
    {
        public ConnectionString(string value) : base(value) { }

        public static implicit operator ConnectionString(string value)
        {
            return new ConnectionString(value);
        }

        public override bool IsString
        {
            get
            {
                return true;
            }
        }
    }
}
