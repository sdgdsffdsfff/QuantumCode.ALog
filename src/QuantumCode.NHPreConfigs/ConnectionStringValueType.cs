using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantumCode.NHEx
{
    public class ConnectionStringValueType
    {
        protected string _ConnectionStringValue;

        public ConnectionStringValueType(string value)
        {
            _ConnectionStringValue = value;
        }

        public static implicit operator string(ConnectionStringValueType conn)
        {
            return conn._ConnectionStringValue;
        }

        public override string ToString()
        {
            return _ConnectionStringValue;
        }

        public virtual bool IsName { get { return false; } }

        public virtual bool IsString { get { return false; } }
    }
}
